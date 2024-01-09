using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Misc;

using UnityEngine;

public class policeman : Customers
{
    GameController controller;
    TrackableValues stats;
    Sprite sprite;
    int dialogCounter = 0;
    bool pleaseletmeleave = false;

    public policeman(){
        controller = GameObject.Find("Controller").GetComponent<GameController>();
        stats = GameObject.Find("StatTracker").GetComponent<TrackableValues>();
        sprite = Resources.Load<Sprite>("Sprites/scriptedNPCs/policeman");
        controller.setCustomerSprite(sprite);
    }

     public override void text()
    {
        controller.clearDialogBox();

        if (controller.day == 1)
        {
            if (dialogCounter == 0)
            {
                controller.addDialog(new string[] { "Good morning. I'm a police officer working with the drug task force." , "We've heard rumours that the mafia is working with shops to sell drugs.", "I have a proposal for you. Will you consider working with us?"});
                controller.button1SetText("Let's hear it...");
                controller.button2SetText("I don't know what you're talking about.");
            }
            else if (dialogCounter == 1)
            {
                controller.addDialog(new string[] { "That's a real shame. I had hoped you wouldn't make this difficult." , "You'll hear from us again soon enough.", "Watch your back." });
                controller.customerLeaves(7.0f);
            }
            else if (dialogCounter == 2)
            {
                controller.addDialog(new string[] { "My boys needs more time to collect evidence and find the ringleader." , "I'll send undercover cops in, you sell drugs to them.", "Don't sell to any regular folks, or I'll be upset.", "How does that sound?"});
                controller.button1SetText("I'll work with you.");
                controller.button2SetText("Not happening.");
            }
            else if (dialogCounter == 3)
            {
                controller.addDialog(new string[] { "Excellent. I look forward to working with you." , "We'll be in touch soon.", "Don't sell anyone drugs today." });
                stats.workingWithCops = true;
                controller.customerLeaves(7.0f);
            }
        }

        else if (controller.day == 2)
        {
            if (dialogCounter == 0)
            {
                if (stats.workingWithCops)
                {
                    pleaseletmeleave = true;

                    if (stats.copRelationDecrease == 0)
                    {
                        controller.addDialog(new string[] { "Excellent job yesterday. Here's a little something for your trouble.", "I'll be sending cops today to collect samples of your drugs.","They'll be wearing gold chains to blend in with your boss' clientele.","We don't have the money to buy the latest, so they'll be wearing old chains.","Sell them the drugs you've got.", "Keep up the good work." });
                        controller.earnMoney(stats.DayNum * 10);
                        stats.copRelation += 2;
                        stats.copRelationDecrease = 0;
                    }
                    else
                    {
                        controller.addDialog(new string[] { "You slipped up a few times yesterday. Don't think I don't know.", "No matter, continue with your work.", "The cops today will be wearing out of fashion chains. Don't forget." });
                        stats.copRelationDecrease = 0;

                    }
                    controller.button2SetText("Understood.");
                }
                else
                {
                    controller.addDialog(new string[] { "Good morning. I trust you remember who I am.", "For your own sake, reconsider working with us." });

                    controller.button1SetText("Maybe...");
                    controller.button2SetText("No.");
                }
            }

            else if (dialogCounter == 1)
            {
                controller.addDialog(new string[] { "That's a real shame. I had hoped you wouldn't make this difficult."});
                controller.customerLeaves(3.0f);
            }
            else if (dialogCounter == 2)
            {
                controller.addDialog(new string[] { "My boys needs more time to collect evidence and find the ringleader.", "I'll send undercover cops in, you sell drugs to them.", "Don't sell to any regular folks, or I'll be upset.", "How does that sound?" });
                controller.button1SetText("I'll work with you.");
                controller.button2SetText("Not happening.");
            }
            else if (dialogCounter == 3)
            {
                controller.addDialog(new string[] { "Excellent. I look forward to working with you.", "The cops today will be wearing gold chains to blend in. Don't forget.", "Don't tell anyone about this. I'll be in touch soon." });
                stats.workingWithCops = true;
                controller.customerLeaves(7.0f);
            }
        }

        else if (controller.day == 3)
        {
            if (dialogCounter == 0)
            {
                if (stats.workingWithCops)
                {
                    pleaseletmeleave = true;

                    if (stats.copRelationDecrease == 0)
                    {
                        controller.addDialog(new string[] { "Excellent job yesterday.","Your boss is moving up - a rival org is sending their men in to pick up samples of your product.", "Our officers will look like them - but we still can't afford the latest chains","Remember, not the latest chains.", "Keep up the good work." });
                        controller.earnMoney(stats.DayNum * 10);
                        stats.copRelation += 2;
                        stats.copRelationDecrease = 0;
                    }
                    else
                    {
                        controller.addDialog(new string[] { "You slipped up a few times yesterday. Don't think I don't know.", "No matter, continue with your work.", "The cops today will be concealing their chains, ask them to show them. Don't forget." });
                        stats.copRelationDecrease = 0;

                    }
                    controller.button2SetText("Understood.");
                }
                else
                {
                    controller.addDialog(new string[] { "Good morning. I trust you remember who I am.", "For your own sake, reconsider working with us." });

                    controller.button1SetText("Maybe...");
                    controller.button2SetText("No.");
                }
            }

            else if (dialogCounter == 1)
            {
                controller.addDialog(new string[] { "That's a real shame. I had hoped you wouldn't make this difficult." });
                controller.customerLeaves(3.0f);
            }
            else if (dialogCounter == 2)
            {
                controller.addDialog(new string[] { "My boys needs more time to collect evidence and find the ringleader.", "I'll send undercover cops in, you sell drugs to them.", "Don't sell to any regular folks, or I'll be upset.", "How does that sound?" });
                controller.button1SetText("I'll work with you.");
                controller.button2SetText("Not happening.");
            }
            else if (dialogCounter == 3)
            {
                controller.addDialog(new string[] { "Excellent. I look forward to working with you.", "The cops today will be wearing out of fashion gold chains. Don't forget.", "Don't tell anyone about this. I'll be in touch soon." });
                stats.workingWithCops = true;
                controller.customerLeaves(7.0f);
            }
        }

        else if (controller.day == 4)
        {
            if (stats.workingWithCops)
            {
                pleaseletmeleave = true;

                if (stats.copRelationDecrease == 0)
                {
                    controller.addDialog(new string[] { "Excellent job yesterday. Here's a little something for your trouble.", "Your boss is getting paranoid, we're closing in.","He's setup a code phrase for IDing special pickups, we'll have to blend in.", "Today's is: Did you watch the dodgers game?", "Answer is: yeah, thank god for #15", "Keep up the good work." });
                    controller.earnMoney(stats.DayNum * 10);
                    stats.copRelation += 2;
                    stats.copRelationDecrease = 0;
                }
                else
                {
                    controller.addDialog(new string[] { "You slipped up a few times yesterday. Don't think I don't know.", "No matter, continue with your work.", "The cops today will be wearing concealed gold chains. Don't forget." });
                    stats.copRelationDecrease = 0;

                }
                controller.button2SetText("Understood.");
            }
        }

        else if (controller.day == 5)
        {
            if (stats.workingWithCops)
            {
                pleaseletmeleave = true;

                if (stats.copRelationDecrease == 0)
                {
                    controller.addDialog(new string[] { "Excellent job yesterday. Here's a little something for your trouble.", "We're so close to your boss - just hang in there for 1 more day..", "We intercepted the code for today, it's something about beef jerky","If anyone asks if you have it, say no" ,"Keep up the good work." });
                    controller.earnMoney(stats.DayNum * 10);
                    stats.copRelation += 2;
                    stats.copRelationDecrease = 0;
                }
                else
                {
                    controller.addDialog(new string[] { "You slipped up a few times yesterday. Don't think I don't know.", "No matter, continue with your work.", "The code for today is ","Don't forget." });
                    stats.copRelationDecrease = 0;

                }
                controller.button2SetText("Understood.");
            }
        }
    }

    public override void checkBarter(float sliderValue, string barterPriceText){
        controller.addDialog(new string[] { "I'm not here to buy anything." });
    }

    public override void barterComplete(){
        
    }

    public override void barterFailed(){

    }

    public override void button1Clicked(){
        if ((controller.day == 1) || (controller.day == 2) || (controller.day == 3))
        {
            if (dialogCounter == 0)
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

    public override void button2Clicked(){
        if (pleaseletmeleave)
        {
            controller.customerLeaves(3.5f);
        }
        else if ((controller.day == 1) || (controller.day == 2) || (controller.day == 3) || (controller.day == 4))
        {
            dialogCounter = 1;
            controller.disableButtons();
            text();
        }
    }
}
