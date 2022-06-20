using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hermite : MonoBehaviour
{
    public AnimationCurve aPoint;
    public float value;

    void Start()
    {
        float i = Mathf.InverseLerp(aPoint.keys[0].time, aPoint.keys[1].time, value);
        float x = Evaluate(i, aPoint.keys[0], aPoint.keys[1]);
        print(aPoint.Evaluate(value));
        print("test : " + i + " and " + x);
    }

    float Evaluate(float t, Keyframe keyframe0, Keyframe keyframe1)
    {
        float dt = keyframe1.time - keyframe0.time;
        
        float m0 = keyframe0.outTangent * dt;
        float m1 = keyframe1.inTangent * dt;
    
        float t2 = t * t;
        float t3 = t2 * t;
        
        float a = 2 * t3 - 3 * t2 + 1;
        float b = t3 - 2 * t2 + t;
        float c = t3 - t2;
        float d = -2 * t3 + 3 * t2;
        
        return a * keyframe0.value + b * m0 + c * m1 + d * keyframe1.value;
    }
}
