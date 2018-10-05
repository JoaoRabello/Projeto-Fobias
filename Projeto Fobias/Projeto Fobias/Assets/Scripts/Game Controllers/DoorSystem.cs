using UnityEngine;

public class DoorSystem : MonoBehaviour {

    public bool isOpen;
    public GameObject doorDestiny;

    public bool GetIsOpen()
    {
        return isOpen;
    }

    public void DoorEnter(GameObject player)
    {
        player.transform.position = doorDestiny.transform.position;
    }
}
