using UnityEngine;

public class Moth : MonoBehaviour {

    private float speed = 1.5f;
    private bool followPlayer = false;
    private GameObject player;
    [SerializeField] private GameObject moth;
    [SerializeField] private GameObject wall;
    [SerializeField] private Interruptor interruptor;
    [SerializeField] private GameObject lampada;
    [SerializeField] private Animator mothAnim;

    void Update () {

        if (followPlayer)
        {
            MoveAndAnime(player);
        }
        else
        {
            if (interruptor.GetAcesa())
            {
                MoveAndAnime(lampada);
            }
            else
            {
                MoveAndAnime(wall);
            }
        }  
	}

    private void MoveAndAnime(GameObject target)
    {
        moth.transform.position = Vector2.MoveTowards(new Vector2(moth.transform.position.x, moth.transform.position.y), target.transform.position, speed * Time.deltaTime);
        if(moth.transform.position != target.transform.position)
        {
            mothAnim.SetBool("Fly", true);
        }
        else
        {
            mothAnim.SetBool("Fly", false);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if((col.gameObject.CompareTag("Clarice") || col.gameObject.CompareTag("Ariel")) && interruptor.GetAcesa() == false)
        {
            followPlayer = true;
            player = col.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if ((col.gameObject.CompareTag("Clarice") || col.gameObject.CompareTag("Ariel")) && interruptor.GetAcesa() == false)
        {
            followPlayer = false;
        }
    }
}
