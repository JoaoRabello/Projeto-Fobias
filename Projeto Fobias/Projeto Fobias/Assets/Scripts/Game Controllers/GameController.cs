using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

    public GameObject pauseTextInstance;
    bool onPause;

    private AudioSource[] allAudioSources;

    public CharMovement arielGO;
    public CharMovement clariceGO;

    [SerializeField] int activeChar = 0;

    private void Awake()
    {
        pauseTextInstance.SetActive(false);
    }

    private void Start()
    {
        allAudioSources = FindObjectsOfType<AudioSource>() as AudioSource[];
    }

    void Update () {

        bool pKeyDown = Input.GetKeyDown(KeyCode.P);
        bool escKeyDown = Input.GetKeyDown(KeyCode.Escape);
        bool pauseKeyDown = Input.GetKeyDown("joystick button 9");

        if ((pKeyDown || escKeyDown || pauseKeyDown) && onPause == false)
        {
            Pause();
        }
        else
        {
            if((escKeyDown || pauseKeyDown) && onPause)
            {
                Unpause();
            }
        }
        
	}

    public int GetActiveChar()
    {
        return activeChar;
    }

    public CharMovement GetActiveCharGO()
    {
        if(GetActiveChar() == 0)
        {
            return arielGO;
        }
        else
        {
            if(GetActiveChar() == 2)
            {
                return clariceGO;
            }
        }
        return arielGO;
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

    public void EndGame()
    {
        StartCoroutine(WaitForCredits());
        
    }

    public void PauseAllAudio()
    {
        foreach (AudioSource audioS in allAudioSources)
        {
            audioS.Pause();
        }
    }

    void UnpauseAllAudio()
    {
        foreach (AudioSource audioS in allAudioSources)
        {
            audioS.UnPause();
        }
    }


    IEnumerator WaitForCredits()
    {
        yield return new WaitForSecondsRealtime(5f);
        SceneManager.LoadScene(2);
    }
}
