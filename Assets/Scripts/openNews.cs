using UnityEngine;
using UnityEngine.EventSystems;

public class openNews : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] GameObject newsPanel;
    public void OnPointerDown(PointerEventData eventData)
    {
        newsPanel.SetActive(true);
    }
}