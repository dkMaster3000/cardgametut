using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardZoom : MonoBehaviour
{
    public GameObject Canvas;

    private GameObject zoomCard;

    public void Awake()
    {
        Canvas = GameObject.Find("Main Canvas");
    }

    public void OnHoverEnter()
    {
        //Input.mousePosition.x, Input.mousePosition.y + 250
        //gameObject.transform.position.x, gameObject.transform.position.y + 280
        zoomCard = Instantiate(gameObject, new Vector2(300, 500), Quaternion.identity);
        zoomCard.transform.SetParent(Canvas.transform, true);
        zoomCard.layer = LayerMask.NameToLayer("Zoom");


        RectTransform rect = zoomCard.GetComponent<RectTransform>();
        rect.sizeDelta = new Vector2(240, 344);

    }

    public void OnHoverExit()
    {
        Destroy(zoomCard);
    }
}
