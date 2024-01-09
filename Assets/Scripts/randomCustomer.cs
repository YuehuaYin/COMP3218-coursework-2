using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using System;

public class randomCustomer : Customers
{
    GameController controller;
    TrackableValues stats;
    Sprite sprite;

    double tolerance;
    double susceptibility;

    int timesFailed = 0;
    bool isCop;
    bool wantsDrugs = false;

    string[] itemNames = {"Alcohol", "Cake", "Sandwich", "Magazine", "Soft drink", "Cigarettes" };

    string[] randomNPCSprites = { "customer1", "customer2", "customer3", "customer4", "customer5", "customer6", "customer7", "customer8", "customer9", "customer0" };
    string[] randomCopSprites = { "cop1", "cop2", "cop3", "cop4", "cop5" };

    string wantedItem;
    System.Random rand = new System.Random();

    public randomCustomer(){
        controller = GameObject.Find("Controller").GetComponent<GameController>();
        stats = GameObject.Find("StatTracker").GetComponent<TrackableValues>();

        sprite = Resources.Load<Sprite>("Sprites/randomNPCs/" + randomNPCSprites[rand.Next(0, randomNPCSprites.Length)]);
        tolerance = rand.NextDouble() * 0.5 + 1; //make this more complicated - scales with day maybe?
        susceptibility = rand.NextDouble() * (stats.cityDrugStatus + 1);

        controller.setCustomerSprite(sprite);

        Debug.Log("Tolerance = " + tolerance);
        Debug.Log("Susceptibility = " + susceptibility);

        wantedItem = itemNames[rand.Next(0, itemNames.Length)];
    }

     public override void text()
    {
        controller.disableButtons();

        int askingfordrugs = rand.Next(0, 100) + ((stats.cityDrugStatus + 1) * 5);

        Debug.Log(askingfordrugs);

        if (askingfordrugs > 90)
        {
            controller.addDialog(new string[] { "I want to buy drugs."});
            controller.button2SetText("We don't have that.");

            wantsDrugs = true;
        }

        else {
            controller.addDialog(new string[] { "I want to buy " + wantedItem.ToLower() + "." });

            controller.button1SetText("I've got something better.");
            controller.button2SetText("We don't have that.");
        }
    }

    public override void checkBarter(float sliderValue, string barterPriceText)
    {
        if (wantsDrugs)
        {
            if (controller.itemOnCounter.getIsDrugs())
            {

                if (sliderValue < tolerance)
                {
                    controller.barteringComplete(float.Parse(barterPriceText));
                    stats.numDrugsSold += 1;
                    if (stats.workingWithCops)
                    {
                        stats.copRelationDecrease += 1;
                    }
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
        else
        {
            if (controller.itemOnCounter.getName() == wantedItem)
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

            else
            {
                tolerance -= 0.05;
                timesFailed++;
                if (timesFailed > 2)
                {
                    controller.addDialog(new string[] { "How incompetant are you?? I give up, I'll shop somewhere else" });
                    controller.customerLeaves(3.5f);
                }
                else
                {
                    controller.addDialog(new string[] { "That's wrong, I asked for " + wantedItem.ToLower() + "." });
                }
            }
        }
        
    }

    public override void barterComplete(){
        if (tolerance > 1.2){
            controller.addDialog(new string[] {"Good purchase!"});
        }
        else {
            controller.addDialog(new string [] {"Fine, I'll take it."});
        }
        controller.customerLeaves(3.5f);
    }

    public override void barterFailed(){
        controller.addDialog(new string[] {"These prices are unreasonable, screw this."});
        controller.customerLeaves(3.5f);
    }

    public override void button1Clicked(){
        if (susceptibility > 0.5)
        {
            string[] accepttext = new string[2];

            if (stats.cityDrugStatus < 2)
            {
                accepttext[0] = "Drugs? ... You know what, I'll try some.";
                accepttext[1] = "I never thought I'd really do something like this...";
            }
            else if (stats.cityDrugStatus < 4)
            {
                accepttext[0] = "I know a friend who takes this kind of thing.";
                accepttext[1] = "Yeah, I think I'll give these drugs a try.";
            }
            else
            {
                accepttext[0] = "Well if you're offering, I'm not going to say no!";
                accepttext[1] = "Whole city's on this stuff these days, I might as well!";
            }

            controller.addDialog(accepttext);
            wantsDrugs = true;

            controller.disableButtons();
            controller.button2SetText("We don't have that.");
        }

        else
        {
            controller.addDialog(new string[] { "I'm not interested in that kind of thing.", "Give me " + wantedItem.ToLower() + "." });
        }
    }

    public override void button2Clicked(){
        if ((stats.workingWithCops) && (isCop))
        {
            stats.copRelation -= 1;
            stats.copRelationDecrease += 1;
        }
        controller.customerLeaves(3.5f);
    }
}
