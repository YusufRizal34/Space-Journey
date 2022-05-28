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

    [Header("UI CONTROLLER")]
    #region UI CONTROLLER
    private int currentScore;
    private float score = 0;
    private float loadingProgress = 1;
    private float t = 0.1f;
    private bool isTutorialOpened;

    private Text highScoreText;
    private Text currentScoreText;
    private Text accelerationText;
    private Slider loadingSlider;
    public GameObject tutorialPanel;
    public Image arrow;
    public Sprite[] arrowImage;
    #endregion

    [Header("CUTSCENE CONTROLLER")]
    private CinemachineVirtualCamera gameCamera1;
    private Transform mainCamera;

    [Header("FFSM VALUE CONTROLER")]
    #region FFSM VALUE CONTROLLER
    public float speedVal;
    public float scoreVal;

    public Shapes[] fuzzySetSpeed;
    public Shapes[] fuzzySetScore;
    #endregion

    private void Awake(){
        Application.targetFrameRate = 120;
        SwitchCanvas();
    }

    private void Start() {
        UIStart();
    }

    private void Update(){
        UIUpdate();
    }

    private void SwitchCanvas(){
        UserDataManager.Load();
        switch(type){
            case CanvasType.SplashScreen :
                loadingSlider = GameObject.FindWithTag("LoadingSlider").GetComponent<Slider>();
            break;
            case CanvasType.MainMenu :
                highScoreText       =  GameObject.FindWithTag("HighScore").GetComponent<Text>();
            break;
            case CanvasType.PlayScene :
                player              = FindObjectOfType<CharacterControllers>().gameObject;
                mainCamera          = GameObject.FindWithTag("MainCamera").transform;
                currentScoreText    = GameObject.FindWithTag("CurrentScore").GetComponent<Text>();
            break;
            case CanvasType.ResultScene :
                currentScoreText        =  GameObject.FindWithTag("CurrentScore").GetComponent<Text>();
                accelerationText        =  GameObject.FindWithTag("Acceleration").GetComponent<Text>();
            break;
            default :
            break;
        }
    }

    private void UIStart(){
        switch(type){
            case CanvasType.MainMenu :
                highScoreText.text  = ShowHighScore().ToString();
                isTutorialOpened    = false;
                tutorialPanel.SetActive(isTutorialOpened);
            break;
            case CanvasType.PlayScene :
                characterControllers = player.gameObject.GetComponent<CharacterControllers>();
                
                if(ShowAcceleration() != 0) characterControllers.acceleration = ShowAcceleration();
                else characterControllers.acceleration = 4f;
    
		        gameCamera1 = mainCamera.gameObject.GetComponent<CinemachineVirtualCamera>();
                gameCamera1.LookAt = characterControllers.transform;
                gameCamera1.Follow = characterControllers.transform;
            break;
            case CanvasType.ResultScene :
                currentScoreText.text   =  ShowCurrentScore().ToString();

                float fuzzyValue = FuzzyLogic.Instance.FuzzyTest(fuzzySetSpeed, fuzzySetScore, ShowLastSpeed(), ShowCurrentScore());
                // float fuzzyValue = FuzzyLogic.Instance.FuzzyTest(fuzzySetSpeed, fuzzySetScore, speedVal, scoreVal);
                int acc = AccelerationController.Instance.GetAccelerationLevel(fuzzyValue);
                if(acc > ShowAcceleration()){
                    arrow.sprite = arrowImage[0];
                }
                else if(acc < ShowAcceleration()){
                    arrow.sprite = arrowImage[1];
                }
                else{
                    arrow.sprite = arrowImage[2];
                }
                AddAcceleration(acc);
                UserDataManager.Save();
            break;
            default :
            break;
        }
    }

    private void UIUpdate(){
        if(type == CanvasType.SplashScreen){
            Loading();
            if(loadingSlider.value == 0) LoadScene("MainMenu");
        }
        else if(type == CanvasType.PlayScene){
            if(characterControllers.isDead != true){
                currentScoreText.text = currentScore.ToString();
                currentScore = (int)player.transform.position.z;
            }
        }
    }

    private void FixedUpdate() {
        if(type == CanvasType.ResultScene){
            score = Mathf.Clamp(Mathf.Lerp(0, ShowCurrentScore(), t), 0, ShowCurrentScore());
            t += 0.5f * Time.deltaTime;
            currentScoreText.text = ((int)score).ToString();
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
    
    public void PlayerDead(){
        if(deadCount < 1){
            deadCount++;
            characterControllers.Dead();

            AddLastScore(currentScore);
            AddCurrentScore(currentScore);
            AddHighScore(currentScore);
            AddLastSpeed(characterControllers.currentSpeed);
            
            StartCoroutine(DeadTime(characterDeadTime));
        }
    }

    public IEnumerator DeadTime(float time){
        yield return new WaitForSeconds(time);
        LoadScene("Result");
    }

    public void ChangeAccelerationText(string text){
        accelerationText.text = text;
    }

    public void OpenTutorial(){
        isTutorialOpened = !isTutorialOpened;
        tutorialPanel.SetActive(isTutorialOpened);
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

    public int ShowAcceleration(){
        return UserDataManager.Progress.Acceleration;
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

    public void AddCurrentScore(int value){
        UserDataManager.Progress.CurrentScore = value;
        UserDataManager.Save();
    }

    public void AddLastSpeed(float value){
        UserDataManager.Progress.LastSpeed = value;
        UserDataManager.Save();
    }
    public void AddLastScore(int value){
        UserDataManager.Progress.LastScore = value;
        UserDataManager.Save();
    }
    
    public void AddAcceleration(int value){
        UserDataManager.Progress.Acceleration = value;
        UserDataManager.Save();
    }
    #endregion
}