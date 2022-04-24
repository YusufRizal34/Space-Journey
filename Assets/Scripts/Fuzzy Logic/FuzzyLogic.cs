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

    List<Shapes> fuzzySetSpeed = new List<Shapes>();
    List<Shapes> fuzzySetScore = new List<Shapes>();

    List<float> inferenceData = new List<float>();
    List<float> compositionData = new List<float>();

    public float FuzzyTest(Shapes[] speedSet, Shapes[] scoreSet, float speed, float score){
        ///FUZZIFIKASI
        for(int i = 0; i < speedSet.Length; i++){
            Fuzzyfication(fuzzySetSpeed, speedSet[i], speed);
        }

        for(int i = 0; i < speedSet.Length; i++){
            Fuzzyfication(fuzzySetScore, scoreSet[i], score);
        }

        ///INFERENSI + COMPOSITION
        for(int i = 0; i < fuzzySetSpeed.Count; i++){
            for(int j = 0; j < fuzzySetScore.Count; j++){
                inferenceData.Add(Inference(fuzzySetSpeed[i], fuzzySetScore[j], speed, score));
                compositionData.Add(Rules.Instance.RulesCheck(fuzzySetSpeed[i].condition, fuzzySetScore[j].condition));
            }
        }
        
        ///DEFUZZIFIKASI
        return Defuzzification(inferenceData, compositionData);
    }

    public void Fuzzyfication(List<Shapes> varList, Shapes set, float value){
        // if(value < set.shape.keys[0].time || value > set.shape.keys[set.shape.length - 1].time) return;
        // else varList.Add(set);
        if(set.shape.length > 2){
            if(value < set.shape.keys[0].time || value > set.shape.keys[2].time) return;
            else varList.Add(set);
        }
        else{
            if(value < set.shape.keys[0].time || value > set.shape.keys[1].time) return;
            else varList.Add(set);
        }
    }

    public float Implication(AnimationCurve shape, float crisp){
        return shape.Evaluate(crisp);
    }

    public float Inference(Shapes shape1, Shapes shape2, float crisp1, float crisp2){
        float result = Mathf.Min(Implication(shape1.shape, crisp1), Implication(shape2.shape, crisp2));
        return result;
    }

    public float Defuzzification(List<float> aPredicate, List<float> z){
        float result = 0;
        float numerator = 0;
        float denominator = 0;
        
        for(int i = 0; i < aPredicate.Count; i++){
            numerator += (aPredicate[i] * z[i]);
            denominator += aPredicate[i];
        }

        result = numerator/denominator;
        return result;
    }
}

public enum Condition{
    LOW,
    MODERATE,
    HIGH,
}