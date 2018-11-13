using System.Collections;
using UnityEngine;

public class DoorSystem : MonoBehaviour {

    public bool isLocked;
    public string keyName;

    public bool hasAnim;
    public GameObject doorDestiny;
    public GameObject chave;
    Animator anim;

    public Animator fadeAnim;

    private void Start()
    {
        anim = GetComponentInParent<Animator>();
    }

    public bool GetIsLocked()
    {
        return isLocked;
    }

    public string GetKeyName()
    {
        return keyName;
    }

    public void Unlock()
    {
        chave.SetActive(false);
        isLocked = false;
    }

    public void SetLock(bool value)
    {
        isLocked = value;
    }

    public void DoorEnter(GameObject player)
    {
        fadeAnim.SetTrigger("Fade");
        StartCoroutine(WaitToOpen(player));
    }

    IEnumerator WaitToOpen(GameObject player)
    {
        if (hasAnim)
            anim.SetBool("Open", true);
        CharMovement playerMove = player.GetComponent<CharMovement>();
        if (player.GetComponentInChildren<Push>())
        {
            player.GetComponentInChildren<Push>().UnChild();
        }
        playerMove.setCanMove(false);
        playerMove.moving = false;
        playerMove.speed = 2f;
        
        yield return new WaitForSecondsRealtime(0.5f);

        fadeAnim.SetTrigger("Fade");
        player.transform.position = doorDestiny.transform.position;
        playerMove.setCanMove(true);
        playerMove.moving = true;
        playerMove.speed = 2f;
    }
}
