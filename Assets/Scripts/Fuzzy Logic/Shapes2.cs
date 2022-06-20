using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Shapes2{
    public AnimationCurve shape;
    public Condition condition;

    public Shapes2(AnimationCurve shape, Condition condition){
        this.shape = shape;
        this.condition = condition;
    }
}