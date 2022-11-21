using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorController : MonoBehaviour
{
    void Start()
    {
        //Cursor.lockState = CursorLockMode.Confined;
    }

    void Update()
    {
        Cursor.visible = false;
        transform.position = Input.mousePosition;
    }
}
