using UnityEngine;

public class closebuttonnewspaper : MonoBehaviour
{
    [SerializeField] GameObject NewsPane;
    public void OnPressed()
    {
        NewsPane.SetActive(false);
    }
}
