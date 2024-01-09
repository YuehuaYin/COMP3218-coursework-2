using System.Collections;
using System.Collections.Generic;
using System.IO;

using UnityEngine;
using UnityEngine.SceneManagement;


public class GameController : MonoBehaviour
{
    public GameObject barteringUI;
    Bartering bartering;
    public GameObject customerPrefab;
    public GameObject customerObject;
    SpriteRenderer customerSR;

    SpriteRenderer IDSR;

    public GameObject customerCountObject;
    TMPro.TextMeshProUGUI customerCount;
    public GameObject moneyEarnedObject;
    TMPro.TextMeshProUGUI moneyEarned;
    public float money = 0;
    public int day;
    [SerializeField] float timer = 0;
    public bool betweenCustomers = false;
    [SerializeField] float waitFor;
    public GameObject counterItem;
    public GameObject dialogBox;
    Dialog dialogScript;
    public Customers customerScript;
    public GameObject button1;
    public GameObject button2;
    public Item itemOnCounter;
    public int soldToCops = 0;
    TrackableValues stats;
    public GameObject itemObjects;

    public GameObject weedObject;
    public GameObject cocaineObject;
    public GameObject methObject;
    public GameObject redactedObject;

    public GameObject drugObject;

    public GameObject daveBadge;

    [SerializeField] GameObject newsIcon;
    [SerializeField] GameObject IDObject;

    Sprite drugSprite;

    // current items in shop
    List<string> currentItems = new List<string>();

    int currentCustomer = -1;

    // the customers that will come in
    //string[] dayScript;

    // no. of customers
    int dayLength = 5;

    // Start is called before the first frame update
    void Start()
    {
        stats = GameObject.Find("StatTracker").GetComponent<TrackableValues>();
        day = stats.GetDayNum();

        barteringUI.SetActive(false);
        disableButtons();

        dialogScript = dialogBox.GetComponent<Dialog>();
        bartering = barteringUI.GetComponent<Bartering>();

        IDSR = IDObject.GetComponent<SpriteRenderer>();

        moneyEarned = moneyEarnedObject.GetComponent<TMPro.TextMeshProUGUI>();
        moneyEarned.text = money.ToString("0.00");
        customerCount = customerCountObject.GetComponent<TMPro.TextMeshProUGUI>();

        Item[] items = itemObjects.GetComponentsInChildren<Item>();
        foreach (Item i in items)
        {
            currentItems.Add(i.getName());
        }

        daveBadge.SetActive(false);
        newsIcon.SetActive(stats.newsActive);

        setDayLength();
        newCustomer();
    }

    void Update(){
        if (betweenCustomers){
            if (timer > waitFor){
                betweenCustomers = false;
                newCustomer();
            }
            else if (timer > waitFor - 3)
            {
                daveBadge.SetActive(false);
                setIDActive(false);
                Destroy(customerObject.gameObject);
                timer += Time.deltaTime;
            }
            else {
                timer += Time.deltaTime;
            }
        }
    }

    public List<string> getItemsInShop()
    {
        return currentItems;
    }

    void setDayLength()
    {
        if ((stats.workingWithCops))
        {
            dayLength += stats.copRelation/2;
        }

        else if ((!stats.workingWithCops))
        {
            dayLength += stats.cityDrugStatus;
        }

        dayLength += day;
    }

    /**
    void newDay(){
        Debug.Log("New day: " + day.ToString());
        switch (day)
        {
        case 1:
            dayScript = new string[] {"d", "p", "r", "r", "r", "r", "r", "r"};
            break;
        case 2:
            dayScript = new string[] { "d", "p", "r", "r", "r", "r", "r", "r" };
                break;
        case 3:
            dayScript = new string[] { "d", "p", "r", "r", "r", "r", "r", "r" };
                break;
        case 4:
            dayScript = new string[] { "d", "p", "r", "r", "r", "r", "r", "r" };
                break;
        }
        newCustomer();
    }
    **/

    public void setCustomerSprite(Sprite s){
        customerSR.sprite = s;
    }
    public void setIDSprite(Sprite s)
    {
        IDSR.sprite = s;
    }

    public void setIDActive(bool active)
    {
        IDObject.SetActive(active);
    }

    public void newCustomer(){

        customerObject = Instantiate(customerPrefab, new Vector2(0f, 1.3f), Quaternion.identity);
        customerSR = customerObject.GetComponent<SpriteRenderer>();

        dialogScript.clearBox();
        currentCustomer += 1;

        if (currentCustomer >= dayLength){
            endDay();
        }
        else if (currentCustomer == 0 && !stats.betrayedDave)
        {
            customerScript = new tradyDave();
            daveBadge.SetActive(true);
        }
        else if (stats.WrongSalesNumber > 3 || (stats.copRelation < 0 && stats.copRelation != -100))
        {
            customerScript = new arrestingCop();
            stats.playSFX(1);
        }
        else if (!stats.betrayedDave && currentCustomer == 1 && stats.copRelation != -100 && !(day == 4 && !stats.workingWithCops) && !(day == 5 && !stats.workingWithCops))
        {
            customerScript = new policeman();
        }
        else if (!stats.betrayedDave && currentCustomer == 6 && day > 3 && (stats.getbadManLevel() == 0 || stats.getbadManLevel() == 1))
        {
            customerScript = new badMan();
        }
        else if (!stats.betrayedDave && currentCustomer == 2 && stats.drugAddictLevel != -2 && stats.drugAddictLevel != 3)
        {
            customerScript = new drugAddict();
        }
        else if (!stats.betrayedDave && currentCustomer == 5 && (stats.cityDrugStatus == 2 || stats.cityDrugStatus == 3)  && stats.illwomanLevel == 0)
        {
            customerScript = new illWoman();
        }
        else if (!stats.betrayedDave && currentCustomer == 5 && (stats.illwomanLevel == 1 || stats.illwomanLevel == 2))
        {
            customerScript = new illWoman();
        }
        else if (!stats.betrayedDave)
        {
            int chance = Random.Range(0, 10);
            if (stats.workingWithCops)//COP ROUTE
            {
                if (stats.DayNum == 1)
                {
                    customerScript = new randomCustomer();
                }
                else if(stats.DayNum < 3) {
                    if (chance < 5)
                    {
                        customerScript = new UndercoverCop();
                    }
                    else
                    {
                        customerScript = new randomCustomer();
                    }
                }
                else{

                    if (chance < 3)
                    {
                        customerScript = new UndercoverCop();
                    }
                    else if (chance < 5)
                    {
                        customerScript = new MobEnforcer();
                    }
                    else
                    {
                        customerScript = new randomCustomer();
                    }

                }
                
            }

            else//NOT COP ROUTE
            {
                if (stats.DayNum == 1)
                {
                    customerScript = new randomCustomer();
                }
                else if (stats.DayNum < 3)
                {
                    if (chance < 5)
                    {
                        customerScript = new MobEnforcer();
                    }
                    else
                    {
                        customerScript = new randomCustomer();
                    }
                }
                else
                {

                    if (chance < 3)
                    {
                        customerScript = new UndercoverCop();
                    }
                    else if (chance < 5)
                    {
                        customerScript = new MobEnforcer();
                    }
                    else
                    {
                        customerScript = new randomCustomer();
                    }

                }
            }
            
        }
        else
        {
            customerScript = new randomCustomer();
        }
        customerCount.text = (dayLength - currentCustomer - 1).ToString();
        customerScript.text();
        

    }

    public void addDialog(string[] dialog){
        StopAllCoroutines();
        StartCoroutine(dialogTimer(dialog));
    }

    IEnumerator dialogTimer(string [] dialog){
        foreach (var d in dialog) {
            dialogScript.addDialog(d);
            yield return new WaitForSeconds(1.2f);
        }
    }

    public void drugsAcquired()
    {
        if (stats.cityDrugStatus >= 6)
        {
            drugObject = Instantiate(redactedObject, new Vector2(-6.75f, -2.5f), Quaternion.identity);
            drugSprite = Resources.Load<Sprite>("Sprites/items/dust");
            Debug.Log(drugSprite.name);
            drugObject.GetComponent<SpriteRenderer>().sprite = drugSprite;
        }
        else if (stats.cityDrugStatus >= 4)
        {
            drugObject = Instantiate(methObject, new Vector2(-6.75f, -2.5f), Quaternion.identity);
            drugSprite = Resources.Load<Sprite>("Sprites/items/dust");
            Debug.Log(drugSprite.name);
            drugObject.GetComponent<SpriteRenderer>().sprite = drugSprite;
        }
        else if (stats.cityDrugStatus >= 2)
        {
            drugObject = Instantiate(cocaineObject, new Vector2(-6.75f, -2.5f), Quaternion.identity);
            drugSprite = Resources.Load<Sprite>("Sprites/items/meds");
            Debug.Log(drugSprite.name);
            drugObject.GetComponent<SpriteRenderer>().sprite = drugSprite;
        }
        else
        {
            drugObject = Instantiate(weedObject, new Vector2(-6.75f, -2.5f), Quaternion.identity);
            drugSprite = Resources.Load<Sprite>("Sprites/items/leaf");
            Debug.Log(drugSprite.name);
            weedObject.GetComponent<SpriteRenderer>().sprite = drugSprite;
        }
    }


    public void checkBarter(float sliderValue, string barterPriceText){
        customerScript.checkBarter(sliderValue, barterPriceText);
    }

    public void putItemOnCounter(Item item){
        itemOnCounter = item;
        counterItem.GetComponent<SpriteRenderer>().sprite = item.getSprite();
        barteringUI.SetActive(true);
        bartering.showSlider(item.getPrice());
    }

    public void removeItemFromCounter()
    {
        barteringUI.SetActive(false);
        bartering.resetSlider();
        counterItem.GetComponent<SpriteRenderer>().sprite = null;
    }

    public void customerLeaves(float waitTime){
        Debug.Log("leaving");
        stats.playSFX(2);
        removeItemFromCounter();
        timer = 0;
        disableButtons();
        waitFor = waitTime;
        betweenCustomers = true;
    }

    public void barteringFailed(){
        customerScript.barterFailed();
    }

    public void barteringComplete(float earned){
        customerScript.barterComplete();
        stats.playSFX(4);
        earnMoney(earned);
    }

    public void earnMoney(float earned)
    {
        money += earned;
        moneyEarned.text = money.ToString("0.00");
    }

    public void endDay(){

        stats.Cash = money;
        stats.TotalCash += money;
        /** TODO FIX VALUES **/

        if (money < stats.targetMoney)
        {
            stats.notEnoughMoney = true;
            SceneManager.LoadScene("GameOverScreen");
        }
        
        else if (day < 5)
        {
            stats.targetMoney += 15;
            SceneManager.LoadScene("EndDayScene");
        }
        else
        {
            SceneManager.LoadScene("GameOverScreen");
        }
    }

    public void button1Clicked(){
        stats.playSFX(0);
        customerScript.button1Clicked();
    }

    public void button2Clicked()
    {
        stats.playSFX(0);
        customerScript.button2Clicked();
    }

    public void button1SetText(string text){
        button1.transform.GetChild(0).gameObject.GetComponent<TMPro.TextMeshProUGUI>().text = text;
        button1.SetActive(true);
    }

    public void button2SetText(string text){
        button2.transform.GetChild(0).gameObject.GetComponent<TMPro.TextMeshProUGUI>().text = text;
        button2.SetActive(true);
    }

    public void disableButtons()
    {
        button1.SetActive(false);
        button2.SetActive(false);
    }

    public void clearDialogBox()
    {
        dialogScript.clearBox();
    }

    public void setNewsActive()
    {
        stats.newsActive = true;
        newsIcon.SetActive(true);
    }
}
