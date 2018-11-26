using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class cutSceneEvent : UnityEvent
{
}

public class Cutscene1 : MonoBehaviour {

    [SerializeField] cutSceneEvent evento;
    public GameObject cutscene;
    public GameObject hud;
    private AudioSource[] allAudioSources;

    private void Awake()
    {
        StartCoroutine(Cutscene());
    }

    IEnumerator Cutscene()
    {
        hud.SetActive(false);
        yield return new WaitForSecondsRealtime(11f);
        hud.SetActive(true);
        evento.Invoke();
    }
}
