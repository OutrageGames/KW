using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

[CreateAssetMenu(fileName = "New Warrior", menuName = "Warrior")]
public class WarriorObject : ScriptableObject
{
    public new string name;
    public int priceKW, priceO;
    public Sprite splashArt;
    public Sprite[] warriorSprites;
    public Sprite[] skillImages;

    [TextArea(3,10)]
    public string skill1, skill2;

    public string skillSet;
    public int[] dripIndexes;
    public Color[] warriorColor;
    public float[] startCooldown1, startCooldown2, skillDamages1, skillDamages2, startDuration1, startDuration2;
    public GameObject[] skillObjects;
}
