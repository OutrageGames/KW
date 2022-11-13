using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    [SerializeField] private NetworkHudCanvases _networkScript;
    void Start()
    {
        //Invoke(nameof(asd), .5f);
        _networkScript.OnClick_Server();
    }

    void asd()
    {
        _networkScript.OnClick_Server();
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
