using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Shapes{
    public IShape shape;
    public Condition condition;

    public Shapes(IShape shape, Condition condition){
        this.shape = shape;
        this.condition = condition;
    }
}