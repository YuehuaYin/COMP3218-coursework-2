using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndDayManager : MonoBehaviour
{
    TrackableValues stats;

    [SerializeField] public GameObject earnedToday;
    [SerializeField] public GameObject earnedTotal;
    [SerializeField] public GameObject cityStatus;
    [SerializeField] public GameObject policeStatus;
    [SerializeField] public GameObject day;
    [SerializeField] public GameObject target;

    // Start is called before the first frame update
    void Start()
    {

        stats = GameObject.Find("StatTracker").GetComponent<TrackableValues>();

        TMPro.TextMeshProUGUI earnedTodayText = earnedToday.GetComponent<TMPro.TextMeshProUGUI>();
        TMPro.TextMeshProUGUI earnedTotalText = earnedTotal.GetComponent<TMPro.TextMeshProUGUI>();
        TMPro.TextMeshProUGUI cityStatusText = cityStatus.GetComponent<TMPro.TextMeshProUGUI>();
        TMPro.TextMeshProUGUI policeStatusText = policeStatus.GetComponent<TMPro.TextMeshProUGUI>();
        TMPro.TextMeshProUGUI dayNumText = day.GetComponent<TMPro.TextMeshProUGUI>();
        TMPro.TextMeshProUGUI targetText = target.GetComponent<TMPro.TextMeshProUGUI>();

        dayNumText.text = "Day " + stats.GetDayNum() + " Complete";

        earnedTodayText.text = stats.Cash.ToString("0.00");
        earnedTotalText.text = stats.TotalCash.ToString("0.00");

        targetText.text = "Tomorrow's target is £" + stats.targetMoney.ToString("0.00");

        if (stats.cityDrugStatus >= 6)
        {
            cityStatusText.text = "filled with decaying corpses from overdoses."; 
        }
        else if (stats.cityDrugStatus >= 4)
        {
            cityStatusText.text = "swarming with addicts.";
        }
        else if (stats.cityDrugStatus >= 2)
        {
            cityStatusText.text = "experiencing a mild drug problem.";
        }
        else
        {
            cityStatusText.text = "moderately pleasant.";
        }

        if (stats.workingWithCops)
        {
            if (stats.copRelation == 3)
            {
                policeStatusText.text = "happy with your performance.";
            }
            else if (stats.copRelation == 2)
            {
                policeStatusText.text = "questioning your usefulness.";
            }
            else
            {
                policeStatusText.text = "not pleased with you.";
            }
        }
        else
        {
            if (stats.WrongSalesNumber == 0)
            {
                policeStatusText.text = "frustrated about the lack of evidence.";
            }
            else if (stats.WrongSalesNumber == 1)
            {
                policeStatusText.text = "building a case against you.";
            }
            else
            {
                policeStatusText.text = "confident they will be able to arrest you.";
            }
        }

    }

    public void newdayButtonClicked()
    {
        stats.playSFX(0);
        stats.SetDayNum(stats.GetDayNum() + 1);

        stats.CorrectIDNecklace = stats.GetDayNum();

        // calculate new city drug status
        stats.oldCityStatus = stats.cityDrugStatus;
        stats.cityDrugStatus += 1;
        if (stats.numDrugsSold >= (3 * stats.DayNum))
        {
            stats.cityDrugStatus += 1;
        }

        // calculate new police interest when working with cops
        if ((stats.workingWithCops) && (stats.copRelationDecrease == 0))
        {
            stats.PoliceInterest += 10;
            stats.copRelation += 1;
        }
        stats.copRelationDecrease = 0;

        // new police interest when not working with cops (based on city status)
        if (!stats.workingWithCops)
        {
            stats.PoliceInterest += stats.cityDrugStatus * 2;
        }

        SceneManager.LoadScene("Store");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
