using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shapes{
    public AnimationCurve shape;
    public Condition condition;

    public Shapes(AnimationCurve shape, Condition condition){
        this.shape = shape;
        this.condition = condition;
    }
}