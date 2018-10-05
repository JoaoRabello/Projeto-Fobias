using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    public GameObject pauseTextInstance;
    bool onPause;

    int activeChar = 0;

    private void Awake()
    {
        pauseTextInstance = GameObject.FindGameObjectWithTag("PauseWindow");
        pauseTextInstance.SetActive(false);
    }

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

    public int GetActiveChar()
    {
        return activeChar;
    }

    public void SetActiveChar(int activeChar)
    {
        this.activeChar = activeChar;
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
