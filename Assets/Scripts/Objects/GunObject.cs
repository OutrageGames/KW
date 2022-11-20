using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Gun", menuName = "Gun")]
public class GunObject : ScriptableObject
{
    public new string name;
    public Sprite UIImage;
    public GameObject gun;
    public int magBullets;
    public int[] colorIndexes;
}
