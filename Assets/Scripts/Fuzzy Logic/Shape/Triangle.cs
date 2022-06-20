using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Triangle : IShape
{
    float a, b, c;

    public Triangle(float a, float b, float c){
        this.a = a;
        this.b = b;
        this.c = c;
    }

    public float Evaluate(float value){
        float result = 0;

        if(value <= a || value >= c){
            result = 0;
        }
        else if((a < value) && (value < b)){
            result = (value - a) / (b - a);
        }
        else if((b < value) && (value < c)){
            result = (c - value) / (c - b);
        }
        return result;
    }
} 
