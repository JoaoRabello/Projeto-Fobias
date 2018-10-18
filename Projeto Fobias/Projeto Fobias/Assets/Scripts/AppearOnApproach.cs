using UnityEngine;

public class AppearOnApproach : MonoBehaviour {

    SpriteRenderer spColor;
    public float aValue = 0;

    public CharMovement player;
    public Transform approachPoint;
    
    void Start()
    {
        spColor = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update () {
        aValue = Vector3.Distance(player.transform.position, approachPoint.position)/3;
        spColor.color = new Color(1, 1, 1, aValue);
	}
}
