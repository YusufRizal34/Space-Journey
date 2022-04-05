using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CurvedWorld : MonoBehaviour {

    private static CurvedWorld _instance;

    public static CurvedWorld Instance{
        get{
            if(_instance == null){
                _instance = FindObjectOfType<CurvedWorld>();
            }
            return _instance;
        }
    }

    public Vector3 Curvature = new Vector3(0, 0, 0);
    public float Distance = 0;

    [Space]
    public float CurvatureScaleUnit = 1000f;
    
    int CurvatureID;
    int DistanceID;

    private void OnEnable()
    {
        CurvatureID = Shader.PropertyToID("_Curvature");
        DistanceID = Shader.PropertyToID("_Distance");
    }

    void Update()
    {
        Vector3 curvature = CurvatureScaleUnit == 0 ? Curvature : Curvature / CurvatureScaleUnit;

        Shader.SetGlobalVector(CurvatureID, curvature);
        Shader.SetGlobalFloat(DistanceID, Distance);
    }
}
