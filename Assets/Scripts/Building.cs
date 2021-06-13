using UnityEngine;

public class Building : MonoBehaviour {

    public Vector2Int Size = Vector2Int.one;

    private void OnDrawGizmos() {
        for (int x = 0; x < Size.x; x++) {
            for (int y = 0; y < Size.y; y++) {
                Gizmos.color = new Color(1f, 0f, 0f, .3f);
                Gizmos.DrawCube(transform.position + new Vector3(x, -.5f, y), new Vector3(1, .1f, 1));
            }
        }
    }
}
