using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tag : MonoBehaviour {


    public enum Tags
    {
        livro,
        chave
    }


    //public Tags[] myTag;          //Multiple tags in one Tag script (tentar implementar depois)
    public Tags myTag;

    public Tags GetMyTag()
    {
        return myTag;
    }
}
