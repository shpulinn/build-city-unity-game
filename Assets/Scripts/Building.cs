using System.Collections;
using UnityEngine;

public class Building : MonoBehaviour {

    // script is class of building

    public BuildingScriptableObj BuildingScriptableObj; // Building Scriptable Object
    [SerializeField] private ResourcesScriptableObj ResourcesScriptableObj; // Resources Scriptable Object

    public Vector2Int Size = Vector2Int.one;

    private MeshRenderer MeshRenderer;
    private Color[] normalColors = new Color[5];
    private bool isBuilded = false;
    private bool isProducing = false;

    private void Awake() {
        MeshRenderer = GetComponentInChildren<MeshRenderer>();
        for (int i = 0; i < MeshRenderer.materials.Length; i++) {
            normalColors[i] = MeshRenderer.materials[i].color;
        }
    }

    private void Update() {
        if (!BuildingScriptableObj.type.Equals("decorative")) {
            if (isBuilded && !isProducing) {
                StartCoroutine(MakeResourceCoroutine(BuildingScriptableObj));
            }
        }
    }

    // colorize house with green color if player can build that house in current place and with red color if it unable to build here
    public void ShowAvailable(bool available) {
        if (available) {
            foreach (Material mat in MeshRenderer.materials) {
                mat.color = Color.green;
            }
        } else {
            foreach (Material mat in MeshRenderer.materials) {
                mat.color = Color.red;
            }
        }
    }

    // gizmos for showing tiles below building
    private void OnDrawGizmos() {
        for (int x = 0; x < Size.x; x++) {
            for (int y = 0; y < Size.y; y++) {
                Gizmos.color = new Color(1f, 0f, 0f, .3f);
                Gizmos.DrawCube(transform.position + new Vector3(x, -.5f, y), new Vector3(1, .1f, 1));
            }
        }
    }

    public void StartBuilding() {
        StartCoroutine(BuildingCoroutine(BuildingScriptableObj.buildTime, MeshRenderer));        
    }

    // coroutine for visualising build process. while house is building, it colorize with red color. and when it built color retuns to in normal.
    private IEnumerator BuildingCoroutine(float time, MeshRenderer mr) {
        float timeLeft = 0f;
        for (int i = 0; i < normalColors.Length; i++) {
            mr.materials[i].color = Color.red;
        }
        while (timeLeft < time) {
            timeLeft += Time.deltaTime;
            yield return null;
        }
        for (int i = 0; i < normalColors.Length; i++) {
            mr.materials[i].color = normalColors[i];
        }
        isBuilded = true;
    }

    // coroutine for producing resources. each resource produces to each type of building with time, setted up in scriptable object.
    private IEnumerator MakeResourceCoroutine(BuildingScriptableObj buildingScriptableObj) {
        isProducing = true;
        yield return new WaitForSeconds(buildingScriptableObj.produceTime);
        switch (buildingScriptableObj.type) {
            case "money":
            ResourcesScriptableObj.money += buildingScriptableObj.amountOfResource;
            break;
            case "wood":
            ResourcesScriptableObj.wood += buildingScriptableObj.amountOfResource;
            break;
            case "brick":
            ResourcesScriptableObj.bricks += buildingScriptableObj.amountOfResource;
            break;
        }
        isProducing = false;
    }
}
