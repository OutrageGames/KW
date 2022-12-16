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
    [SerializeField] private Image _splashArt;
    [SerializeField] private Image[] _mainMenuSprites, _warriorSprites;
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
        // _mainMenuSprites[0].sprite = _chosenWarrior.warriorSprites[0];

        _warriorSprites[0].sprite = _chosenWarrior.warriorSprites[0];
        _warriorSprites[1].sprite = _chosenWarrior.warriorSprites[1];
        _warriorSprites[2].sprite = _chosenWarrior.warriorSprites[2];
        _warriorSprites[3].sprite = _chosenWarrior.warriorSprites[2];
    }
}
