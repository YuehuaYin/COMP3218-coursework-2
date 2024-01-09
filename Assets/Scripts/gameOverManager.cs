using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.SceneManagement;


public class gameOverManager : MonoBehaviour
{
    TrackableValues stats;

    [SerializeField] public GameObject earnedTotal;
    [SerializeField] public GameObject daysSurvived;
    [SerializeField] public GameObject title;
    [SerializeField] public GameObject ending;
    [SerializeField] public GameObject nextButton;
    [SerializeField] public GameObject restartButton;

    // NPC epilogue
    [SerializeField] public GameObject pic1;
    [SerializeField] public GameObject pic2;
    [SerializeField] public GameObject pic3;
    [SerializeField] public GameObject npcText1;
    [SerializeField] public GameObject npcText2;
    [SerializeField] public GameObject npcText3;

    // Start is called before the first frame update
    void Start()
    {
        stats = GameObject.Find("StatTracker").GetComponent<TrackableValues>();
        stats.switchBGM(2);

        TMPro.TextMeshProUGUI titleText = title.GetComponent<TMPro.TextMeshProUGUI>();
        TMPro.TextMeshProUGUI endingText = ending.GetComponent<TMPro.TextMeshProUGUI>();

        TMPro.TextMeshProUGUI earnedTotalText = earnedTotal.GetComponent<TMPro.TextMeshProUGUI>();
        TMPro.TextMeshProUGUI daySurvivedText = daysSurvived.GetComponent<TMPro.TextMeshProUGUI>();

        pic1.SetActive(false);
        pic2.SetActive(false);
        pic3.SetActive(false);
        npcText1.SetActive(false);
        npcText2.SetActive(false);
        npcText3.SetActive(false);
        restartButton.SetActive(false);
        nextButton.SetActive(false);

        if (stats.notEnoughMoney)
        {
            titleText.text = "You are executed.";
            endingText.text = "As the boss shakes his head, disappointed that you didn't earn enough money, you spy Dave in the back. He looks away. The last thing you hear is a gunshot before everything goes black.";
        }
        else if (stats.workingWithCops)
        {
            if (stats.copRelation < 0)
            {
                titleText.text = "You are arrested.";
                endingText.text = "You are arrested and charged with conspiracy to sell narcotics. You never hear from Dave again. As you sit and think in your cell, you wonder where it all went wrong...";
            }
            else
            {
                stats.DayNum += 1;

                titleText.text = "The shop closes.";
                endingText.text = "As you step out the door into the cool night to the waiting cop car, you smile in satisfaction to yourself that none of the mob avoided the law. None expect Dave, who gives you a thumbs up outside the window. It's finally over.";
            }
        }
        else
        {
            if (stats.WrongSalesNumber > 3)
            {
                if (stats.betrayedDave)
                {
                    titleText.text = "You are arrested.";
                    endingText.text = "You and your fellow accomplices are arrested and charged with conspiracy to sell narcotics. The court date is set for next week. It will not be a long trial. Maybe this is what you deserve for betraying Dave...";
                }
                else
                {
                    titleText.text = "You are arrested.";
                    endingText.text = "You and your fellow accomplices are arrested and charged with conspiracy to sell narcotics. The court date is set for next week. It will not be a long trial. At the very least, you have Dave with you to keep you company.";
                }
            }
            else
            {
                if (stats.betrayedDave)
                {
                    stats.DayNum += 1;

                    titleText.text = "The shop closes... for tonight";
                    endingText.text = "Your boss is pleased with your work. He suggests a more permanent working relation, with less threat of death. You treat yourself to a nice dinner, staring at the empty seat where Dave would have been. Welcome to the big leagues.";
                }
                else
                {
                    stats.DayNum += 1;

                    titleText.text = "The shop closes... for tonight";
                    endingText.text = "Your boss is pleased with your work. He suggests a more permanent working relation, with less threat of death. You and Dave toast to your new future as business partners. Congratulations and welcome to the big leagues.";
                }
            }
        }

        daySurvivedText.text = "Days survived: " + (stats.GetDayNum() - 1);
        earnedTotalText.text = "Total earned: �" + stats.TotalCash.ToString("0.00");

        if (stats.badmanEnd || stats.drugAddictEnd || stats.illwomanEnd)
        {
            nextButton.SetActive(true);
        }
        else
        {
            restartButton.SetActive(true);
        }
    }

    public void nextButtonPressed()
    {
        TMPro.TextMeshProUGUI titleText = title.GetComponent<TMPro.TextMeshProUGUI>();
        titleText.text = "Excerpts from the local newspaper:";

        restartButton.SetActive(true);
        nextButton.SetActive(false);

        ending.SetActive(false);
        earnedTotal.SetActive(false);
        daysSurvived.SetActive(false);

        GameObject npcPic = pic1;
        GameObject npcText = npcText1;

        if (stats.drugAddictEnd)
        {
            SpriteRenderer picSR = npcPic.GetComponent<SpriteRenderer>();
            TMPro.TextMeshProUGUI npctext = npcText.GetComponent<TMPro.TextMeshProUGUI>();

            if (stats.drugAddictLevel == 3)
            {
                picSR.sprite = Resources.Load<Sprite>("Sprites/scriptedNPCs/addict2");
                npctext.text = "MAN FOUND DEAD AFTER DRUG OVERDOSE: Last night police discovered the body of a man in an alleyway. He is suspected to have died from a drug overdose. Friends and family say he had been going through a hard time and had turned to drugs to cope.";
            }

            else if (stats.drugAddictLevel == -2)
            {
                picSR.sprite = Resources.Load<Sprite>("Sprites/scriptedNPCs/addict1");
                npctext.text = "MAN SPEAKS OUT ABOUT MENTAL HEALTH STRUGGLES: Last night a talk was held where members of the public talked about their struggles. Amongst them was an emotional talk about a man's struggles with mental health and how he has begun the road to healing.";
            }

            npcPic.SetActive(true);
            npcText.SetActive(true);

            npcPic = pic2;
            npcText = npcText2;
        }

        if (stats.illwomanEnd)
        {
            SpriteRenderer picSR = npcPic.GetComponent<SpriteRenderer>();
            TMPro.TextMeshProUGUI npctext = npcText.GetComponent<TMPro.TextMeshProUGUI>();

            if (stats.illwomanLevel == 3)
            {
                picSR.sprite = Resources.Load<Sprite>("Sprites/scriptedNPCs/illwoman");
                npctext.text = "WOMAN FOUND DEAD IN RIVER: The body of a woman was found in the nearby river. Police suspect she committed suicide after her infant son died of a rare illness. \"We are heartbroken,\" says family, struggling to cope with the tragedy that has befallen them.";
            }

            else if (stats.illwomanLevel == 4)
            {
                picSR.sprite = Resources.Load<Sprite>("Sprites/scriptedNPCs/illwoman");
                npctext.text = "WOMAN SPEAKS OUT ABOUT SON'S MIRACULOUS RECOVERY: A woman has spoken about her son's miraculous recovery from a rare illness. \"I am forever grateful for the kind shopkeeper who sold me the medicine he needed,\" she says, her smiling son in her arms.";
            }

            else if (stats.illwomanLevel == 5)
            {
                picSR.sprite = Resources.Load<Sprite>("Sprites/scriptedNPCs/illwomanpoor");
                npctext.text = "HOMELESS CRISIS IN THE CITY: As part of our coverage of the ongoing homelessness crisis, we spoke to a woman who is living on the streets with her infant son after spending all her money acquiring medicine for him.";
            }

            npcPic.SetActive(true);
            npcText.SetActive(true);

            if (npcPic.gameObject.name == "pic1")
            {
                npcPic = pic2;
                npcText = npcText2;
            }
            else if (npcPic.gameObject.name == "pic2")
            {
                npcPic = pic3;
                npcText = npcText3;
            }
        }

        if (stats.badmanEnd)
        {
            SpriteRenderer picSR = npcPic.GetComponent<SpriteRenderer>();
            TMPro.TextMeshProUGUI npctext = npcText.GetComponent<TMPro.TextMeshProUGUI>();

            picSR.sprite = Resources.Load<Sprite>("Sprites/scriptedNPCs/badman");

            if (stats.badmanLevel == -1)
            {
                npctext.text = "SUSPECTED KIDNAPPER CAUGHT: Police have confirmed that they have arrested a man for attempting to kidnap a woman from a bar. Notes on his person reveal a plan to spike her first, but he didn't, allowing the woman to fight him off and call the police.";
            }

            else if (stats.badmanLevel == -2)
            {
                npctext.text = "MAN ON THE RUN AFTER KIDNAPPING WOMAN: Police have released an appeal for anyone with information about this man, who kidnapped a woman after spiking her drink at a bar. The woman has been located and is safe, but the man is on the run.";
            }

            else if (stats.badmanLevel == -3)
            {
                npctext.text = "MULTIPLE PEOPLE SPIKED AND KIDNAPPED: Police have released an appeal for anyone with information about this man, who is suspected of spiking multiple people at a bar and kidnapping them. None of the victims have been found yet.";
            }

            npcPic.SetActive(true);
            npcText.SetActive(true);
        }


    }

    public void restartGameButton()
    {
        Destroy(stats.gameObject);
        SceneManager.LoadScene("TitleScene");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
