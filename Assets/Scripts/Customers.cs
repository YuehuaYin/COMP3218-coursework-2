using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Customers : MonoBehaviour
{
    public abstract void text();
    public abstract void checkBarter(float sliderValue, string barterPriceText);
    public abstract void barterComplete();
    public abstract void barterFailed();
    public abstract void button1Clicked();
    public abstract void button2Clicked();
}
