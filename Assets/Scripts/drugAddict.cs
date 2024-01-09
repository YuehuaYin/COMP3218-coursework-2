using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class drugAddict : Customers
{
    GameController controller;
    Sprite sprite;
    int dialogCounter = 0;
    int addictionLevel;
    TrackableValues stats;

    public drugAddict(){
        controller = GameObject.Find("Controller").GetComponent<GameController>();
        stats = GameObject.Find("StatTracker").GetComponent<TrackableValues>();
        addictionLevel = stats.getDrugAddictLevel();

        if (addictionLevel < 2)
        {
            sprite = Resources.Load<Sprite>("Sprites/scriptedNPCs/addict1");
            controller.setCustomerSprite(sprite);
        }
        else { 
            sprite = Resources.Load<Sprite>("Sprites/scriptedNPCs/addict2");
            controller.setCustomerSprite(sprite);
            
        }
    }

    public override void text()
    {
        controller.clearDialogBox();
        if (addictionLevel == 0)
        {
            if (dialogCounter == 0)
            {
                controller.addDialog(new string[] { "Hi. umm.... I heard from a buddy of mine that you sell... unique goods?", "Thing is, I've been going through a rough time lately.", "Wife left, work fired me.... Everything just sucks right now", "I'd like to buy something which will... make me feel happy, y'know?" });
                controller.button2SetText("You don't want to do this, believe me.");
            }
            else if (dialogCounter == 1)
            {
                controller.addDialog(new string[] { "Yes I do. You don't understand anything.", "My life is going to shit. Don't act like you understand.", "Just shut up, do your job and give me the drugs." });
                controller.button2SetText("I'm not selling to you.");
            }
            else if (dialogCounter == 2)
            {
                controller.addDialog(new string[] { "Fine, I guess you don't want my money then.", "I'll go find someone else." });
                controller.customerLeaves(5.0f);
                stats.setDrugAddictLevel(-1);
            }
        }

        else if (addictionLevel == 1)
        {
            if (dialogCounter == 0)
            {
                controller.addDialog(new string[] { "Hi! Remember me? How's it going?", "That's good to hear! I'm doing great, actually.", "I mean my life still sucks but the stuff you gave me last time was great!", "Hit me with some more of the good stuff!" });
                controller.button2SetText("Maybe you shouldn't.");
            }
            else if (dialogCounter == 1)
            {
                controller.addDialog(new string[] { "Why not? It was fine last time.", "My life isn't getting better, this is the only thing that helps.", "Seriously, it's the only thing I can think about." });
                controller.button2SetText("That's why I won't give you any more.");
            }
            else if (dialogCounter == 2)
            {
                controller.addDialog(new string[] { "Fine, I guess you don't want my money then.", "I'll go find someone else." });
                controller.customerLeaves(5.0f);
                stats.setDrugAddictLevel(-1);
            }
        }

        else if (addictionLevel == 2)
        {
            if (dialogCounter == 0)
            {
                controller.addDialog(new string[] { ".....", "..........", "d..ru...g....s...." });
            }
        }

        else if (addictionLevel == -1)
        {
            controller.addDialog(new string[] { "Hi, um, do you remember me? I'm sorry about the other day.", "No one else would sell me drugs so I went home and cried.", "But I'm feeling better today. Life's still rough but...", "Just... thank you for not helping me make a bad decision." });

            //END REACHED
            stats.drugAddictEnd = true;
            stats.setDrugAddictLevel(-2);

            controller.customerLeaves(9.0f);
        }

    }

    public override void checkBarter(float sliderValue, string barterPriceText){
        if (addictionLevel < 2)
        {
            if (controller.itemOnCounter.isDrugs)
            {
                controller.barteringComplete(float.Parse(barterPriceText));
            }
            else
            {
                controller.addDialog(new string[] { "That's not what I meant, I want drugs OK." });
            }
        }
        else
        {
            if (controller.itemOnCounter.isDrugs)
            {
                controller.barteringComplete(float.Parse(barterPriceText));
            }
            else
            {
                controller.addDialog(new string[] { "....no.." });
            }
        }
    }

    public override void barterComplete(){
        if (stats.workingWithCops)
        {
            stats.copRelationDecrease += 1;
            stats.copRelation -= 1;
        }

        if (addictionLevel == 0)
        {
            controller.addDialog(new string[] { "Thank you." });
            controller.customerLeaves(3.5f);
        }
        else if (addictionLevel == 1)
        {
            controller.addDialog(new string[] { "Cheers!" });
            controller.customerLeaves(3.5f);
        }
        else if (addictionLevel == 2)
        {
            controller.addDialog(new string[] { "....." , "...."});
            controller.customerLeaves(5.0f);

            //END REACHED
            stats.drugAddictEnd = true;
        }
        stats.setDrugAddictLevel(addictionLevel += 1);
    }

    public override void barterFailed()
    {

    }

    public override void button1Clicked(){
    }

    public override void button2Clicked(){
        if (addictionLevel == 0)
        {
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
        }

        if (addictionLevel == 1)
        {
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
        }
    }
}
