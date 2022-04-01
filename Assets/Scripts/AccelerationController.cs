using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AccelerationLevel{
    VERYLOW,
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
    public float lowAcceleration;
    public float normalAcceleration;
    public float highAcceleration;
    public float veryHighAcceleration;
    #endregion
    
    [Header("FSM CONTROLLER")]
    #region FSM CONTROLLER
    public int lowResult;
    public int normalResult;
    public int highResult;
    #endregion

    public float GetAccelerationLevel(int value){
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
        else if(value > highResult){
            level = AccelerationLevel.VERYHIGH;
        }

        return ChangeAccelerationLevel(level);
    }

    private float ChangeAccelerationLevel(AccelerationLevel level){
        if(level == AccelerationLevel.LOW){
            return lowAcceleration;
        }
        else if(level == AccelerationLevel.NORMAL){
            return normalAcceleration;
        }
        else if(level == AccelerationLevel.HIGH){
            return highAcceleration;
        }
        else if(level == AccelerationLevel.VERYHIGH){
            return veryHighAcceleration;
        }

        return 0;
    }
}