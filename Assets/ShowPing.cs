using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FishNet.Object;
using TMPro;

public class ShowPing : NetworkBehaviour
{
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<TMP_Text>().text = "Ping: " + TimeManager.RoundTripTime.ToString();
    }
}
