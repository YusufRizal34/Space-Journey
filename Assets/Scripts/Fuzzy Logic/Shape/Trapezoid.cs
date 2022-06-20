using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trapezoid : IShape
{
    float a, b, c, d;

    public Trapezoid(float a, float b, float c, float d){
        this.a = a;
        this.b = b;
        this.c = c;
        this.d = d;
    }

    public float Evaluate(float value){
        float result = 0;

        if(value <= a || value >= d){
            result = 0;
        }
        else if(value > a && value < b){
            result = (value - a) / (b - a);
        }
        else if(value >= b && value <= c){
            result = 1;
        }
        else if(value > c || value < d){
            result = (d - value) / (d - c);
        }
        return result;
    }
}
