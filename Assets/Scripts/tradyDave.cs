using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class tradyDave : Customers
{
    GameController controller;
    TrackableValues stats;
    Sprite sprite;
    int dialogCounter = 0;

    public tradyDave(){
        controller = GameObject.Find("Controller").GetComponent<GameController>();
        stats = GameObject.Find("StatTracker").GetComponent<TrackableValues>();

        sprite = Resources.Load<Sprite>("Sprites/scriptedNPCs/dave");
        controller.setCustomerSprite(sprite);
    }

     public override void text()
    {
        controller.clearDialogBox();

        /**
        if (stats.betrayedDave)
        {
            controller.addDialog(new string[] { "Your 'friend' Dave is sitting in a jail cell now.", "He's been crying since he found out who sold him out." , "Anyway, you haven't forgotten our deal right?"});

            if (controller.day == 2) {
                controller.addDialog(new string[] {"The undercover cops will be wearing hexagonal badges"});
            }
            else if (controller.day == 3)
            {
                controller.addDialog(new string[] { "The undercover cops will be wearing hexagonal badges" });
            }
            else if (controller.day == 3)
            {
                controller.addDialog(new string[] { "The undercover cops will be wearing hexagonal badges" });
            }

            else if (controller.day == 3)
            {
                controller.addDialog(new string[] { "Don't betray me like you betrayed your friend." });
            }

            controller.drugsAcquired();
            stats.numDrugsSold = 0;
            controller.customerLeaves(10.0f);

        }
        **/

        if (controller.day == 1)
        {
            if (dialogCounter == 0)
            {
                controller.addDialog(new string[] { "Hey man how's it going?", "Heard you're running this shop to pay off your debts to the mafia.", "Sounds tough! Boss says you need to earn £30 or he'll kill you!", "I hope you remember how everything works!" });
                controller.button1SetText("Actually I need a refresher.");
                controller.button2SetText("Yeah I do.");
            }
            else if (dialogCounter == 1)
            {
                controller.addDialog(new string[] { "Seriously...? OK, pretend I'm a customer. I want to buy... cigarettes.", "Select some cigarettes from the shelves on the left.", "You can adjust the sale price using the slider.", "Try it and press OK!" });
            }
            else if (dialogCounter == 2)
            {
                controller.addDialog(new string[] { "You got it! I'm not paying for that though haha.", "Make sure not to set prices too high or people might not buy anything.", "Also, it might be hard to get enough money, so I've got an idea." });
                controller.button2SetText("Let's hear it.");
            }
            else if (dialogCounter == 3)
            {
                controller.addDialog(new string[] { "So uhh it might not be legal but it'll help you pay your debt." , "Try offering some to customers, they might just accept."});
                controller.drugsAcquired();
                controller.button1SetText("I don't want to sell drugs");
                controller.button2SetText("I'm down.");
            }
            else if (dialogCounter == 4)
            {
                controller.addDialog(new string[] { "Fine, good luck paying your debt back with that attitude." , "You can keep it in case you change your mind." , "I'll see you tomorrow, OK? Good luck."});
                controller.customerLeaves(7.0f);
            }
            else if (dialogCounter == 5)
            {
                controller.addDialog(new string[] { "Wow, you didn't need any convincing haha." , "Alright, I'll leave you to it. See you tommorrow!"});
                controller.customerLeaves(5.0f);
            }
            else if (dialogCounter == 9)
            {
                controller.addDialog(new string[] { "That's great! There's one more thing before I leave you alone.", "I thought it might be hard to get enough money, so I've got an idea." });
                controller.button2SetText("Let's hear it.");
            }
        }

        else if (controller.day == 2)
        {
            if (dialogCounter == 0)
            {
                controller.addDialog(new string[] { "Hey dude! Good work yesterday!"});

                if ((stats.oldCityStatus < 2 && stats.cityDrugStatus>= 2) || (stats.oldCityStatus < 4 && stats.cityDrugStatus >= 4) || (stats.oldCityStatus < 6 && stats.cityDrugStatus >= 6)) {
                    controller.addDialog(new string[] { "I got you some stronger stuff than yesterday.", "You sold so many drugs, people should be more receptive to them now."});
                }
                else
                {
                    controller.addDialog(new string[] { "Here's some more drugs, hopefully you found them useful!" });
                }
                if (stats.workingWithCops)
                {
                    controller.addDialog(new string[] { "Seems like undercover cops have started going into shops.", "Keep an eye out!", "Definitely don't sell drugs to them. OK bye!" });
                }
                else
                {
                    controller.addDialog(new string[] { "Boss is sending a few goons in to pick up some samples.", "Keep an eye out - if you identify their chains there's a big payday in it for you!"});
                }
                controller.addDialog(new string[] { "Oh by the way, I've left the newspaper on your desk. Might be interesting?" });
                controller.setNewsActive();
                controller.drugsAcquired();
                stats.numDrugsSold = 0;
                controller.customerLeaves(11.0f);
            }
        }

        else if (controller.day == 3)
        {
            if (dialogCounter == 0)
            {
                controller.addDialog(new string[] { "Hey dude! Good work yesterday!" });

                if ((stats.oldCityStatus < 2 && stats.cityDrugStatus >= 2) || (stats.oldCityStatus < 4 && stats.cityDrugStatus >= 4) || (stats.oldCityStatus < 6 && stats.cityDrugStatus >= 6))
                {
                    controller.addDialog(new string[] { "I got you some stronger stuff than yesterday.", "You sold so many drugs, people should be more receptive to them now." });
                }
                else
                {
                    controller.addDialog(new string[] { "Here's some more drugs, hopefully you found them useful!" });
                }

                controller.addDialog(new string[] { "I heard the cops today are disguising themselves.", "They're so poor I bet they can't afford the latest chains", "Definitely don't sell drugs to them!", "Alright I'll be off now, bye!" });

                controller.drugsAcquired();
                stats.numDrugsSold = 0;
                controller.customerLeaves(11.0f);
            }
        }

        else if (controller.day == 4)
        {
            if (dialogCounter == 0)
            {
                controller.addDialog(new string[] { "Hey dude! Good work yesterday!" });

                if ((stats.oldCityStatus < 2 && stats.cityDrugStatus >= 2) || (stats.oldCityStatus < 4 && stats.cityDrugStatus >= 4) || (stats.oldCityStatus < 6 && stats.cityDrugStatus >= 6))
                {
                    controller.addDialog(new string[] { "I got you some stronger stuff than yesterday.", "You sold so many drugs, people should be more receptive to them now." });
                }
                else
                {
                    controller.addDialog(new string[] { "Here's some more drugs, hopefully you found them useful!" });
                }

                controller.addDialog(new string[] { "I heard the cops today are coming in smarter so boss set up a code phrase",  "If someone asks about the dodgers game, thank number 15!", "Alright I'll be off now, bye!" });

                controller.drugsAcquired();
                stats.numDrugsSold = 0;
                controller.customerLeaves(11.0f);
            }
        }

        else if (controller.day == 5)
        {
            if (dialogCounter == 0)
            {
                controller.addDialog(new string[] { "Hey dude! Good work yesterday!", "This is the last day before you pay off your debt." });

                if ((stats.oldCityStatus < 2 && stats.cityDrugStatus >= 2) || (stats.oldCityStatus < 4 && stats.cityDrugStatus >= 4) || (stats.oldCityStatus < 6 && stats.cityDrugStatus >= 6))
                {
                    controller.addDialog(new string[] { "I got you some stronger stuff than yesterday.", "You sold so many drugs, people should be more receptive to them now." });
                }
                else
                {
                    controller.addDialog(new string[] { "Here's some more drugs, hopefully you found them useful!" });
                }

                controller.addDialog(new string[] { "New code from the boss, he's getting paranoid!", " Remember, we DON'T have beef jerky.", "Alright I'll be off now, bye!" });

                controller.drugsAcquired();
                stats.numDrugsSold = 0;
                controller.customerLeaves(11.0f);
            }
        }
    }
    
    public override void checkBarter(float sliderValue, string barterPriceText){
        if (controller.day == 1 && dialogCounter == 1)
        {
            if (controller.itemOnCounter.getName() == "Cigarettes")
            {
                dialogCounter = 2;
                controller.removeItemFromCounter();
                text();
            }
            else
            {
                controller.addDialog(new string[] { "That's not cigarettes..." });
            }
        }
    }

    public override void barterComplete(){
        
    }

    public override void barterFailed(){

    }

    public override void button1Clicked(){
        if (controller.day == 1)
        {
            if (dialogCounter == 0)
            {
                dialogCounter = 1;
                controller.disableButtons();
                text();
            }
            else if (dialogCounter == 3)
            {
                dialogCounter = 4;
                controller.disableButtons();
                text();
            }
        }
    }

    public override void button2Clicked(){
        if (controller.day == 1)
        {
            if (dialogCounter == 0)
            {
                dialogCounter = 9;
                controller.disableButtons();
                text();
            }
            else if ((dialogCounter == 9) || (dialogCounter == 2))
            {
                dialogCounter = 3;
                controller.disableButtons();
                text();
            }
            else if (dialogCounter == 3)
            {
                dialogCounter = 5;
                controller.disableButtons();
                text();
            }
        }
    }
}
