using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeLeftScript : MonoBehaviour
{
    public GameObject TargetBattery;
    public Text output;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        output.text = $"Time left: {TargetBattery.GetComponent<BatteryScript>().TimeLeft}\nCharge: {TargetBattery.GetComponent<BatteryScript>().charge}";
    }
}
