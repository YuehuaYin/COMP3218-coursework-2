using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UndercoverCop : Customers
{
    GameController controller;
    Sprite sprite;
    Sprite IDSprite;
    int dialogCounter = 0;
    int timesFailed = 0;
    int coverLevel;
    TrackableValues stats;
    private double tolerance;
    string idspritephrase;
    int fakeIDnecklaceNo;
    bool gtfo;
    

    string[] codephraseDialogue = new string[] { "Weather's horribe today.", "Did you watch the dodgers game?", "Do you have beef jerky?" };
    string[] codephraseResponse = new string[] { "It really is", "Yeah, thank god for #15", "No, we don't sell that - try down the street" };
    string[] codephraseIncorrect = new string[] { "I think it's alright", "Yeah, we lost horribly", "Yeah, top shelf on your right" };


    string[] randomNPCSprites = { "customer1", "customer2", "customer3", "customer4", "customer5", "customer6", "customer7", "customer8", "customer9", "customer0" };
    System.Random rand = new System.Random();

    public UndercoverCop()
    {

        gtfo = false;
        tolerance = 1.1;
        controller = GameObject.Find("Controller").GetComponent<GameController>();
        stats = GameObject.Find("StatTracker").GetComponent<TrackableValues>();

        sprite = Resources.Load<Sprite>("Sprites/randomNPCs/" + randomNPCSprites[rand.Next(0, randomNPCSprites.Length)]);
        controller.setCustomerSprite(sprite);

        fakeIDnecklaceNo = stats.CorrectIDNecklace + 1;
        if (fakeIDnecklaceNo > 5)
        {
            fakeIDnecklaceNo = 1;
        }
        idspritephrase = "Sprites/items/chainID" + fakeIDnecklaceNo.ToString();

        IDSprite = Resources.Load<Sprite>(idspritephrase);
        controller.setIDSprite(IDSprite);
        controller.setIDActive(false);
    }


    public override void barterComplete()
    {
        if (stats.workingWithCops && controller.itemOnCounter.isDrugs)
        {
            controller.addDialog(new string[] { "I'll get this back to the station" });
            controller.customerLeaves(3.5f);
        }
        else
        {
            controller.addDialog(new string[] { "Thanks!" });
            controller.customerLeaves(3.5f);
        }
    }

    public override void barterFailed()
    {
        controller.customerLeaves(3.5f);
    }

    public override void button1Clicked()
    {

        if (dialogCounter == 0)
        {
            dialogCounter = 2;
            controller.disableButtons();
            text();
        }
        else if (dialogCounter == 1)
        {
            if (stats.workingWithCops)
            {
                controller.disableButtons();

            }
        }
        else if (dialogCounter == 2)
        {
            controller.disableButtons();
            dialogCounter = 3;
            controller.setIDActive(false);
            text();
        }

        else if (dialogCounter == 3)
        {
            controller.disableButtons();
        }
        
        
    }
    public override void button2Clicked()
    {
        if (dialogCounter == 0)
        {
            dialogCounter = 1;
            controller.disableButtons();
            text();
        }
        else if (dialogCounter == 2)
        {
            controller.disableButtons();
            controller.addDialog(new string[] {"Cmon, I got these yesterday"});
            controller.setIDActive(false);
            gtfo = true;
        }
        else if (dialogCounter == 3)
        {
            dialogCounter = 4;
            controller.disableButtons();
            text();
            
        }
        else if (dialogCounter == 4)
        {
            if (!stats.workingWithCops)
            {
                controller.disableButtons();
                controller.customerLeaves(3.5f);
            }
            else
            {
                controller.disableButtons();
            }
        }
        if (gtfo)
        {
            controller.customerLeaves(3.5f);
        }

    }

    public override void checkBarter(float sliderValue, string barterPriceText)
    {
        if (controller.itemOnCounter.isDrugs)
        {
            if (stats.workingWithCops)
            {
                controller.barteringComplete(float.Parse(barterPriceText));

            }
            else
            {
                controller.addDialog(new string[] { "......Thanks" });

                stats.numDrugsSold += 1;
                stats.WrongSalesNumber += 1;
                controller.customerLeaves(3.5f);
            }

        }
        else
        {
            if (sliderValue < tolerance)
            {
                controller.barteringComplete(float.Parse(barterPriceText));
            }
            else if (tolerance <= 1)
            {
                controller.barteringFailed();
            }
            else
            {
                tolerance -= 0.05;
                timesFailed++;
                if (timesFailed > 2)
                {
                    controller.barteringFailed();
                }
                else
                {
                    controller.addDialog(new string[] { "That's too expensive." });
                    Debug.Log("Tolerance = " + tolerance);
                }
            }
        }
    }
    
    public override void text()
    {
        controller.clearDialogBox();
        if(dialogCounter == 0) {
            switch(stats.GetDayNum())
            {
                case 1:
                    controller.addDialog(new string[] { codephraseDialogue[0] });
                    controller.button1SetText(codephraseResponse[0]);
                    controller.button2SetText(codephraseIncorrect[0]);
                    break;
                case 2:
                    controller.setIDActive(true);
                    controller.addDialog(new string[] { "Yo, I'm tryna score, you the man?" });
                    controller.button1SetText("Yeah, we got the stuff");
                    controller.button2SetText("Nah, you got the wrong place");
                    break;
                case 3:
                    controller.addDialog(new string[] { "I'm looking to buy some drugs" });
                    controller.button1SetText("We got some - you got creds?");
                    controller.button2SetText("Wrong place");
                    break;
                case 4:
                    controller.addDialog(new string[] { codephraseDialogue[1] });
                    controller.button1SetText(codephraseResponse[1]);
                    controller.button2SetText(codephraseIncorrect[1]);
                    break;
                case 5:
                    controller.addDialog(new string[] { codephraseDialogue[2] });
                    controller.button1SetText(codephraseResponse[2]);
                    controller.button2SetText(codephraseIncorrect[2]);
                    break;
                default:
                    break;
            }
        }

        else if (dialogCounter == 1)
        {
            controller.addDialog(new string[] { "Never mind, I've got some errands to run - see you." });
            controller.button2SetText("Goodbye.");
            gtfo = true;
        }

        else if (dialogCounter == 2)
        {
            controller.addDialog(new string[] { "Yeah, got it on me right here?"});
            controller.setIDActive(true);
            controller.button1SetText("Looks good here.");
            controller.button2SetText("This isn't correct.");
        }

        else if (dialogCounter == 3)
        {
            controller.addDialog(new string[] { "So, what've you got in today?" });
            controller.button1SetText("Just the usual.");
            controller.button2SetText("Something special...");
        }

        else if (dialogCounter == 4) 
        {
            if (stats.workingWithCops)
            {
                controller.addDialog(new string[] { "Yeah, I'll take some of the good stuff", "I'll take whatever you're selling" });

                controller.button2SetText("Coming right up!.");
            }
            else
            {
                controller.addDialog(new string[] { "Wait, what's this?", "Sorry buddy, I'm a cop..." });
                controller.button2SetText("Oh no...");
            }
        }

    }



}