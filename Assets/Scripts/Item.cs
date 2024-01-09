using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Misc;

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Item : MonoBehaviour, IPointerDownHandler
{
    GameController controller;
    GameObject infobox;
    TMPro.TextMeshPro infoText;
    public float price;
    SpriteRenderer spriteRenderer;
    public string itemName;
    public bool isDrugs;
    public string desc;
    TrackableValues stats;

    void Start()
    {
        AddPhysics2DRaycaster();
        controller = GameObject.Find("Controller").GetComponent<GameController>();
        stats = GameObject.Find("StatTracker").GetComponent<TrackableValues>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        infobox = transform.GetChild(0).gameObject;
        infoText = infobox.transform.GetChild(0).GetComponent<TMPro.TextMeshPro>();

        if (!isDrugs) {
            price = price + ((stats.DayNum - 1) * 2);
        }

        setDesc(desc);
    }

    // when item is clicked
    public void OnPointerDown(PointerEventData eventData)
    {
        controller.putItemOnCounter(this);
        stats.playSFX(3);
    }

    void OnMouseOver()
    {
        //If your mouse hovers over the GameObject with the script attached, output this message
        infobox.SetActive(true);
    }

    void OnMouseExit()
    {
        //The mouse is no longer hovering over the GameObject so output this message each frame
        infobox.SetActive(false);
    }

    private void AddPhysics2DRaycaster()
    {
        Physics2DRaycaster physicsRaycaster = FindObjectOfType<Physics2DRaycaster>();
        if (physicsRaycaster == null)
        {
            Camera.main.gameObject.AddComponent<Physics2DRaycaster>();
        }
    }

    public void setDesc(string d)
    {
        infoText.text = itemName + " \n£" + price.ToString("0.00") + "\n" + d;
        infobox.SetActive(false);
    }

    public void setPrice(float p)
    {
        price = p;
    }

    public float getPrice()
    {
        return price;
    }

    public void setSprite(Sprite sprite)
    {
        spriteRenderer.sprite = sprite;
    }

    public Sprite getSprite()
    {
        return spriteRenderer.sprite;
    }

    public void setName(string n)
    {
        itemName = n;
    }

    public string getName()
    {
        return itemName;
    }

    public bool getIsDrugs()
    {
        return isDrugs;
    }
}
