using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuMusic : MonoBehaviour {

    AudioSource audioSrc;
    public float loopTime;
    public AudioClip loop;

    void Start () {
        audioSrc = GetComponent<AudioSource>();
        StartCoroutine(StartLoop());
	}
	
	IEnumerator StartLoop()
    {
        yield return new WaitForSecondsRealtime(loopTime);
        audioSrc.clip = loop;
        audioSrc.Play();
    }

}
