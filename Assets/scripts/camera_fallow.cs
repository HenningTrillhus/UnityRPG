using UnityEngine;

public class camera_fallow : MonoBehaviour
{
    public Transform target;   // Player to follow
    public float smoothSpeed = 0.125f;
    public Vector3 offset;     // Camera offset (usually 0,0,-10)

    public float cameraZoom = 3f;

    private void Start() {
        Camera.main.orthographicSize = cameraZoom; 
    }

    void FixedUpdate()
    {
        if (!StoreAreaLogic.Instance.playerNearby)
        {
            if (target == null) return;

            Vector3 desiredPosition = target.position + offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;
            cameraZoom = 3f;
            Camera.main.orthographicSize = cameraZoom;
        }
        if (StoreAreaLogic.Instance.playerNearby)
        {
            Vector3 desiredPosition = new Vector3(-10f, 20.5f, -10f);
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;
            cameraZoom = 3.5f;
            Camera.main.orthographicSize = cameraZoom;
        }
        
    }

    public void ZoomIn() {
        cameraZoom = Mathf.Max(1f, cameraZoom - 0.5f); // Prevent zooming in too much
        Camera.main.orthographicSize = cameraZoom;
    }

    public void ZoomOut() {
        cameraZoom += 0.5f;
        Camera.main.orthographicSize = cameraZoom;
    }
}
