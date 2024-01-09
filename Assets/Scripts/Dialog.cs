using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialog : MonoBehaviour
{
    public GameObject dialogBox;
    RectTransform rectangle;

    // Start is called before the first frame update
    void Start()
    {
        rectangle = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void addDialog(string dialogText){
        if (transform.childCount >= 5)
        {
            DestroyImmediate(transform.GetChild(0).gameObject);
        }
        GameObject dialog = Instantiate(dialogBox, transform);
        dialog.transform.GetChild(0).gameObject.GetComponent<TMPro.TextMeshProUGUI>().text = dialogText;
    }

    public void clearBox(){
        if (transform.childCount > 0){
            while (transform.childCount > 0) {
                DestroyImmediate(transform.GetChild(0).gameObject);
            }
            rectangle.sizeDelta = new Vector2 (176, 0);
        }
    }

}
