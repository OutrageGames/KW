
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopItem
{
    public int ItemID, ItemPriceKW, ItemPriceO, dripID;
    public Sprite ItemSprite;
    public string ItemName;
    public string gunColorHex;
    public WarriorObject playerObj;
    public GunObject gunObj;
}

public static class Shop
{
    public static Dictionary<int, ShopItem> items = new Dictionary<int, ShopItem>()
    {
        {0,  new ShopItem    { ItemID = 0,   dripID = 0, playerObj = Resources.Load<WarriorObject>("Objects/Warriors/Outrage") } },
        {1,  new ShopItem    { ItemID = 1,   dripID = 1, playerObj = Resources.Load<WarriorObject>("Objects/Warriors/Erebus") } },
        {2,  new ShopItem    { ItemID = 2,   dripID = 2, playerObj = Resources.Load<WarriorObject>("Objects/Warriors/Scientist")   } },
        {3,  new ShopItem    { ItemID = 3,   dripID = 3, playerObj = Resources.Load<WarriorObject>("Objects/Warriors/Loker")   } },
        {4,  new ShopItem    { ItemID = 4,   dripID = 4, playerObj = Resources.Load<WarriorObject>("Objects/Warriors/Juice")   } },
        {5,  new ShopItem    { ItemID = 5,   dripID = 5, playerObj = Resources.Load<WarriorObject>("Objects/Warriors/Kotro")   } },
        {6,  new ShopItem    { ItemID = 6,   dripID = 6, playerObj = Resources.Load<WarriorObject>("Objects/Warriors/Trog")    } },
        {7,  new ShopItem    { ItemID = 7,   ItemName = "[DRIP] styled ereboss",       ItemPriceO = 540, dripID = 9   } },
        {8,  new ShopItem    { ItemID = 8,   ItemName = "[DRIP] jean ereboss",         ItemPriceO = 750, dripID = 10  } },
        {9,  new ShopItem    { ItemID = 9,   ItemName = "[DRIP] thug outrage",         ItemPriceO = 350, dripID = 11  } },
        {10, new ShopItem    { ItemID = 10,  ItemName = "[DRIP] deer outrage",         ItemPriceO = 240, dripID = 12  } },
        {11, new ShopItem    { ItemID = 11,  ItemName = "[DRIP] sad kosas",            ItemPriceO = 460, dripID = 7   } },
        {12, new ShopItem    { ItemID = 12,  ItemName = "[DRIP] horny kosas",          ItemPriceO = 340, dripID = 8   } },
        {13, new ShopItem    { ItemID = 13,  ItemName = "[DRIP] potato juice",         ItemPriceO = 480, dripID = 13  } },
        {14, new ShopItem    { ItemID = 14,  ItemName = "[DRIP] baloon juice",         ItemPriceO = 250, dripID = 14  } },
        {15, new ShopItem    { ItemID = 15,  ItemName = "[DRIP] banana loker",         ItemPriceO = 360, dripID = 15  } },
        {16, new ShopItem    { ItemID = 16,  ItemName = "[DRIP] rugby loker",          ItemPriceO = 320, dripID = 16  } },
        {17, new ShopItem    { ItemID = 17,  ItemName = "[DRIP] sync trog",            ItemPriceO = 550, dripID = 17  } },
        {18, new ShopItem    { ItemID = 18,  ItemName = "[DRIP] cyc-22 trog",          ItemPriceO = 740, dripID = 18  } },
        {19, new ShopItem    { ItemID = 19,  ItemName = "[SALMON] ak - 47", gunObj = Resources.Load<GunObject>("Objects/Guns/Ak"), ItemPriceO = 10, gunColorHex = "FA8072" } },
        {20, new ShopItem    { ItemID = 20,  ItemName = "[BROWN] ak - 47", gunObj = Resources.Load<GunObject>("Objects/Guns/Ak"), ItemPriceO = 10, gunColorHex = "966B3F"} },
        {21, new ShopItem    { ItemID = 21,  ItemName = "[MINT] ak - 47", gunObj = Resources.Load<GunObject>("Objects/Guns/Ak"), ItemPriceO = 10, gunColorHex = "A5FF7C"} },
        {22, new ShopItem    { ItemID = 22,  ItemName = "[LIGHTBLUE] ak - 47", ItemPriceO = 10, gunColorHex = "8FFFF7", gunObj = Resources.Load<GunObject>("Objects/Guns/Ak") } },
        {23, new ShopItem    { ItemID = 23,  ItemName = "[VIOLET] ak - 47", ItemPriceO = 10, gunColorHex = "EE82EE",  gunObj = Resources.Load<GunObject>("Objects/Guns/Ak") } },
        {24, new ShopItem    { ItemID = 24,  ItemName = "[RED] ak - 47", ItemPriceO = 10, gunColorHex = "FF0000",   gunObj = Resources.Load<GunObject>("Objects/Guns/Ak") } },
        {25, new ShopItem    { ItemID = 25,  ItemName = "[ORANGE] ak - 47", ItemPriceO = 10, gunColorHex = "FF7800",   gunObj = Resources.Load<GunObject>("Objects/Guns/Ak") } },
        {26, new ShopItem    { ItemID = 26,  ItemName = "[GREEN] ak - 47", ItemPriceO = 10, gunColorHex = "00FF00",   gunObj = Resources.Load<GunObject>("Objects/Guns/Ak") } },
        {27, new ShopItem    { ItemID = 27,  ItemName = "[CYAN] ak - 47", ItemPriceO = 10, gunColorHex = "00FFFF",   gunObj = Resources.Load<GunObject>("Objects/Guns/Ak") } },
        {28, new ShopItem    { ItemID = 28,  ItemName = "[BLUE] ak - 47", ItemPriceO = 10, gunColorHex = "0000FF",  gunObj = Resources.Load<GunObject>("Objects/Guns/Ak") } },
        {29, new ShopItem    { ItemID = 29,  ItemName = "[PURPLE] ak - 47", ItemPriceO = 10, gunColorHex = "9400FF",   gunObj = Resources.Load<GunObject>("Objects/Guns/Ak") } },
        {30, new ShopItem    { ItemID = 30,  ItemName = "[PINK] ak - 47", ItemPriceO = 10, gunColorHex = "FF00F2",   gunObj = Resources.Load<GunObject>("Objects/Guns/Ak") } },
        {31, new ShopItem    { ItemID = 31,  ItemName = "[YELLOW] ak - 47", ItemPriceO = 10, gunColorHex = "FFFF00",   gunObj = Resources.Load<GunObject>("Objects/Guns/Ak") } },
        {32, new ShopItem    { ItemID = 32,  ItemName = "[WHITE] ak - 47", ItemPriceO = 10, gunColorHex = "FFFFFF",  gunObj = Resources.Load<GunObject>("Objects/Guns/Ak") } },
        {33, new ShopItem    { ItemID = 33,  ItemName = "[TURQUOISE] ak - 47", ItemPriceO = 10, gunColorHex = "30D5C8",  gunObj = Resources.Load<GunObject>("Objects/Guns/Ak") } },
        {34, new ShopItem    { ItemID = 34,  ItemName = "kar 98", ItemPriceKW = 23480, ItemPriceO = 1800, gunColorHex = "966B3F",  gunObj = Resources.Load<GunObject>("Objects/Guns/Kar98") } },
        {35, new ShopItem    { ItemID = 35,  ItemName = "smith", ItemPriceKW = 16850, ItemPriceO = 1300, gunColorHex = "EE82EE", gunObj = Resources.Load<GunObject>("Objects/Guns/Smith") } },
        {36, new ShopItem    { ItemID = 36,  ItemName = "uzi", ItemPriceKW = 12500, ItemPriceO = 1000, gunColorHex = "8FFFF7",  gunObj = Resources.Load<GunObject>("Objects/Guns/Uzi") } },
        {37, new ShopItem    { ItemID = 37,  ItemName = "ak - 47", ItemPriceKW = 6500, ItemPriceO = 2, gunColorHex = "404098",   gunObj = Resources.Load<GunObject>("Objects/Guns/Ak") } },
        {38, new ShopItem    { ItemID = 38,  ItemName = "[GREEN] uzi",  ItemPriceO = 10, gunColorHex = "00FF00",  gunObj = Resources.Load<GunObject>("Objects/Guns/Uzi") } },
        {39, new ShopItem    { ItemID = 39,  ItemName = "[CYAN] uzi",  ItemPriceO = 10, gunColorHex = "00FFFF",  gunObj = Resources.Load<GunObject>("Objects/Guns/Uzi") } },
        {40, new ShopItem    { ItemID = 40,  ItemName = "[PURPLE] uzi",  ItemPriceO = 10, gunColorHex = "9400FF",  gunObj = Resources.Load<GunObject>("Objects/Guns/Uzi") } },
        {41, new ShopItem    { ItemID = 41,  ItemName = "[RED] smith",  ItemPriceO = 10, gunColorHex = "FF0000", gunObj = Resources.Load<GunObject>("Objects/Guns/Smith") } },
        {42, new ShopItem    { ItemID = 42,  ItemName = "[ORANGE] smith",  ItemPriceO = 10, gunColorHex = "FF7800",  gunObj = Resources.Load<GunObject>("Objects/Guns/Smith") } },
        {43, new ShopItem    { ItemID = 43,  ItemName = "[GREEN] smith",  ItemPriceO = 10, gunColorHex = "00FF00",  gunObj = Resources.Load<GunObject>("Objects/Guns/Smith") } },
        {44, new ShopItem    { ItemID = 44,  ItemName = "[BLUE] smith",  ItemPriceO = 10, gunColorHex = "0000FF",  gunObj = Resources.Load<GunObject>("Objects/Guns/Smith") } },
        {45, new ShopItem    { ItemID = 45,  ItemName = "[PURPLE] smith",  ItemPriceO = 10, gunColorHex = "9400FF",  gunObj = Resources.Load<GunObject>("Objects/Guns/Smith") } },
        {46, new ShopItem    { ItemID = 46,  ItemName = "[PINK] smith",  ItemPriceO = 10, gunColorHex = "FF00F2", gunObj = Resources.Load<GunObject>("Objects/Guns/Smith") } },
        {47, new ShopItem    { ItemID = 47,  ItemName = "[DRIP] green outrage",        ItemPriceO = 240, dripID = 19 } },
        {48, new ShopItem    { ItemID = 48,  ItemName = "[DRIP] love outrage",         ItemPriceO = 420, dripID = 20 } },
        {49, new ShopItem    { ItemID = 49,  ItemName = "[DRIP] raiden outrage",       ItemPriceO = 250, dripID = 21 } },
        {50, new ShopItem    { ItemID = 50,  ItemName = "[DRIP] robot outrage",        ItemPriceO = 180, dripID = 22 } },
        {51, new ShopItem    { ItemID = 51,  ItemName = "[DRIP] slime outrage",        ItemPriceO = 160, dripID = 23 } },
        {52, new ShopItem    { ItemID = 52,  ItemName = "[GREEN] kar 98", ItemPriceO = 12, gunColorHex = "00FF00",  gunObj = Resources.Load<GunObject>("Objects/Guns/Kar98") } },
        {53, new ShopItem    { ItemID = 53,  ItemName = "[CYAN] kar 98", ItemPriceO = 12, gunColorHex = "00FFFF",  gunObj = Resources.Load<GunObject>("Objects/Guns/Kar98") } },
        {54, new ShopItem    { ItemID = 54,  ItemName = "[PURPLE] kar 98", ItemPriceO = 12, gunColorHex = "9400FF",  gunObj = Resources.Load<GunObject>("Objects/Guns/Kar98") } },
        {55, new ShopItem    { ItemID = 55,  ItemName = "[RED] kar 98", ItemPriceO = 12, gunColorHex = "FF0000",  gunObj = Resources.Load<GunObject>("Objects/Guns/Kar98") } },
        {56, new ShopItem    { ItemID = 56,  ItemName = "[ORANGE] kar 98", ItemPriceO = 12, gunColorHex = "FF7800",  gunObj = Resources.Load<GunObject>("Objects/Guns/Kar98") } },
        {57, new ShopItem    { ItemID = 57,  ItemName = "[DRIP] zombie outrage",        ItemPriceO = 340, dripID = 24 } },




    };
}