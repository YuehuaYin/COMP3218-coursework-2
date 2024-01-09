using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bartering : MonoBehaviour
{
    GameController controller;
    Slider slider;
    TMPro.TextMeshProUGUI barterPriceText;
    float barterPrice;
    float itemSalePrice;
    int timesFailed = 0;

    // Start is called before the first frame update
    void Start()
    {
        controller = GameObject.Find("Controller").GetComponent<GameController>();
        slider = transform.GetChild(0).gameObject.GetComponent<Slider>();
        barterPriceText = transform.GetChild(1).gameObject.GetComponent<TMPro.TextMeshProUGUI>();
    }

    public void showSlider(float price){
        itemSalePrice = price;
        barterPriceText.text = price.ToString("0.00");
        slider.onValueChanged.AddListener((v) => {
            barterPriceText.text = (price * v).ToString("0.00");
        });
    }

    public void resetSlider(){
        slider.value = 1;
    }

    public void OKbuttonPressed(){
        controller.checkBarter(slider.value, barterPriceText.text);
    }
}
