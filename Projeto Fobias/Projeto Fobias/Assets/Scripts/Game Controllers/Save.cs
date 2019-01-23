using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Save : MonoBehaviour {

    [SerializeField] DoorSystem door1;
    [SerializeField] DoorSystem door2;
    private int door1Status;
    private int door2Status;

    [SerializeField] CharMovement player;
    [SerializeField] GameController gc;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void SaveStatus()
    {
        if (door1.GetIsLocked())
            door1Status = 1;
        else
            door1Status = 0;

        if (door2.GetIsLocked())
            door2Status = 1;
        else
            door2Status = 0;

        PlayerPrefs.SetInt("door1", door1Status);
        PlayerPrefs.SetInt("door2", door2Status);

        player = gc.GetActiveCharGO();
        PlayerPrefs.SetFloat("PlayerX", player.transform.position.x);
        PlayerPrefs.SetFloat("PlayerY", player.transform.position.y);
    }

    public void LoadStatus()
    {
        player = gc.GetActiveCharGO();
        player.transform.position = new Vector2(PlayerPrefs.GetFloat("PlayerX"), PlayerPrefs.GetFloat("PlayerY"));
    }
}
