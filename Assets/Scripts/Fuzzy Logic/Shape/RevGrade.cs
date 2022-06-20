using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RevGrade : IShape
{
    float a;
    float b;

    public RevGrade(float a, float b){
        this.a = a;
        this.b = b;
    }

    public float Evaluate(float value){
        float result = 0;

        if(value <= a){
            result = 1;
        }
        else if(value >= b){
            result = 0;
        }
        else{
            result = (b - value)/(b - a);
        }
        return result;
    }
} 
