using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Counter : MonoBehaviour, IPointerDownHandler
{
    bool hasObject;
    GameController controller;

    void Start(){
        AddPhysics2DRaycaster();
        controller = GameObject.Find("Controller").GetComponent<GameController>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        //controller.removeCounterItem();
    }

    private void AddPhysics2DRaycaster()
    {
        Physics2DRaycaster physicsRaycaster = FindObjectOfType<Physics2DRaycaster>();
        if (physicsRaycaster == null)
        {
            Camera.main.gameObject.AddComponent<Physics2DRaycaster>();
        }
    }
}
