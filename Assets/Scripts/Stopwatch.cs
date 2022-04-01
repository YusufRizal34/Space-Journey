using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stopwatch : MonoBehaviour{
    private static Stopwatch _instance;

    public static Stopwatch Instance{
        get{
            if(_instance == null){
                _instance = FindObjectOfType<Stopwatch>();
            }
            return _instance;
        }
    }

    bool stopwatchActive = false;
    float currentTime;
    public Text timeText;

    private void Start(){
        currentTime = 0;
    }

    private void Update(){
        if(stopwatchActive == true){
            currentTime += Time.deltaTime;
        }
        TimeSpan time = TimeSpan.FromSeconds(currentTime);
        timeText.text = time.ToString(@"mm\:ss");
    }

    public void StartStopwatch(){
        stopwatchActive = true;
    }

    public void EndStopwatch(){
        stopwatchActive = false;
    }

    public float GetTime(){
        return currentTime;
    }
}