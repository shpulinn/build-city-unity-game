using System.Collections;
using UnityEngine;

public class Building : MonoBehaviour {

    public BuildingScriptableObj BuildingScriptableObj;

    public Vector2Int Size = Vector2Int.one;

    private MeshRenderer MeshRenderer;

    private void Awake() {
        MeshRenderer = GetComponentInChildren<MeshRenderer>();
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
        float time = 0;
        MeshRenderer.material.color = new Color(100, 0, 0);
        for (int i = 0; i < BuildingScriptableObj.buildTime;) {
            IEnumerator Wait() {
                yield return new WaitForSeconds(1f);
                i++;
            }
            StartCoroutine(Wait());
            Debug.Log(i);
        }
        MeshRenderer.material.color = Color.white;
    }
}
