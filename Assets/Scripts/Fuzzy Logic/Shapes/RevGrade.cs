using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RevGrade{
    float start;
    float end;

    public RevGrade(float start, float end){
        this.start = start;
        this.end = end;
    }

    public float Evaluate(float value){
        float result = 0;
        var x = value;

        if(x <= this.start){
            result = 1;
        }
        else if(x >= this.end){
            result = 0;
        }
        else{
            result = (-x / (this.end - this.start)) + (this.start / (this.end - this.start));
        }
        return result;
    }
}