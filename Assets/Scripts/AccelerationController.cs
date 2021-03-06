using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AccelerationLevel{
    LOW,
    NORMAL,
    HIGH,
    VERYHIGH
}

public class AccelerationController : MonoBehaviour
{
    private static AccelerationController _instance;

    public static AccelerationController Instance{
        get{
            if(_instance == null){
                _instance = FindObjectOfType<AccelerationController>();
            }
            return _instance;
        }
    }

    [Header("ACCELERATION CONTROLLER")]
    #region ACCELERATION CONTROLLER
    public int lowAcceleration;
    public int normalAcceleration;
    public int highAcceleration;
    public int veryHighAcceleration;
    #endregion
    
    [Header("FSM CONTROLLER")]
    #region FSM CONTROLLER
    public float lowResult;
    public float normalResult;
    public float highResult;
    #endregion

    public int GetAccelerationLevel(float value){
        AccelerationLevel level = AccelerationLevel.NORMAL;

        if(value <= lowResult){
            level = AccelerationLevel.LOW;
        }
        else if(value <= normalResult){
            level = AccelerationLevel.NORMAL;
        }
        else if(value <= highResult){
            level = AccelerationLevel.HIGH;
        }
        else{
            level = AccelerationLevel.VERYHIGH;
        }

        return ChangeAccelerationLevel(level);
    }

    private int ChangeAccelerationLevel(AccelerationLevel level){
        if(level == AccelerationLevel.LOW){
            return lowAcceleration;
        }
        else if(level == AccelerationLevel.NORMAL){
            return normalAcceleration;
        }
        else if(level == AccelerationLevel.HIGH){
            return highAcceleration;
        }
        else{
            return veryHighAcceleration;
        }
    }
}