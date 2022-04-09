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

    public float low = 0f;
    public float moderate = 0.5f;
    public float high = 1f;

    public float RulesCheck(Condition speed, Condition score){
        if(speed is Condition.LOW && score is Condition.LOW){
            return low;
        }
        else if(speed is Condition.LOW && score is Condition.MODERATE){
            return low;
        }
        else if(speed is Condition.LOW && score is Condition.HIGH){
            return low;
        }
        else if(speed is Condition.MODERATE && score is Condition.LOW){
            return low;
        }
        else if(speed is Condition.MODERATE && score is Condition.MODERATE){
            return moderate;
        }
        else if(speed is Condition.MODERATE && score is Condition.HIGH){
            return moderate;
        }
        else if(speed is Condition.HIGH && score is Condition.LOW){
            return moderate;
        }
        else if(speed is Condition.HIGH && score is Condition.MODERATE){
            return high;
        }
        else{
            return high;
        }
    }
}