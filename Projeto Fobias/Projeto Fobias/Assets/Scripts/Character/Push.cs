using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Push : MonoBehaviour {

    public float pushSpeed;

    public bool playerTouching;
    public bool playerPushing;
    Transform playerT;

    CharMovement player;

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.X))
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

        if(  ( ( Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow) ) ||
               ( Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) ) )
               && playerPushing)
        {
            player.Cansa();
            player.speed = pushSpeed;
            if (player.cansado == true)
            {
                UnChild();
            }
        }
	}

    void Childer()
    {
        transform.SetParent(playerT);
        playerPushing = true;
    }

    void UnChild()
    {
        player.speed = 2f;
        transform.SetParent(null);
        playerPushing = false;
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
