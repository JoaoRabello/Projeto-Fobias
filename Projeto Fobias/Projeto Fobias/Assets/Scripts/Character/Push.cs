using UnityEngine;

public class Push : MonoBehaviour {

    public float pushSpeed;

    public bool playerTouching;
    public bool playerPushing;
    Transform playerT;

    CharMovement player;

	void Update () {

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        bool xDownKey = Input.GetKeyDown(KeyCode.X);
        bool joyInteractDownKey = Input.GetKeyDown("joystick button 1");

        if (xDownKey || joyInteractDownKey)
        {
            if (playerPushing == false && playerTouching)
            {
                Childer();
            }
            else
            {
                if (playerPushing)
                {
                    UnChild();
                }
            }      
        }

        if((horizontal != 0 || vertical != 0 ) && playerPushing)
        {
            player.podeCorrer = false;
            player.Cansa();
            player.speed = pushSpeed;
            if (player.cansado == true)
            {
                UnChild();
                player.podeCorrer = true;
            }
        }
	}

    void Childer()
    {
        transform.SetParent(playerT);
        playerPushing = true;
        player.GetComponent<Animator>().SetBool("pushing", true);
    }

    public void UnChild()
    {
        player.speed = 2f;
        transform.SetParent(null);
        playerPushing = false;
        player.GetComponent<Animator>().SetBool("pushing", false);

    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Clarice") || col.gameObject.CompareTag("Ariel")){
            playerTouching = true;
            playerT = col.gameObject.transform;
            player = col.gameObject.GetComponent<CharMovement>();
        }
    }

    private void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Clarice") || col.gameObject.CompareTag("Ariel"))
        {
            playerTouching = false;
        }
    }
}
