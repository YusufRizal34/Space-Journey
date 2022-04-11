using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using Cinemachine;

public enum CanvasType{
    SplashScreen,
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
    private int deadCount = 0;
    #endregion

    [Header("CURVED WORLD CONTROLLER")]
    #region CURVED WORLD CONTROLLER
    public float changeCurvedWorldTime = 20f;
    #endregion

    [Header("DATA CONTROLLER")]
    #region DATA CONTROLLER
    public bool isDataLoaded = false;
    #endregion

    [Header("UI CONTROLLER")]
    #region UI CONTROLLER
    private int currentScore;
    private float loadingProgress = 1;

    private Text highScoreText;
    private Text currentScoreText;
    private Slider loadingSlider;
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

    // private void Start() {
        
    // }

    private void Update(){
        UIUpdate();
    }

    private void SwitchCanvas(){
        
        switch(type){
            case CanvasType.SplashScreen :
                UserData.Save();
                loadingSlider = GameObject.FindWithTag("LoadingSlider").GetComponent<Slider>();
                StartCoroutine(UserData.LoadFromDatabase(() => {
                    if(loadingSlider.value == 0) LoadScene("MainMenu");
                }));
            break;
            case CanvasType.MainMenu :
                highScoreText       =  GameObject.FindWithTag("HighScore").GetComponent<Text>();
                highScoreText.text  = ShowHighScore().ToString();
            break;
            case CanvasType.PlayScene :
                player = FindObjectOfType<CharacterControllers>().gameObject;
                mainCamera = GameObject.FindWithTag("MainCamera").transform;
                currentScoreText = GameObject.FindWithTag("CurrentScore").GetComponent<Text>();
                characterControllers = player.gameObject.GetComponent<CharacterControllers>();
                characterControllers.acceleration = ShowAcceleration();
		        gameCamera1 = mainCamera.gameObject.GetComponent<CinemachineVirtualCamera>();
                gameCamera1.LookAt = characterControllers.transform;
                gameCamera1.Follow = characterControllers.transform;
            break;
            case CanvasType.ResultScene :
                currentScoreText        =  GameObject.FindWithTag("CurrentScore").GetComponent<Text>();
                currentScoreText.text   =  ShowCurrentScore().ToString();
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

                float fuzzyValue = FuzzyLogic.Instance.FuzzyTest(speed, score, ShowLastSpeed(), ShowCurrentScore());
                // float fuzzyValue = FuzzyLogic.Instance.FuzzyTest(speed, score, speedVal, scoreVal);
                int acc = AccelerationController.Instance.GetAccelerationLevel(fuzzyValue);
                AddAcceleration(acc);
                UserData.Save(true);
            break;
            default :
            break;
        }
    }

    private void UIUpdate(){
        if(type == CanvasType.SplashScreen){
            Loading();
        }
        else if(type == CanvasType.PlayScene){
            if(characterControllers.isDead != true){
                currentScoreText.text = currentScore.ToString();
                currentScore = (int)player.transform.position.z;
            }
        }
    }

    public async void Loading(){
        while(loadingProgress > 0){
            loadingProgress -= 0.001f;
            loadingSlider.value = loadingProgress;
            await Task.Yield();
        }
    }

    public void LoadScene(string menu){
		SceneManager.LoadScene(menu);
	}
    
    public async void PlayerDead(){
        if(deadCount < 1){
            characterControllers.Dead();
            deadCount++;
        }
        
        while (characterDeadTime > 0){
            characterDeadTime -= Time.deltaTime;
            await Task.Yield();
        }
        
        AddLastScore(currentScore);
        AddCurrentScore(currentScore);
        AddHighScore(currentScore);
        AddLastSpeed(characterControllers.currentSpeed);

        LoadScene("Result");
    }

    ///GET USER DATA MANAGER VALUE
    #region GET USERDATA
    public int ShowHighScore(){
        return UserData.Progress.HighScore;
    }

    public int ShowCurrentScore(){
        return UserData.Progress.CurrentScore;
    }

    public int ShowLastScore(){
        return UserData.Progress.LastScore;
    }

    public float ShowLastSpeed(){
        return UserData.Progress.LastSpeed;
    }

    public int ShowAcceleration(){
        return UserData.Progress.Acceleration;
    }
    #endregion

    ///SET USER DATA MANAGER VALUE
    #region SET USERDATA
    public void AddHighScore(int value){
        if(value > UserData.Progress.HighScore){
            UserData.Progress.HighScore = value;
            UserData.Save();
        }
    }

    public void AddCurrentScore(int value){
        UserData.Progress.CurrentScore = value;
        UserData.Save();
    }

    public void AddLastSpeed(float value){
        UserData.Progress.LastSpeed = value;
        UserData.Save();
    }
    public void AddLastScore(int value){
        UserData.Progress.LastScore = value;
        UserData.Save();
    }
    
    public void AddAcceleration(int value){
        UserData.Progress.Acceleration = value;
        UserData.Save();
    }
    #endregion
}