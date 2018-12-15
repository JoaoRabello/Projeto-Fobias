using UnityEngine;

public class CameraControl : MonoBehaviour {

    public Transform cameraPositionDestiny;
    public Transform target;
    GameObject cameraParent;
    Camera cam;
    public float size;

    public bool follow, isFollowArea;
    public float smoothSpeed = 10f;
    public Vector3 offset;

    private void Awake()
    {
        cameraParent = GameObject.FindGameObjectWithTag("Camera Parent");
        cam = FindObjectOfType<Camera>();
    }

    private void LateUpdate()
    {
        if (follow && isFollowArea)
        {
            Vector3 destinyPosition = target.position + offset;
            Vector3 smoothedPosition = Vector3.Lerp(cameraParent.transform.position, destinyPosition, smoothSpeed * Time.deltaTime);
            cameraParent.transform.position = smoothedPosition;
        }
    }

    public void Resize()
    {
        cam.orthographicSize = size;
    }

    public void GetTarget(Transform tgt)
    {
        target = tgt;
    }

    public void MoveCamera()
    {
        if (isFollowArea)
            cameraParent.transform.position = new Vector3(target.transform.position.x, target.transform.position.y, -10f);
        else
            cameraParent.transform.position = new Vector3(transform.position.x,transform.position.y, -10f);
    }
}
