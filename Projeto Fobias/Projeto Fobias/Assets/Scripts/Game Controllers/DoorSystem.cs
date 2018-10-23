using System.Collections;
using UnityEngine;

public class DoorSystem : MonoBehaviour {

    public bool isOpen;
    public bool hasAnim;
    public GameObject doorDestiny;
    Animator anim;

    private void Start()
    {
        anim = GetComponentInParent<Animator>();
    }
    public bool GetIsOpen()
    {
        return isOpen;
    }

    public void DoorEnter(GameObject player)
    {
        StartCoroutine(WaitToOpen(player));
    }

    IEnumerator WaitToOpen(GameObject player)
    {
        if (hasAnim)
            anim.SetBool("Open", true);
        CharMovement playerMove = player.GetComponent<CharMovement>();
        playerMove.setCanMove(false);
        playerMove.moving = false;
        playerMove.speed = 2f;
        
        yield return new WaitForSecondsRealtime(0.5f);
        player.transform.position = doorDestiny.transform.position;
        playerMove.setCanMove(true);
        playerMove.moving = true;
        playerMove.speed = 2f;
    }
}
