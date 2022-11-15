using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class userController : MonoBehaviour
{
    public static Dictionary<int, bool> UnlockedItems;

    [SerializeField] private string _userName;
    [SerializeField] WarriorObject _warriorObject;
    [SerializeField] GunObject _gunObject;
    [SerializeField] private int _dripIndex, _gunColorIndex;


    public string UserName { get => _userName; }
    public int DripIndex { get => _dripIndex; set => _dripIndex = value; }
    public int GunColorIndex { get => _gunColorIndex; set => _gunColorIndex = value; }
    public WarriorObject WarriorObject { get => _warriorObject; set => _warriorObject = value; }
    public GunObject GunObject { get => _gunObject; set => _gunObject = value; }

    private void Start()
    {
        DontDestroyOnLoad(gameObject);

        UnlockedItems = new Dictionary<int, bool>();

        for (int i = 0; i <= 500; i++)
        {
            UnlockedItems.Add(i, false);
        }

        UnlockedItems[0] = true;
        UnlockedItems[37] = true;
    }
}