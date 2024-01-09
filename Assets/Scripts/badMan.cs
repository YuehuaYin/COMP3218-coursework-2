using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class badMan : Customers
{
    GameController controller;
    Sprite sprite;
    int dialogCounter = 0;
    int badManLevel;
    TrackableValues stats;
    bool willPay;

    public badMan(){
        controller = GameObject.Find("Controller").GetComponent<GameController>();
        stats = GameObject.Find("StatTracker").GetComponent<TrackableValues>();
        badManLevel = stats.getbadManLevel();

        willPay = false;

        sprite = Resources.Load<Sprite>("Sprites/scriptedNPCs/badman");
        controller.setCustomerSprite(sprite);
    }

     public override void text()
    {
        controller.clearDialogBox();
        if (badManLevel == 0)
        {
            if (dialogCounter == 0)
            {
                controller.addDialog(new string[] { "Hey, I heard that you sell drugs here hehehe...", "Those drugs, when you take them does it inhibit senses?", "Like theoretically would it make it easier to sneak up on someone?"});
                controller.button2SetText("Yes, it would.");
                controller.button2SetText("Why do you ask?");
            }
            else if (dialogCounter == 1)
            {
                controller.addDialog(new string[] { "No reason, no reason hehehe...", "Just pass me some of those yeah? I'll pay whatever.", "Oh man, this'll make it so much easier..." });
                controller.button2SetText("I'm not selling to you.");
            }
            else if (dialogCounter == 2)
            {
                controller.addDialog(new string[] { "Hahahaha much thanks..." });
                controller.customerLeaves(3.5f);
                stats.badmanLevel = 1;
            }
        }

        else if (badManLevel == 1)
        {
            if (dialogCounter == 0)
            {
                controller.addDialog(new string[] { "Hello, hehehehe... Thank you for you help last time.", "They were so effective, it made me work so much easier hehe..", "I'd like some more please..." });
                controller.button2SetText("I'm not selling to you.");
            }
            else if (dialogCounter == 1)
            {
                controller.addDialog(new string[] { "Hmmmmmm? Why not? I haven't done anything wrong.", "If I did though, you're an acomplice you know.", "Give me the drugs or I'll tell the police all about you hehe.." });
                controller.button2SetText("Leave.");
            }
            else if (dialogCounter == 2)
            {
                controller.addDialog(new string[] { "Haha you really don't care about what I do with these hmm?", "Thank you, you've made my life so much easier hehehe" });
                controller.customerLeaves(5.0f);

                //END REACHED
                stats.badmanEnd = true;
                stats.badmanLevel = -3;
            }
        }
    }

    public override void checkBarter(float sliderValue, string barterPriceText){
        if (controller.itemOnCounter.isDrugs)
        {
            dialogCounter = 2;
            controller.barteringComplete(float.Parse(barterPriceText));
        }
        else
        {
            controller.addDialog(new string[] { "No no, the drugs man, give me the drugs." });
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

    public override void barterFailed() { 
    }

    public override void button1Clicked()
    {
    }

    public override void button2Clicked()
    {
        if (badManLevel == 0)
        {
            if (dialogCounter == 0)
            {
                dialogCounter = 1;
                controller.disableButtons();
                text();
            }
            else if (dialogCounter == 1)
            {
                controller.clearDialogBox();
                controller.addDialog(new string[] { "Damn it... you're ruining my plans..." });
                controller.customerLeaves(4.0f);

                //END REACHED
                stats.badmanEnd = true;
                stats.badmanLevel = -1;
            }
        }

        else if (badManLevel == 1)
        {
            if (dialogCounter == 0)
            {
                dialogCounter = 1;
                controller.disableButtons();
                text();
            }
            else if (dialogCounter == 1)
            {
                controller.clearDialogBox();
                controller.addDialog(new string[] { "Hehehe... I'll tell them allll about you!" });

                if (stats.workingWithCops)
                {
                    stats.copRelationDecrease += 1;
                    stats.copRelation -= 1;
                }
                else
                {
                    stats.WrongSalesNumber += 1;
                }
                controller.customerLeaves(4.0f);

                //END REACHED
                stats.badmanEnd = true;
                stats.badmanLevel = -2;
            }
        }

    }

}
