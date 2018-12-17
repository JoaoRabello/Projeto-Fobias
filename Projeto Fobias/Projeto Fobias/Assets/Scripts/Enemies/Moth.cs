using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class Som : UnityEvent
{

}

public class Moth : MonoBehaviour {

    private float speed = 1.5f;
    private bool followPlayer = false;
    private bool firstFollow = true;
    private GameObject playerGO;
    private CharMovement player;

    [SerializeField] private Collider2D invisibleWall;

    [SerializeField] private GameObject moth;
    [SerializeField] private GameObject wall;
    [SerializeField] private Interruptor interruptor;
    [SerializeField] private GameObject lampada;
    [SerializeField] private Animator mothAnim;
    [SerializeField] private Som iniciaVoaSom;
    [SerializeField] private Som paraVoaSom;
    [SerializeField] private Som loopVoaSom;

    void Update () {

        if (followPlayer)
        {
            MoveAndAnime(playerGO);
            if(interruptor.GetAcesa() == false)
                player.EntraEmPanico(0.01f);
        }
        else
        {
            if (interruptor.GetAcesa())
            {
                MoveAndAnime(lampada);
                invisibleWall.enabled = false;
            }
            else
            {
                MoveAndAnime(wall);
                invisibleWall.enabled = true;
            }
        }  
	}

    private void MoveAndAnime(GameObject target)
    {
        moth.transform.position = Vector2.MoveTowards(new Vector2(moth.transform.position.x, moth.transform.position.y), target.transform.position, speed * Time.deltaTime);
        if(moth.transform.position != target.transform.position)
        {
            mothAnim.SetBool("Fly", true);
            loopVoaSom.Invoke();
        }
        else
        {
            mothAnim.SetBool("Fly", false);
            paraVoaSom.Invoke();
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if((col.gameObject.CompareTag("Clarice") || col.gameObject.CompareTag("Ariel")) && interruptor.GetAcesa() == false)
        {
            followPlayer = true;
            playerGO = col.gameObject;
            player = col.gameObject.GetComponent<CharMovement>();
            if (firstFollow)
            {
                iniciaVoaSom.Invoke();
                firstFollow = false;
            }
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
