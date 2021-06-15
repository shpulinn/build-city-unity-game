using UnityEngine;

public class CameraController : MonoBehaviour
{
    // script of camera control

    public bool canControl = true; // public is for blocking camera control while building screen is on
    private Vector3 newPosition;
    [SerializeField] private float movementTime;
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
        if (canControl) {
            HandleMouseInput();
        }
    }

    private void HandleMouseInput() {
        if (Input.GetMouseButtonDown(0)) { // if press (touch) 1 time
            Plane plane = new Plane(Vector3.up, Vector3.zero);
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

            float entry;
            if (plane.Raycast(ray, out entry)) {
                drarPosStart = ray.GetPoint(entry);
            }
        }
        if (Input.GetMouseButton(0)) { // if hold press (touch)
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
