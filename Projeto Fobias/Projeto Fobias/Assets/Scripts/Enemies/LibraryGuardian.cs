using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LibraryGuardian : MonoBehaviour {

    float speed;
    bool isSpawned;
    GameObject player;

	// Use this for initialization
	void Start () {

        player = GameObject.FindGameObjectWithTag("Clarice");
        speed = 2f;

    }
	
	// Update is called once per frame
	void Update () {

        if (isSpawned)
        {
            FollowPlayer();
        }

	}

    public void OnNotify()
    {
        isSpawned = true;
    }

    public void FollowPlayer()
    {
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
    }
}
