using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class illWoman : Customers
{
    GameController controller;
    Sprite sprite;
    int dialogCounter = 0;
    int illWomanLevel;
    TrackableValues stats;
    bool willPay;

    public illWoman(){
        controller = GameObject.Find("Controller").GetComponent<GameController>();
        stats = GameObject.Find("StatTracker").GetComponent<TrackableValues>();
        illWomanLevel = stats.getIllWomanLevel();

        willPay = false;

        //0 = intro, 1 = happy, 2 = poor, 3 = child dies
        if (illWomanLevel == 2)
        {
            sprite = Resources.Load<Sprite>("Sprites/scriptedNPCs/illwomanpoor");
            controller.setCustomerSprite(sprite);
        }
        else { 
            sprite = Resources.Load<Sprite>("Sprites/scriptedNPCs/illwoman");
            controller.setCustomerSprite(sprite);
        }
    }

     public override void text()
    {
        controller.clearDialogBox();
        if (illWomanLevel == 0)
        {
            if (dialogCounter == 0)
            {
                controller.addDialog(new string[] { "Hello. I need to buy some cocaine." , "My child has a very rare illness, it is the only thing that will help.", "Medical bills are expensive, I only have £10.", "Please help my child."});
                controller.button2SetText("That's too low.");
            }
            else if (dialogCounter == 1)
            {
                controller.addDialog(new string[] { "My child will die without this drug.", "Please reconsider!" });
                controller.button2SetText("Give me more money.");
            }
            else if (dialogCounter == 2)
            {
                controller.addDialog(new string[] { "I'm so desperate for this drug...", "Fine, I'll pay whatever you want!" });
                willPay = true;
                controller.button2SetText("I'm not selling to you.");
            }
            else if (dialogCounter == 3)
            {
                controller.addDialog(new string[] { "Why? Why not??!! How could you?", "My child will die because of you!" });
                controller.customerLeaves(7.5f);

                //END REACHED
                stats.illwomanEnd = true;
                stats.illwomanLevel = 3;
            }
            else if (dialogCounter == 4)
            {
                controller.addDialog(new string[] { ".... How are we going to afford rent...." });
                controller.customerLeaves(6.0f);
                stats.illwomanLevel = 2;
            }
            else if (dialogCounter == 5)
            {
                controller.addDialog(new string[] { "Thank you so much." });
                stats.illwomanLevel = 1;
                controller.customerLeaves(5.0f);
            }
        }

        else if (illWomanLevel == 1)
        {
            controller.addDialog(new string[] { "Hello. I'm here to thank you for what you did the other day.", "My child will live thanks to you.", "I'll never be able to repay you." });
            controller.customerLeaves(7.5f);

            //END REACHED
            stats.illwomanEnd = true;
            stats.illwomanLevel = 4;
        }

        else if (illWomanLevel == 2)
        {
            controller.addDialog(new string[] { "My child is alive but we are living on the streets now." , "I came to tell you that you are a vile, selfish person.", "I hope karma gets you."});
            controller.customerLeaves(7.5f);

            //END REACHED
            stats.illwomanEnd = true;
            stats.illwomanLevel = 5;
        }
    }

    public override void checkBarter(float sliderValue, string barterPriceText){
        if (illWomanLevel == 0)
        {
            if (willPay)
            {
                if (controller.itemOnCounter.isDrugs)
                {
                    if (float.Parse(barterPriceText) <= 10f)
                    {
                        dialogCounter = 5;
                    }
                    else
                    {
                        dialogCounter = 4;
                    }
                    controller.barteringComplete(float.Parse(barterPriceText));
                }
                else
                {
                    controller.addDialog(new string[] { "I need cocaine." });
                }
            }
            else if (float.Parse(barterPriceText) <= 10f)
            {
                if (controller.itemOnCounter.isDrugs)
                {
                    dialogCounter = 5;
                    controller.barteringComplete(float.Parse(barterPriceText));
                }
                else
                {
                    controller.addDialog(new string[] { "I need cocaine." });
                }
            }
            else
            {
                controller.addDialog(new string[] { "I can't afford that, I only have £10 to spare..." });
            }
        }
    }

    public override void barterComplete(){
        if (stats.workingWithCops)
        {
            stats.copRelationDecrease += 1;
            stats.copRelation -= 1;
        }

        text();
    }

    public override void barterFailed(){
    }

    public override void button1Clicked(){
    }

    public override void button2Clicked(){
        if (dialogCounter == 0)
        {
            dialogCounter = 1;
            controller.disableButtons();
            text();
        }
        else if (dialogCounter == 1)
        {
            dialogCounter = 2;
            controller.disableButtons();
            text();
        }
        else if (dialogCounter == 2)
        {
            dialogCounter = 3;
            controller.disableButtons();
            text();
        }
    }
}
