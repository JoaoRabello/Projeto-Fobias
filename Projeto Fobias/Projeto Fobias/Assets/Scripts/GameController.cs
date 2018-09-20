using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    //public Canvas pauseTextReference;
    public GameObject pauseTextInstance;
    bool onPause;

    private void Awake()
    {
        pauseTextInstance = GameObject.FindGameObjectWithTag("PauseWindow");
        pauseTextInstance.SetActive(false);
    }

    // Update is called once per frame
    void Update () {
        if ((Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape)) && onPause == false)
        {
            Pause();
        }
        else
        {
            if(Input.GetKeyDown(KeyCode.Escape) && onPause)
            {
                Unpause();
            }
        }
        
	}

    public void Pause()
    {
        Time.timeScale = 0;
        onPause = true;
        pauseTextInstance.gameObject.SetActive(true);
    }

    public void Unpause()
    {
        Time.timeScale = 1;
        onPause = false;
        pauseTextInstance.gameObject.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
