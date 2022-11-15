using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class chooseWarrior : MonoBehaviour
{
    [SerializeField] private TMP_Text warriorName, price;
    public GameObject choose;
    private checkIfLocked getIndex;

    void Start()
    {
        getIndex = GetComponent<checkIfLocked>();

        var asd = Shop.items[getIndex.itemIndex];
        //GetComponent<Image>().sprite = asd.ItemSprite;
        //warriorName.text = asd.playerObj.name;
        //price.text = asd.ItemPriceKW + " KW / " + asd.ItemPriceO + " O";
    }
}
