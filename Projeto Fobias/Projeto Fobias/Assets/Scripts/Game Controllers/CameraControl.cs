using UnityEngine;

public class CameraControl : MonoBehaviour {

    public Transform cameraPositionDestiny;
    Camera cam;
    public float size;

    private void Awake()
    {
        cam = FindObjectOfType<Camera>();
    }

    public void Resize()
    {
        cam.orthographicSize = size;
    }

    public void MoveCamera()
    {
        cam.transform.position = new Vector3(transform.position.x,transform.position.y, -10f);
    }
}
