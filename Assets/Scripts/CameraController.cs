using UnityEngine;

public class CameraController : MonoBehaviour
{

    public Vector3 newPosition;
    public float movementTime;
    private Camera cam;
    private Vector3 drarPosStart;
    private Vector3 drarPosCurrent;

    void Start()
    {
        cam = GetComponent<Camera>();
        newPosition = transform.position;
    }

    void Update()
    {
        HandleMouseInput();
    }

    private void HandleMouseInput() {
        if (Input.GetMouseButtonDown(0)) {
            Plane plane = new Plane(Vector3.up, Vector3.zero);
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

            float entry;
            if (plane.Raycast(ray, out entry)) {
                drarPosStart = ray.GetPoint(entry);
            }
        }
        if (Input.GetMouseButton(0)) {
            Plane plane = new Plane(Vector3.up, Vector3.zero);
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

            float entry;
            if (plane.Raycast(ray, out entry)) {
                drarPosCurrent = ray.GetPoint(entry);

                newPosition = transform.position + drarPosStart - drarPosCurrent;
            }
        }

        transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * movementTime);
    }
}
