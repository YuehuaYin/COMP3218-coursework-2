using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndingManager : MonoBehaviour

{

    [SerializeField] private TextMeshProUGUI PoliceGood;
    [SerializeField] private TextMeshProUGUI PoliceBad;
    [SerializeField] private TextMeshProUGUI MobGood;
    [SerializeField] private TextMeshProUGUI MobBad;
    [SerializeField] private TextMeshProUGUI WinText;
    [SerializeField] private TextMeshProUGUI LossText;
    [SerializeField] private TextMeshProUGUI DaveBetrayed;

    private bool PolicePath;
    private int WrongSalesNo;

    TrackableValues stats;

    // Start is called before the first frame update
    void Start()
    {
        stats = GameObject.Find("StatTracker").GetComponent<TrackableValues>();


        PoliceGood.enabled = false;
        PoliceBad.enabled = false;  
        WinText.enabled = false;
        LossText.enabled = false;
        MobBad.enabled = false;
        MobGood.enabled = false;
        DaveBetrayed.enabled = false;

        /**
        if (stats.workingWithCops)
        {
            if(stats.copRelation < 0)
            {
                PoliceBad.enabled=true;
            }
            else
            {
                PoliceGood.enabled=true;
            }
        }
        else
        {
            if(stats.WrongSalesNumber > 3) 
            {
                MobBad.enabled=true;
            }
            else if (stats.betrayedDave)
            {
                DaveBetrayed.enabled=true;
            }
            else
            {
                MobGood.enabled=true;
            }
        }
        **/
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
