using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using Cinemachine;

public enum CanvasType{
    SplashScene,
    MainMenu,
    PlayScene,
    ResultScene,
}

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    public static GameManager Instance{
        get{
            if(_instance == null){
                _instance = FindObjectOfType<GameManager>();
            }
            return _instance;
        }
    }

    [Header("CANVAS CONTROLLER")]
    #region CANVAS CONTROLLER
    public CanvasType type;
    #endregion


    [Header("CHARACTER CONTROLLER")]
    #region CHARACTER CONTROLLER
    private GameObject player;
    private CharacterControllers characterControllers;
    [SerializeField] private float characterDeadTime = 2f; ///DEFAULT IS 2
    #endregion

    [Header("UI ITEM CONTROLLER")]
    #region UI ITEM CONTROLLER
    private int currentScore;

    private Text highScoreText;
    private Text currentScoreText;
    #endregion

    [Header("CUTSCENE CONTROLLER")]
    private CinemachineVirtualCamera gameCamera1;
    private Transform mainCamera;

    [Header("FFSM VALUE CONTROLER")]
    #region FFSM VALUE CONTROLLER
    public float speedVal;
    public float scoreVal;

    public AnimationCurve speedGrade;
    public Condition speedGradeCondition;
    public AnimationCurve speedTriangle;
    public Condition speedTriangleCondition;
    public AnimationCurve speedRevGrade;
    public Condition speedRevGradeCondition;

    public AnimationCurve scoreGrade;
    public Condition scoreGradeCondition;
    public AnimationCurve scoreTriangle;
    public Condition scoreTriangleCondition;
    public AnimationCurve scoreRevGrade;
    public Condition scoreRevGradeCondition;
    #endregion

    private void Awake(){
        Application.targetFrameRate = 120;

        SwitchCanvas();
    }

    private void Update(){
        UIUpdate();
    }

    private void SwitchCanvas(){
        UserDataManager.Load();

        switch(type){
            case CanvasType.SplashScene :
                UserDataManager.Remove();
            break;
            case CanvasType.MainMenu :
                highScoreText           = GameObject.FindWithTag("HighScore").GetComponent<Text>();
                highScoreText.text      =  ShowHighScore().ToString();
            break;
            case CanvasType.PlayScene :
                player = GameObject.FindWithTag("Player");
                mainCamera = GameObject.FindWithTag("MainCamera").transform;
                currentScoreText = GameObject.FindWithTag("CurrentScore").GetComponent<Text>();
                characterControllers = player.gameObject.GetComponent<CharacterControllers>();
		        gameCamera1 = mainCamera.gameObject.GetComponent<CinemachineVirtualCamera>();
                gameCamera1.LookAt = characterControllers.transform;
                gameCamera1.Follow = characterControllers.transform;
            break;
            case CanvasType.ResultScene :
                currentScoreText        =  GameObject.FindWithTag("CurrentScore").GetComponent<Text>();
                currentScoreText.text   =  ShowCurrentScore().ToString();
                AddHighScore(ShowCurrentScore());
                FuzzySet speed = new FuzzySet(
                    new Shapes(speedGrade, speedGradeCondition),
                    new Shapes(speedTriangle, speedTriangleCondition),
                    new Shapes(speedRevGrade, speedRevGradeCondition)
                );
                FuzzySet score = new FuzzySet(
                    new Shapes(scoreGrade, scoreGradeCondition),
                    new Shapes(scoreTriangle, scoreTriangleCondition),
                    new Shapes(scoreRevGrade, scoreRevGradeCondition)
                );
                // // FuzzyLogic.Instance.FuzzyTest(speed, score, ShowLastSpeed(), ShowCurrentScore());
                FuzzyLogic.Instance.FuzzyTest(speed, score, speedVal, scoreVal);
            break;
            default :
            break;
        }
    }

    private void UIUpdate(){
        if(type == CanvasType.SplashScene){
            Invoke("LoadMainMenu", 5f);
        }
        // else if(type == CanvasType.MainMenu){
            
        // }
        else if(type == CanvasType.PlayScene){
            if(GameManager.Instance.characterControllers.isDead != true){
                currentScoreText.text = currentScore.ToString();
                currentScore = (int)player.transform.position.z;
            }
        }
        // else if(type == CanvasType.ResultScene){
            
        // }
    }

    public void LoadMainMenu(){
        SceneManager.LoadScene("MainMenu");
	}

    public void LoadScene(string menu){
		SceneManager.LoadScene(menu);
	}
    
    public async void PlayerDead(){
        characterControllers.Dead();

        // while (characterDeadTime > 0){
        //     characterDeadTime -= Time.deltaTime;
        //     await Task.Yield();
        // }
        
        AddLastScore(currentScore);
        AddCurrentScore(currentScore);
        AddHighScore(currentScore);
        AddLastSpeed(characterControllers.currentSpeed);

        player.SetActive(false);

        await Task.Yield();
        LoadScene("Result");
    }

    ///GET USER DATA MANAGER VALUE
    #region GET USERDATA
    public int ShowHighScore(){
        return UserDataManager.Progress.HighScore;
    }

    public int ShowCurrentScore(){
        return UserDataManager.Progress.CurrentScore;
    }

    public int ShowLastScore(){
        return UserDataManager.Progress.LastScore;
    }

    public float ShowLastSpeed(){
        return UserDataManager.Progress.LastSpeed;
    }

    public bool ShowIsSoundMuted(){
        return UserDataManager.Progress.IsSoundMuted;
    }
    #endregion

    ///SET USER DATA MANAGER VALUE
    #region SET USERDATA
    public void AddHighScore(int value){
        if(value > UserDataManager.Progress.HighScore){
            UserDataManager.Progress.HighScore = value;
            UserDataManager.Save();
        }
    }

    public void AddLastScore(int value){
        UserDataManager.Progress.LastScore = value;
        UserDataManager.Save();
    }

    public void AddCurrentScore(int value){
        UserDataManager.Progress.CurrentScore = value;
        UserDataManager.Save();
    }

    public void AddLastSpeed(float value){
        UserDataManager.Progress.LastSpeed = value;
        UserDataManager.Save();
    }

    public void SetTutorialDone(){
        UserDataManager.Progress.IsTutorialDone = true;
        UserDataManager.Save();
    }

    public void SetSoundMuted(bool value){
        UserDataManager.Progress.IsSoundMuted = value;
        UserDataManager.Save();
    }
    #endregion
}