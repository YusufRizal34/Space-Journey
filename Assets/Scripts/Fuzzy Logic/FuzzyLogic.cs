using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuzzyLogic : MonoBehaviour
{
    private static FuzzyLogic _instance;

    public static FuzzyLogic Instance{
        get{
            if(_instance == null){
                _instance = FindObjectOfType<FuzzyLogic>();
            }
            return _instance;
        }
    }

    List<Shapes> speedSets = new List<Shapes>();
    List<Shapes> scoreSets = new List<Shapes>();
    List<float> aPredicate = new List<float>();
    List<float> z = new List<float>();

    public float FuzzyTest(Shapes[] speedData, Shapes[] scoreData, float speed, float score){
        print(speed + "," + score);
        for(int i = 0; i < speedData.Length; i++){
            Fuzzyfication(speedSets, speedData[i], speed);
        }

        for(int i = 0; i < scoreData.Length; i++){
            Fuzzyfication(scoreSets, scoreData[i], score);
        }

        for(int i = 0; i < speedSets.Count; i++){
            for(int j = 0; j < scoreSets.Count; j++){
                aPredicate.Add(Implication(speedSets[i], scoreSets[j], speed, score));
                z.Add(Rules.Instance.Composition(speedSets[i].condition, scoreSets[j].condition));
                print(speedSets[i].condition + "," + scoreSets[j].condition);
            }
        }

        for(int i = 0; i < aPredicate.Count; i++){
            print(aPredicate[i] + "," + z[i]);
            // print(z[i]);
            // print(aPredicate[i] + "," + z[i]);
        }

        return Defuzzification(aPredicate, z);
    }

    public void Fuzzyfication(List<Shapes> varList, Shapes set, float value){
        if(value < set.shape.keys[0].time || value > set.shape.keys[set.shape.length - 1].time) return;
        else varList.Add(set);
    }

    public float Evaluation(AnimationCurve shape, float crisp){
        return shape.Evaluate(crisp);
    }

    public float Implication(Shapes shape1, Shapes shape2, float crisp1, float crisp2){
        float result = Mathf.Min(Evaluation(shape1.shape, crisp1), Evaluation(shape2.shape, crisp2));
        // print(Evaluation(shape1.shape, crisp1) + "," + Evaluation(shape2.shape, crisp2));
        return result;
    }

    public float Defuzzification(List<float> aPredicate, List<float> z){
        float numerator = 0;
        float denominator = 0;
        
        for(int i = 0; i < aPredicate.Count; i++){
            numerator += (aPredicate[i] * z[i]);
            denominator += aPredicate[i];
        }

        print(numerator + "/" + denominator);

        float result = numerator/denominator;
        return result;
    }
}

public enum Condition{
    LOW,
    MODERATE,
    HIGH,
}