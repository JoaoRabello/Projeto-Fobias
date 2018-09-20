using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageList : MonoBehaviour {

    Image            sptRenderer;
    ItemImageSpawner item;
    public Sprite[]  imagens;
    int              counter = 0;

    private void Awake()
    {
        item = FindObjectOfType<ItemImageSpawner>();
    }

    private void Start()
    {
        sptRenderer = GetComponent<Image>();
    }

    void Update () {
		if(Input.GetKeyDown(KeyCode.Z) && ItemImageSpawner.imageSpawned)
        {
            ImageChange();
        }
	}

    void ImageChange()
    {
        if (counter < imagens.Length && imagens[counter] != null)
        {
            sptRenderer.sprite = imagens[counter];
            counter++;
        }
        else
        {
            item.ItemShow("close");
        }
    }
}
