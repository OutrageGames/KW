using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class checkIfLocked : MonoBehaviour
{
    public int itemIndex;

    void Update()
    {
        if (userController.UnlockedItems[itemIndex] == true)
        {
            GetComponent<Button>().interactable = false;

            foreach (Transform child in transform)
            {
                if (child.name == "locket")
                {
                    GameObject.Destroy(child.gameObject);
                }
                if (child.name == "price")
                {
                    GameObject.Destroy(child.gameObject);
                }
                if (child.name == "chooseWarrior")
                {
                    child.gameObject.SetActive(true);
                }
            }
        }
    }
}
