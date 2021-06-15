using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingsGrid : MonoBehaviour {

    public Vector2Int GridSize = new Vector2Int(30, 30);

    [SerializeField] private ResourcesScriptableObj ResourcesScriptableObj;
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
        if (ResourcesScriptableObj.money < buildingPrefab.gameObject.GetComponent<Building>().BuildingScriptableObj.moneyCost) {
            Debug.LogError("Now enougn money!");
            return;
        }
        else if (ResourcesScriptableObj.wood < buildingPrefab.gameObject.GetComponent<Building>().BuildingScriptableObj.woodCost) {
            Debug.LogError("Now enougn wood!");
            return;
        }
        else if (ResourcesScriptableObj.bricks < buildingPrefab.gameObject.GetComponent<Building>().BuildingScriptableObj.brickCost) {
            Debug.LogError("Now enougn bricks!");
            return;
        }
        else {
            selectedBuilding = Instantiate(buildingPrefab);
            if (buildingScreen != null) {
                buildingScreen.gameObject.SetActive(false);
                cameraController.canControl = true;
            }
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

                if (canPlace && IsTileTaken(x, z)) canPlace = false;

                selectedBuilding.transform.position = new Vector3(x, 0, z);
                selectedBuilding.ShowAvailable(canPlace);

                if (Input.GetMouseButtonDown(0) && canPlace) {
                    SetSelectedBuilding(x, z);
                }
            }
        }
    }

    private bool IsTileTaken(int placeX, int placeZ) {
        for (int x = 0; x < selectedBuilding.Size.x; x++) {
            for (int z = 0; z < selectedBuilding.Size.y; z++) {
                if (grid[placeX + x, placeZ + z] != null) return true;
            }
        }
        return false;
    }

    private void SetSelectedBuilding(int placeX, int placeZ) {

        for (int x = 0; x < selectedBuilding.Size.x; x++) {
            for (int z = 0; z < selectedBuilding.Size.y; z++) {
                grid[placeX + x, placeZ + z] = selectedBuilding;
            }
        }

        selectedBuilding.gameObject.GetComponent<Building>().StartBuilding();
        ResourcesScriptableObj.money -= selectedBuilding.BuildingScriptableObj.moneyCost;
        ResourcesScriptableObj.wood -= selectedBuilding.BuildingScriptableObj.woodCost;
        ResourcesScriptableObj.bricks -= selectedBuilding.BuildingScriptableObj.brickCost;
        selectedBuilding = null;
    }
}
