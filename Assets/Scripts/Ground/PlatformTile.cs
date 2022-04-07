using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformTile : MonoBehaviour
{
    public Transform startPoint;
    public Transform endPoint;

    public GameObject obstacle1;
    public GameObject obstacle2;

    public Transform[] obstacle1Position;
    public Transform[] obstacle2Position;

    public void SpawnObstacle(GameObject obstacle, Transform[] obstaclePositions){
        obstacle.transform.position = obstaclePositions[Random.Range(0, obstaclePositions.Length)].position;
    }
}