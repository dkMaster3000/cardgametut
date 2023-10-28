using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class YardZoom : MonoBehaviour
{

    public GameObject DisplayArea;
    public GameObject ZoomCard;

    private GameObject zoomCard;

    void Start()
    {
        DisplayArea = GameObject.Find("DisplayArea");
    }

    public void OnHoverEnter()
    {
        if (transform.childCount != 0)
        {
            foreach (Transform child in transform)
            {
                zoomCard = Instantiate(ZoomCard, new Vector2(0, 0), Quaternion.identity);
                zoomCard.transform.SetParent(DisplayArea.transform, false);

                zoomCard.GetComponent<Image>().sprite = child.GetComponent<Image>().sprite;
            }
        }
    }

    public void OnHoverExit()
    {
        if (DisplayArea.transform.childCount != 0)
        {
            foreach (Transform child in DisplayArea.transform)
            {
                Destroy(child.gameObject);
            }
        }
    }
}