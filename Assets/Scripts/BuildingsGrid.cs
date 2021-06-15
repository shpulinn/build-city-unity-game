using UnityEngine;

public class BuildingsGrid : MonoBehaviour {

    // script for generating grid of buildings and interaction player with grid (placing buildings)

    public Vector2Int GridSize = new Vector2Int(30, 30); // 30x30 grid

    [SerializeField] private ResourcesScriptableObj ResourcesScriptableObj;
    private Building[,] grid;
    private Building selectedBuilding; // building that player select in building screen
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

    // method for starting placing selected building; calls by button in build screen.
    public void StartPlacingBuilding(Building buildingPrefab) {
        canPlace = false;
        if (selectedBuilding != null) {
            Destroy(selectedBuilding.gameObject);
        }
        if (ResourcesScriptableObj.money < buildingPrefab.gameObject.GetComponent<Building>().BuildingScriptableObj.moneyCost) {
            Debug.LogError("Not enougn money!");
            return;
        }
        else if (ResourcesScriptableObj.wood < buildingPrefab.gameObject.GetComponent<Building>().BuildingScriptableObj.woodCost) {
            Debug.LogError("Not enougn wood!");
            return;
        }
        else if (ResourcesScriptableObj.bricks < buildingPrefab.gameObject.GetComponent<Building>().BuildingScriptableObj.brickCost) {
            Debug.LogError("Not enougn bricks!");
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

    void Update() {
        if (selectedBuilding != null) {
            canPlace = true;
            var groundPlane = new Plane(Vector3.up, Vector3.zero);
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

            if (groundPlane.Raycast(ray, out float position)) {
                Vector3 worldPos = ray.GetPoint(position);

                int x = Mathf.RoundToInt(worldPos.x);
                int z = Mathf.RoundToInt(worldPos.z);

                if (x < 0 || x > GridSize.x - selectedBuilding.Size.x) canPlace = false;
                if (z < 0 || z > GridSize.x - selectedBuilding.Size.y) canPlace = false;

                if (canPlace && IsTileTaken(x, z)) canPlace = false;

                selectedBuilding.transform.position = new Vector3(x, 0, z); // moving selected building with pointer (on PC)
                selectedBuilding.ShowAvailable(canPlace); 

                if (Input.GetMouseButtonDown(0) && !canPlace) {
                    Debug.LogError("Unable to build here");
                }

                if (Input.GetMouseButtonDown(0) && canPlace) {
                    SetSelectedBuilding(x, z);
                }
            }
        }
    }

    // return false if tile, where player want to build house is taken 
    private bool IsTileTaken(int placeX, int placeZ) {
        for (int x = 0; x < selectedBuilding.Size.x; x++) {
            for (int z = 0; z < selectedBuilding.Size.y; z++) {
                if (grid[placeX + x, placeZ + z] != null) return true;
            }
        }
        return false;
    }

    // adds building to the grid array and places building on the spot tha player pressed.
    private void SetSelectedBuilding(int placeX, int placeZ) {

        for (int x = 0; x < selectedBuilding.Size.x; x++) {
            for (int z = 0; z < selectedBuilding.Size.y; z++) {
                grid[placeX + x, placeZ + z] = selectedBuilding;
            }
        }

        selectedBuilding.gameObject.GetComponent<Building>().StartBuilding();
        // decrease player resources by the cost of current house
        ResourcesScriptableObj.money -= selectedBuilding.BuildingScriptableObj.moneyCost;
        ResourcesScriptableObj.wood -= selectedBuilding.BuildingScriptableObj.woodCost;
        ResourcesScriptableObj.bricks -= selectedBuilding.BuildingScriptableObj.brickCost;
        selectedBuilding = null;
    }
}
