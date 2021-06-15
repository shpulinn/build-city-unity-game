using System.Collections.Generic;
using UnityEngine;

public class GridController : MonoBehaviour {

    // script generates field if tiles prefabs

    [SerializeField] private GameObject prefab;
    [SerializeField] private int rows;
    [SerializeField] private int cols;
    [SerializeField] private float tileSize;
    private List<GameObject> tiles = new List<GameObject>();

    void Start() {
        GenerateGrid();
    }

    private void GenerateGrid() {
        GameObject referenceTile = Instantiate(prefab);
        for (int row = 0; row < rows; row++) {
            for (int col = 0; col < cols; col++) {
                GameObject tile = Instantiate(referenceTile, transform);
                tiles.Add(tile);

                float posX = col * tileSize;
                float posZ = row * tileSize;

                tile.transform.position = new Vector3(posX, 0, posZ);
            }
        }
        Destroy(referenceTile);
    }
}
