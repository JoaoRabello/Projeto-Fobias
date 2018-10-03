using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Push : MonoBehaviour {

    public bool playerTouching;
    Transform playerT;

    CharMovement player;

	// Use this for initialization
	void Start () {
        player = FindObjectOfType<CharMovement>();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKey(KeyCode.S) && playerTouching)
        {
            //transform.SetParent(playerT);
            transform.Translate(player.GetDirection()*0.5f*Time.deltaTime);
        }
        else
        {
            transform.parent = null;
        }
	}

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Clarice") || col.gameObject.CompareTag("Ariel")){
            playerTouching = true;
            playerT = col.gameObject.transform;
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
