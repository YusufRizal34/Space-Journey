using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grade : IShape
{
    float a;
    float b;

    public Grade(float a, float b){
        this.a = a;
        this.b = b;
    }

    public float Evaluate(float value){
        float result = 0;

        if(value <= a){
            result = 0;
        }
        else if(value >= b){
            result = 1;
        }
        else{
            result = (value-a)/(b-a);
        }
        return result;
    }
} 
