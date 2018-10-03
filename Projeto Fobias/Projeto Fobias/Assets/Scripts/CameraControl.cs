using UnityEngine;

public class CameraControl : MonoBehaviour {

    public Transform cameraPositionDestiny;
    Camera cam;

    private void Awake()
    {
        cam = FindObjectOfType<Camera>();
    }

    public void MoveCamera()
    {
        cam.transform.position = new Vector3(transform.position.x,transform.position.y, -10f);
    }
}
