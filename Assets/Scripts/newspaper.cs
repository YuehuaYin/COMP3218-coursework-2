using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class newspaper : MonoBehaviour
{
    public Image ImageOnPanel;  ///set this in the inspector
    private int textureChain;
    bool setupnewspaper = false;
    Sprite newsprite = null;

    public List<Sprite> spriteArr = new List<Sprite>();

    public List<Sprite> cityArr = new List<Sprite>();

    public List<Sprite> personArr = new List<Sprite>();

    [SerializeField] TrackableValues stats;
    [SerializeField] Image cityimg;
    [SerializeField] Image personimg;
    [SerializeField] TextMeshProUGUI citytext;
    [SerializeField] TextMeshProUGUI persontext;
    private void Update()
    {
        if (!setupnewspaper)
        {
            textureChain = stats.CorrectIDNecklace;
            print(textureChain);


            newsprite = spriteArr[stats.CorrectIDNecklace-1];
            ImageOnPanel.sprite = newsprite;
            setupnewspaper = true;
            this.gameObject.SetActive(false);

            if (stats.cityDrugStatus >= 6)
            {
                cityimg.sprite = cityArr[3];
                citytext.text = "City overrun by criminals!";
            }
            else if (stats.cityDrugStatus >= 4)
            {
                cityimg.sprite = cityArr[2];
                citytext.text = "Rising crime levels in the city!.";
            }
            else if (stats.cityDrugStatus >= 2)
            {
                cityimg.sprite = cityArr[1];
                citytext.text = "City council expresses concern about drugs.";
            }
            else
            {
                cityimg.sprite = cityArr[0];
                citytext.text = "City council elections - all candidates.";
            }


            if (stats.workingWithCops)
            {
                if (stats.copRelation == 3)
                {
                    persontext.text = "Investigation into \"THE BOSS\" nearing completion, police say.";
                }
                else if (stats.copRelation == 2)
                {
                    persontext.text = "Police say arrests will ramp up.";
                }
                else
                {
                    persontext.text = "Who is the crime lord \" THE BOSS\"?.";
                }
            }
            else
            {
                if (stats.WrongSalesNumber == 0)
                {
                    persontext.text = "Police investigation stalls.";
                }
                else if (stats.WrongSalesNumber == 1)
                {
                    persontext.text = "Arrests ramp up in downtown.";
                }
                else
                {
                    persontext.text = "Police \"confident\" in drug trials.";
                }

            }


        }
    }
    private void setContent(int daynum) 
    { 
        
    }
}