using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;

public class TrackableValues : MonoBehaviour
{
    private static TrackableValues singleton;

    public static TrackableValues Singleton
    {
        get { return singleton; }
        private set
        {
            if (singleton == null) singleton = value;
            else
            {
                Debug.Log("Singleton Already exists");
                Destroy(value.gameObject);
            }
        }
    }



    [SerializeField] public int PoliceInterest;
    [SerializeField] public float Cash;
    [SerializeField] public float TotalCash;
    [SerializeField] public int DayTarget;
    [SerializeField] public int BossTemper;
    [SerializeField] public int ItemLevel;
    [SerializeField] public int DayNum;
    [SerializeField] public int bucketPicked;

    [SerializeField] public int targetMoney;

    [SerializeField] public bool notEnoughMoney;

    [SerializeField] public bool workingWithCops;
    [SerializeField] public int copRelation;
    [SerializeField] public int copRelationDecrease;

    [SerializeField] public int numDrugsSold;
    [SerializeField] public int oldCityStatus;
    [SerializeField] public int cityDrugStatus;

    [SerializeField] public bool betrayedDave;

    //vars for NPCs
    [SerializeField] public int drugAddictLevel;
    [SerializeField] public bool drugAddictEnd;
    [SerializeField] public int illwomanLevel;
    [SerializeField] public bool illwomanEnd;
    [SerializeField] public int badmanLevel;
    [SerializeField] public bool badmanEnd;

    //stats relating to ending
    [SerializeField] public int WrongSalesNumber;

    [SerializeField] public int CorrectIDNecklace;

    [SerializeField] public bool newsActive;
    [SerializeField] AudioSource Audio1;
    [SerializeField] AudioSource Audio2;
    [SerializeField] AudioSource SFX;

    [SerializeField] public List<AudioClip>SFXList = new List<AudioClip>();
    public void Awake()
    {
        Singleton = this;
        DontDestroyOnLoad(this.gameObject);

        PoliceInterest = 35;
        TotalCash = 0;
        Cash = 0;
        DayTarget = 0;
        BossTemper = 0;
        ItemLevel = 0;
        DayNum = 1;

        CorrectIDNecklace = DayNum;

        targetMoney = 30;
        notEnoughMoney = false;

        workingWithCops = false;
        copRelation = 3;
        copRelationDecrease = 0;

        numDrugsSold = 0;
        oldCityStatus = 0;
        cityDrugStatus = 0;

        betrayedDave = false;

        // var for NPCs
        // 3 = addicted to drugs, -2 = not addicted
        drugAddictLevel = 0;
        drugAddictEnd = false;
        // 3 = child dies, 4 = good end, 5 = poor
        illwomanLevel = 0;
        illwomanEnd = false;
        // -1 = caught by police, -2 = escapes but no murder, -3 = mass murderer
        badmanLevel = 0;
        badmanEnd = false;

        newsActive = false;

        //start for ending stats
        WrongSalesNumber = 0;

        SceneManager.sceneLoaded -= OnSceneLoaded;
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public int getBucketPicked()
    {
        return bucketPicked;
    }

    public void setBucketPicked(int bucket) 
    {  
        bucketPicked = bucket; 
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {

    }

    public void SetPoliceInterest(int value)
    {
        PoliceInterest = value;
    }
    public void SetBossTemper(int value)
    {
        BossTemper = value;
    }
    public void SetDayTarget(int value)
    {
        DayTarget = value;
    }
    public void SetItemLevel(int value)
    {
        ItemLevel = value;
    }
    public void SetDayNum(int value)
    {
        DayNum = value;
    }

    public int GetPoliceInterest()
    {
        return PoliceInterest;
    }
    public int GetBossTemper()
    {
        return BossTemper;
    }
    public int GetDayTarget()
    {
        return DayTarget;
    }
    public int GetItemLevel()
    {
        return ItemLevel;
    }
    public int GetDayNum()
    {
        return DayNum;
    }

    public int getDrugAddictLevel()
    {
        return drugAddictLevel;
    }

    public int getIllWomanLevel()
    {
        return illwomanLevel;
    }

    public int getbadManLevel()
    {
        return badmanLevel;
    }

    public void setDrugAddictLevel(int value)
    {
        drugAddictLevel = value;
    }

    public int getWrongSalesChoices()
    {
        return WrongSalesNumber;
    }
    public void setWrongSalesChoices(int value)
    {
        WrongSalesNumber = value;
    }

    public void randomizeCorrectNecklace()
    {
        CorrectIDNecklace = Random.Range(1, 6);

    }

    public void playSFX(int index)
    {
        SFX.PlayOneShot(SFXList[index]);
    }
    public void switchBGM(int sourceNum)
    {
        if(sourceNum == 1)
        {
            Audio1.Stop();
            Audio2.Play();
        }
        else
        {
            Audio2.Stop();
            Audio1.Play();
        }
    }
}
