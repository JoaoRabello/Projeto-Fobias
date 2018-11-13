using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour {

    private bool playerOnSight;
    public float speed;
    private CharMovement player;

    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update () {
        if (playerOnSight)
        {
            Animate();
            transform.position = Vector2.MoveTowards(new Vector2(transform.position.x, transform.position.y), player.transform.position, speed * Time.deltaTime);
        }
	}

    private void Animate()
    {
        if (player.transform.position.x >= transform.position.x)
        {
            anim.SetFloat("x", 1);
        }
        else
        {
            anim.SetFloat("x", -1);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Ariel") || col.gameObject.CompareTag("Clarice"))
        {
            playerOnSight = true;
            anim.SetBool("Spawn", true);
            player = col.gameObject.GetComponent<CharMovement>();
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Ariel") || col.gameObject.CompareTag("Clarice"))
        {
            playerOnSight = false;
            anim.SetBool("Spawn", false);
            StartCoroutine(SetInactive());
        }
    }

    IEnumerator SetInactive()
    {
        yield return new WaitForSecondsRealtime(1f);
        gameObject.SetActive(false);
    }
}
