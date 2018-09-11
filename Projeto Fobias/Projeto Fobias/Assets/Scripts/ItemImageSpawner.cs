using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemImageSpawner : MonoBehaviour {

    [HideInInspector]
    public bool canImageSpawn;
    bool imageSpawned;
    bool guardianSpawned;

    public Canvas canvasWithImage;

    public LibraryGuardian guardian;
    public Transform guardianPosition;
	

	void Update () {
        if (Input.GetKeyDown(KeyCode.C) && canImageSpawn && imageSpawned == false)
        {
            ItemShow("open");
        }

        if (Input.GetKeyDown(KeyCode.Escape) && imageSpawned)
        {
            ItemShow("close");    
        }
	}

    void ItemShow(string op)
    {
        switch (op)
        {
            case "open":
                Instantiate(canvasWithImage);
                imageSpawned = true;
                Time.timeScale = 0;
                break;
            case "close":
                Destroy(GameObject.FindGameObjectWithTag("Item Image"));
                imageSpawned = false;
                Time.timeScale = 1;
                if (guardianSpawned == false)
                {
                    guardian = Instantiate(guardian, guardianPosition);
                    guardianSpawned = true;
                    guardian.OnNotify();
                }
                break;
        }
    }
}
