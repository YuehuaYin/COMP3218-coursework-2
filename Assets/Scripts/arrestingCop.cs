using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms;

public class arrestingCop : Customers
{
    GameController controller;
    TrackableValues stats;
    Sprite sprite;
    int dialogCounter = 0;

    public arrestingCop(){
        controller = GameObject.Find("Controller").GetComponent<GameController>();
        stats = GameObject.Find("StatTracker").GetComponent<TrackableValues>();

        sprite = Resources.Load<Sprite>("Sprites/scriptedNPCs/policeman");
        controller.setCustomerSprite(sprite);
        stats.switchBGM(2);
    }

     public override void text()
    {
        controller.clearDialogBox();
        if (!stats.workingWithCops)
        {
            if (dialogCounter == 0)
            {
                controller.addDialog(new string[] { "Hands in the air! You're under arrest!", "You've been working with the mafia and selling drugs.", "You're going to go away for a long time!" });
                controller.button1SetText("Let's make a deal!");
                controller.button2SetText("You got me...");
            }
            else if (dialogCounter == 1)
            {
                controller.addDialog(new string[] { "Hah! Now you want to work with us? You had your chance!", "Unless... If you tell us who's been supplying you with drugs....", "We MIGHT be able to work something out." });
                controller.button1SetText("His name is Dave...");
                controller.button2SetText("*Stay silent*");
            }
            else if (dialogCounter == 2)
            {
                controller.addDialog(new string[] { "Betraying your own friend like that? You are truly despicable.", "You may be scum, but I suppose you've done us a service.", " I'm confiscating those drugs. Don't cross the law again." });
                controller.customerLeaves(7.0f);
                controller.drugObject.SetActive(false);
                stats.PoliceInterest = 0;
                stats.copRelation = -100;
                stats.betrayedDave = true;
            }
        }

        else
        {
            if (dialogCounter == 0)
            {
                controller.addDialog(new string[] { "Hands in the air! You're under arrest!", "You haven't done your part in our deal. You are useless to us." });
                controller.button1SetText("Wait, let's work it out!");
                controller.button2SetText("You got me...");
            }
            else if (dialogCounter == 1)
            {
                controller.addDialog(new string[] { "Hah! Work it out? You had your chance!", "Unless... If you tell us who's been supplying you with drugs....", "We MIGHT be able to work something out." });
                controller.button1SetText("His name is Dave...");
                controller.button2SetText("*Stay silent*");
            }
            else if (dialogCounter == 2)
            {
                controller.addDialog(new string[] { "Betraying your own friend like that? You are truly despicable.", "You may be scum, but I suppose you've done us a service.", " I'm confiscating those drugs. Don't cross the law again." });
                controller.customerLeaves(7.0f);
                controller.drugObject.SetActive(false);
                stats.PoliceInterest = 0;
                stats.copRelation = -100;
                stats.betrayedDave = true;
                stats.switchBGM(1);
            }
        }
    }

    public override void checkBarter(float sliderValue, string barterPriceText){

    }

    public override void barterComplete(){
        
    }

    public override void barterFailed(){

    }

    public override void button1Clicked(){
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

    public override void button2Clicked(){
        SceneManager.LoadScene("EndingScene");
    }
}
