using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WarriorMenu : MonoBehaviour
{
    [SerializeField] private WarriorObject[] _warriorObjects;
    [SerializeField] private WarriorObject _chosenWarrior;
    [SerializeField] private int _warriorID;
    [SerializeField] private Image _splashArt, _warriorSprite, _mainMenuSprite;
    [SerializeField] private TMP_Text _warriorName;
    void Start()
    {
        GetComponent<CanvasGroup>().alpha = 0;
        GetComponent<CanvasGroup>().interactable = false;
        GetComponent<CanvasGroup>().blocksRaycasts = false;
        _chosenWarrior = _warriorObjects[_warriorID];
    }

    void Update()
    {
        
    }

    public void ChangeWarrior(int adder)
    {
        _warriorID += adder;

        if(_warriorID >= _warriorObjects.Length)
            _warriorID = 0;
        else if(_warriorID < 0)
            _warriorID = _warriorObjects.Length - 1;

        _chosenWarrior = _warriorObjects[_warriorID];

        _warriorName.text = _chosenWarrior.name;
        _splashArt.sprite = _chosenWarrior.splashArt;
        _mainMenuSprite.sprite = _chosenWarrior.warriorSprite;
        _warriorSprite.sprite = _chosenWarrior.warriorSprite;
    }
}
