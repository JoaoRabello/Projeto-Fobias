using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moth : MonoBehaviour {

    private float speed = 1.5f;
    private bool followPlayer = false;
    private bool backToWall = false;
    private GameObject player;
    [SerializeField] private GameObject moth;
    [SerializeField] private GameObject wall;

    void Update () {
        if (followPlayer)
        {
            moth.transform.position = Vector2.MoveTowards(new Vector2(moth.transform.position.x, moth.transform.position.y), player.transform.position, speed * Time.deltaTime);
        }
        else
        {
            moth.transform.position = Vector2.MoveTowards(new Vector2(moth.transform.position.x, moth.transform.position.y), wall.transform.position, speed * Time.deltaTime);
        }
	}

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.CompareTag("Clarice") || col.gameObject.CompareTag("Ariel"))
        {
            followPlayer = true;
            backToWall = false;
            player = col.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Clarice") || col.gameObject.CompareTag("Ariel"))
        {
            followPlayer = false;
            backToWall = true;
        }
    }
}
