using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rules : MonoBehaviour
{
    private static Rules _instance;

    public static Rules Instance{
        get{
            if(_instance == null){
                _instance = FindObjectOfType<Rules>();
            }
            return _instance;
        }
    }

    public float RulesCheck(Condition speed, Condition score){
        if(speed is Condition.LOW && score is Condition.LOW){
            return 0.3f;
        }
        else if(speed is Condition.LOW && score is Condition.MODERATE){
            return 0.3f;
        }
        else if(speed is Condition.LOW && score is Condition.HIGH){
            return 0.3f;
        }
        else if(speed is Condition.MODERATE && score is Condition.LOW){
            return 0.3f;
        }
        else if(speed is Condition.MODERATE && score is Condition.MODERATE){
            return 0.5f;
        }
        else if(speed is Condition.MODERATE && score is Condition.HIGH){
            return 0.5f;
        }
        else if(speed is Condition.HIGH && score is Condition.LOW){
            return 0.5f;
        }
        else if(speed is Condition.HIGH && score is Condition.MODERATE){
            return 0.9f;
        }
        else{
            return 0.9f;
        }
    }
}