using UnityEngine;

public class AppearOnApproach : MonoBehaviour {

    SpriteRenderer spColor;
    private float aValue = 0;
    public float distance = 0f;

    public CharMovement player;
    public Transform approachPoint;
    
    void Start()
    {
        spColor = GetComponent<SpriteRenderer>();
    }

    void Update () {
        aValue = Vector3.Distance(player.transform.position, approachPoint.position)/distance;
        spColor.color = new Color(1, 1, 1, aValue);
	}
}
