using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Triangle{
    float start;
    float center;
    float end;

    public Triangle(float start, float center, float end){
        this.start = start;
        this.center = center;
        this.end = end;
    }

    public float Evaluate(float value){
        float result = 0;
        var x = value;

        if(x <= this.start){
            result = 0;
        }
        else if(x >= this.center){
            result = 1;
        }
        else if((x > this.start) && (x <= this.center)){
            result = (x - this.start) / (this.center - this.start);
        }
        else if((x > this.center) && (x < this.end)){
            result = (this.end - x) / (this.end - this.center);
        }
        return result;
    }
}