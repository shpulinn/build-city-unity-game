using System.Collections;
using UnityEngine;

public class Building : MonoBehaviour {

    public BuildingScriptableObj BuildingScriptableObj;
    [SerializeField] private ResourcesScriptableObj ResourcesScriptableObj;

    public Vector2Int Size = Vector2Int.one;

    private MeshRenderer MeshRenderer;
    private Color normalColor;
    private bool isBuilded = false;
    private bool inReload = false;

    private void Awake() {
        MeshRenderer = GetComponentInChildren<MeshRenderer>();
        normalColor = MeshRenderer.material.color;
    }

    private void Update() {
        if (!BuildingScriptableObj.type.Equals("decorative")) {
            if (isBuilded && !inReload) {
                StartCoroutine(MakeResourceCoroutine(BuildingScriptableObj));
            }
        }
    }

    public void ShowAvailable(bool available) {
        if (available) {
            MeshRenderer.material.color = Color.green;
        } else {
            MeshRenderer.material.color = Color.red;
        }
    }

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

    private IEnumerator BuildingCoroutine(float time, MeshRenderer mr) {
        float timeLeft = 0f;
        mr.material.color = Color.red;
        while (timeLeft < time) {
            timeLeft += Time.deltaTime;
            yield return null;
        }
        mr.material.color = normalColor;
        isBuilded = true;
    }

    private IEnumerator MakeResourceCoroutine(BuildingScriptableObj buildingScriptableObj) {
        inReload = true;
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
            default:
            break;
        }
        inReload = false;
    }

}
