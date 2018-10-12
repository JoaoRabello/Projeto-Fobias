using UnityEngine;

public class Depth : MonoBehaviour {

    SpriteRenderer spRenderer;

    [SerializeField]
    private float m_floorHeight;
    private float m_spriteLowerBound;
    private float m_spriteHalfWidht;
    private readonly float m_tan30 = Mathf.Tan(Mathf.PI / 5);


	void Start () {
        spRenderer = GetComponent<SpriteRenderer>();
        m_spriteLowerBound = spRenderer.bounds.size.y * 0.5f;
        m_spriteHalfWidht = spRenderer.bounds.size.x * 0.5f;
	}


    void LateUpdate()
    {
        if (!Application.isEditor)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, (transform.position.y - m_spriteLowerBound + m_floorHeight) * m_tan30);
        }
    }


    void OnDrawGizmos()
    {
        Vector3 floorHeightPos = new Vector3(transform.position.x, (transform.position.y - m_spriteLowerBound + m_floorHeight), transform.position.z);

        Gizmos.color = Color.magenta;
        Gizmos.DrawLine((floorHeightPos + Vector3.left * m_spriteHalfWidht), (floorHeightPos + Vector3.right * m_spriteHalfWidht));
    }
}
