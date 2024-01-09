using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleScreen : MonoBehaviour
{
    [SerializeField] TrackableValues stats;
    public GameObject creditsPanel;

    // Start is called before the first frame update
    void Start()
    {
        creditsPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        stats.playSFX(0);
        SceneManager.LoadScene("Store");
    }

    public void creditsbutton()
    {
        creditsPanel.SetActive(true);
    }

    public void closecredits()
    {
        creditsPanel.SetActive(false);
    }

}
