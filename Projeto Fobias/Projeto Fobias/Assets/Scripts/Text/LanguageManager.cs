using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanguageManager : MonoBehaviour {

    [SerializeField] private int language;

    public int Language
    {
        get
        {
            return language;
        }

        set
        {
            language = value;
        }
    }

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}
