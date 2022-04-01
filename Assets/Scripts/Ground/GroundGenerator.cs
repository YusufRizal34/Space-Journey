using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundGenerator : MonoBehaviour
{
    private static GroundGenerator _instance;
    public static GroundGenerator Instance{
        get{
            if(_instance == null){
                _instance = FindObjectOfType<GroundGenerator>();
            }
            return _instance;
        }
    }

    [Header("SPAWN CONTROLLER")]
    public Camera mainCamera;
    public Transform startPoint;
    public float zStartPoint = 10f; ///DEFAULT 10
    public float yStartPoint = 5f; ///DEFAULT 10

    [Header("SPAWN GROUND")]
    public PlatformTile[] tilePrefab;
    public PlatformTile[] earlyTilePrefab;
    List<PlatformTile> spawnedTiles = new List<PlatformTile>();
    public int maxTile = 3; ///DEFAULT 3

    private void Awake() {
        mainCamera = FindObjectOfType<Camera>();
    }

    void Start()
    {
        startPoint = GameObject.FindWithTag("Player").transform;
        Vector3 spawnPosition = startPoint.position - new Vector3(0, yStartPoint, zStartPoint);

        //SPAWN EARLY TILE FIRST
        for (int i = 0; i < earlyTilePrefab.Length; i++) {
            spawnPosition -= earlyTilePrefab[i].startPoint.localPosition;
            PlatformTile spawnedTile = Instantiate(earlyTilePrefab[i], spawnPosition, Quaternion.identity) as PlatformTile;
            spawnPosition = spawnedTile.endPoint.position;
            spawnedTile.transform.SetParent(transform);
        }

        //SPAWN NEXT TILE
        for (int i = 0; i < tilePrefab.Length; i++) {
            spawnPosition -= tilePrefab[i].startPoint.localPosition;
            PlatformTile spawnedTile = Instantiate(tilePrefab[i], spawnPosition, Quaternion.identity) as PlatformTile;
            spawnPosition = spawnedTile.endPoint.position;
            spawnedTile.transform.SetParent(transform);
            if(i > maxTile){
                spawnedTile.gameObject.SetActive(false);
            }
            spawnedTiles.Add(spawnedTile);
        }
    }

    void Update()
    {
        //IF CAMERA POINT(ENDPOINT ON CURRENT TILE) NEAR 0
        if(mainCamera.WorldToViewportPoint(spawnedTiles[0].endPoint.position).z < 0) {
            PlatformTile tileTmp = spawnedTiles[0];
            spawnedTiles[maxTile].gameObject.SetActive(true);
            spawnedTiles.RemoveAt(0);
            SpawnTile(tileTmp);
            
            ///ADD TILE TO LIST spawnedTiles
            spawnedTiles.Add(tileTmp);
        }
    }

    void SpawnTile(PlatformTile tileTmp){
        // tileTmp.ChangeObjectRotation();
        tileTmp.gameObject.SetActive(false);
        tileTmp.transform.position = spawnedTiles[spawnedTiles.Count - 1].endPoint.position - tileTmp.startPoint.localPosition;
        // tileTmp.SpawnObject();
    }
}