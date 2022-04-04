using System;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControllers : MonoBehaviour
{
	[Header("CHARACTER CONTROLLER")]
	public Positions currentPosition;
    private float currentXPosition = 0;

    public enum Positions{
        LEFT,
        CENTER,
        RIGHT,
    }

    public bool IsInvisible{ get; set; }
    public bool IsShielded{ get; set; }

    public bool isCutScene;

	[Header("ANDROID CONTROLLER")]


    [Header("MOVEMENT CONTROLLER")]
    public float initialSpeed; ///DEFAULT 20
    public float currentSpeed;
    public float CurrentSpeed{
        get{ return currentSpeed; }
        set{ currentSpeed = (float)Math.Round(Mathf.Clamp(value, initialSpeed, maxSpeed), 2); }
    }
    [SerializeField] private float maxSpeed = 100; ///DEFAULT 100
    [SerializeField] private float dodgeSpeed = 2f; ///DEFAULT 5
    [SerializeField] private float fixedPosition = 10f; ///DEFAULT 2

    public float acceleration = 1; ///DEFAULT 1
    public float Acceleration {
        get{ return acceleration; }
        set{ acceleration = (float)Math.Round(Mathf.Clamp(value, 1, 5), 2); }
    }
    [SerializeField] private int increaseSpeedModulo = 100; ///DEFAUL 100
    private float movingTransition = 0;

    [Header("CHARACTER DEAD")]
    [HideInInspector] public bool isDead = false;
    public GameObject deadExplotion;

    [Header("CHARACTER ANIMATION")]
    private Animator _animation;

    private void Start()
    {
        CurrentSpeed = initialSpeed;
        currentPosition = Positions.CENTER;
        _animation = GetComponent<Animator>();
	}

    private void Update() {
        if(isDead != true){
            MovementController();
        }
    }

    private void LateUpdate() {
        if((int)transform.position.z % increaseSpeedModulo == 0 &&
            (int)transform.position.z != 0 &&
            CurrentSpeed < maxSpeed &&
            isDead != true){
            IncreaseSpeed();
            increaseSpeedModulo *= 2;
        }
    }

    private void IncreaseSpeed(){
        CurrentSpeed += acceleration;
    }

    private void MovementController(){
    #if UNITY_EDITOR || UNITY_STANDALONE
        //USING KEYBOARD
        KeyboardMovement();
    #else
        ///USING ANDROID
        // AndroidMovement();
    #endif

        Vector3 moving = new Vector3((currentXPosition - transform.position.x) * dodgeSpeed, 0, currentSpeed);
        transform.Translate(moving * Time.deltaTime, Space.World);
    }

    private void KeyboardMovement(){
        if(Input.GetKeyDown("left")){
            if(currentPosition != Positions.LEFT){
                if(currentPosition == Positions.RIGHT){
                    _animation.Play("Turn Right");
                    currentPosition = Positions.CENTER;
                    currentXPosition = 0;
                }
                else if(currentPosition == Positions.CENTER){
                    _animation.Play("Turn Right");
                    currentPosition = Positions.LEFT;
                    currentXPosition = -fixedPosition;
                }
            }
        }
        else if(Input.GetKeyDown("right")){
            if(currentPosition != Positions.RIGHT){
                if(currentPosition == Positions.LEFT){
                    _animation.Play("Turn Left");
                    currentPosition = Positions.CENTER;
                    currentXPosition = 0;
                }
                else if(currentPosition == Positions.CENTER){
                    _animation.Play("Turn Left");
                    currentPosition = Positions.RIGHT;
                    currentXPosition = fixedPosition;
                }
            }
        }
    }

    public void Dead(){
        // Instantiate(deadExplotion, transform.position, transform.rotation);
        this.isDead = true;
    }

    private void OnCollisionEnter(Collision other){
        if(other.gameObject.tag == "Interactable"){
            other.gameObject.GetComponent<IInteractable>().Interaction();
        }
    }
}