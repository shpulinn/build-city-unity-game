using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingsGrid : MonoBehaviour {

    public Vector2Int GridSize = new Vector2Int(30, 30);

    private Building[,] grid;
    private Building selectedBuilding;
    private Camera cam;
    private GameObject buildingScreen;
    private CameraController cameraController;
    private bool canPlace = true;

    private void Awake() {
        grid = new Building[GridSize.x, GridSize.y];
        
        cam = Camera.main;
        buildingScreen = GameObject.Find("BuildScreen");
        buildingScreen.gameObject.SetActive(false);
        cameraController = cam.GetComponent<CameraController>();
    }

    public void StartPlacingBuilding(Building buildingPrefab) {
        canPlace = false;
        if (selectedBuilding != null) {
            Destroy(selectedBuilding.gameObject);
        }

        selectedBuilding = Instantiate(buildingPrefab);
        if (buildingScreen != null) {
            buildingScreen.gameObject.SetActive(false);
            cameraController.canControl = true;
        }
    }

    void Start() {

    }

    void Update() {
        if (selectedBuilding != null) {
            canPlace = true;
            var groundPlane = new Plane(Vector3.up, Vector3.zero);
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

            if (groundPlane.Raycast(ray, out float position)) {
                Vector3 worldPos = ray.GetPoint(position);

                int x = Mathf.RoundToInt(worldPos.x);
                int z = Mathf.RoundToInt(worldPos.z);

                //bool canPlace = true;

                if (x < 0 || x > GridSize.x - selectedBuilding.Size.x) canPlace = false;
                if (z < 0 || z > GridSize.x - selectedBuilding.Size.y) canPlace = false;

                selectedBuilding.transform.position = new Vector3(x, 0, z);

                if (Input.GetMouseButtonDown(0) && canPlace) {
                    selectedBuilding = null;
                }
            }
        }
    }
}
