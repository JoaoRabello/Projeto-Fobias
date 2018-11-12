using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemImageSpawner : MonoBehaviour {

    [HideInInspector]
    public bool canImageSpawn;
    public static bool imageSpawned;
    bool guardianSpawned;

    public Canvas canvasWithImage;
	

	void Update () {
        if (Input.GetKeyDown(KeyCode.X) && canImageSpawn && imageSpawned == false)
        {
            ItemShow("open");
        }

        if (Input.GetKeyDown(KeyCode.Space) && imageSpawned)
        {
            ItemShow("close");    
        }
	}

    public void ItemShow(string op)
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
                break;
        }
    }
}
