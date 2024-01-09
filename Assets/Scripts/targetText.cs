using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class targetText : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI txt;
    [SerializeField] TrackableValues stats;
    // Start is called before the first frame update
    private void Start()
    {
        txt.text = stats.GetDayTarget().ToString();
    }
}
