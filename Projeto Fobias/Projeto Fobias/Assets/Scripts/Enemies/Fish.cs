using System.Collections;
using UnityEngine;
using EZCameraShake;

public class Fish : MonoBehaviour {

    private bool playerOnSight;
    private bool spawn = true;
    public float speed;
    private CharMovement player;

    private Animator anim;

    CameraShakeInstance camShakeInstance;

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
    
    private void Spawn()
    {
        playerOnSight = true;
        anim.SetBool("Spawn", true);
        StartCoroutine(MoveAnim());
        camShakeInstance = CameraShaker.Instance.ShakeOnce(2f, 0.3f, 0.15f, 0.15f);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Ariel") || col.gameObject.CompareTag("Clarice"))
        {
            if (spawn)
            {
                player = col.gameObject.GetComponent<CharMovement>();
                Spawn();
                spawn = false;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Ariel") || col.gameObject.CompareTag("Clarice"))
        {
            StartCoroutine(SetInactive());
        }
    }

    IEnumerator MoveAnim()
    {
        yield return new WaitForSecondsRealtime(0.8f);
        anim.SetBool("Move", true);
    }

    IEnumerator SetInactive()
    {
        yield return new WaitForSecondsRealtime(1.5f);
        playerOnSight = false;
        anim.SetBool("Spawn", false);
        yield return new WaitForSecondsRealtime(1f);
        gameObject.SetActive(false);
    }
}
