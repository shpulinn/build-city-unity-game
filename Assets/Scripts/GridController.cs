using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridController : MonoBehaviour {

    [SerializeField] private GameObject prefab;
    [SerializeField] private int rows;
    [SerializeField] private int cols;
    [SerializeField] private float tileSize;
    private List<GameObject> tiles = new List<GameObject>();

    void Start() {
        GenerateGrid();
    }

    private void GenerateGrid() {
        GameObject referenceTile = (GameObject)Instantiate(prefab);
        for (int row = 0; row < rows; row++) {
            for (int col = 0; col < cols; col++) {
                GameObject tile = (GameObject)Instantiate(referenceTile, transform);
                tiles.Add(tile);

                float posX = col * tileSize;
                float posZ = row * tileSize;

                tile.transform.position = new Vector3(posX, 0, posZ);
            }
        }
        Destroy(referenceTile);
    }

    void Update() {

    }
}
