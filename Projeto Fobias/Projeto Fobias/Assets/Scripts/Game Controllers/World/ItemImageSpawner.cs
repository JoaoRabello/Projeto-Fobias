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
            Debug.Log("spawna imagem lua");
            ItemShow("open");
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.X) && imageSpawned)
            {
                Debug.Log("fecha imagem lua");
                ItemShow("close");
            }
        }
	}

    public void ItemShow(string op)
    {
        switch (op)
        {
            case "open":
                Instantiate(canvasWithImage);
                imageSpawned = true;
                canImageSpawn = false;
                Time.timeScale = 0;
                break;
            case "close":
                Debug.Log("destroi imagem lua");
                Destroy(GameObject.FindGameObjectWithTag("Item Image"));
                imageSpawned = false;
                canImageSpawn = false;
                Time.timeScale = 1;
                break;
        }
    }
}
