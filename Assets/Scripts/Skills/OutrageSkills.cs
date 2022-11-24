using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using FishNet.Object;

using UnityEngine.InputSystem;

public class OutrageSkills : Skills
{
    public override void Skill1()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        float dmg = PlayerVariables.Warrior.skillDamages1[PlayerVariables.skillLevel1 - 1];
        //Debug.Log("gab?");
        int playerID = GetComponent<NetworkObject>().OwnerId;

        ServerAb1(mousePos, dmg, PlayerVariables.WarriorColor, playerID);
        Ab1(mousePos, dmg, PlayerVariables.WarriorColor, playerID);

        
    }

    [ServerRpc]
    void ServerAb1(Vector2 pos, float dmg, Color color, int playerID)
    {
        ClientAb1(pos, dmg, color, playerID);
    }
    [ObserversRpc]
    void ClientAb1(Vector2 pos, float dmg, Color color, int playerID)
    {
        if(!IsOwner)
        {
            Ab1(pos, dmg, color, playerID);
        }
    }
    void Ab1(Vector2 pos, float dmg, Color color, int playerID)
    {
        GameObject skill = Instantiate(PlayerVariables.Warrior.skillObjects[0], new Vector2(pos.x, pos.y), Quaternion.identity);
        skill.GetComponentInChildren<damager>().Damage = dmg;
        skill.GetComponentInChildren<SpriteRenderer>().color = color;
        skill.GetComponentInChildren<damager>().DamagerID = playerID;
        
        ParticleSystem ps = skill.GetComponentInChildren<ParticleSystem>();
        ps.Stop();
        var main = ps.main;
        main.startColor = color;
        ps.Play();
    }



    public override void Skill2()
    {
        GetComponentInChildren<Gun>().DamageMultiplier = PlayerVariables.Warrior.skillDamages2[PlayerVariables.skillLevel2 - 1];
        StartCoroutine(ResetTextAndDmg());
        TMP_Text dmgText = GameObject.Find("Skill2Text").GetComponent<TMP_Text>();
        dmgText.text = "x" + PlayerVariables.Warrior.skillDamages2[PlayerVariables.skillLevel2 - 1] + " dmg";

        //fire
        ServerAb2(transform, PlayerVariables.Warrior.startDuration2[PlayerVariables.skillLevel2 - 1], PlayerVariables.WarriorColor);
        Ab2(transform, PlayerVariables.Warrior.startDuration2[PlayerVariables.skillLevel2 - 1], PlayerVariables.WarriorColor);
    }

    [ServerRpc]
    void ServerAb2(Transform trs, float duration, Color color)
    {
        ClientAb2(trs, duration, color);
    }
    [ObserversRpc]
    void ClientAb2(Transform trs, float duration, Color color)
    {
        if(!IsOwner)
        {
            Ab2(trs, duration, color);
        }
    }
    void Ab2(Transform trs, float duration, Color color)
    {
        GameObject fire = Instantiate(PlayerVariables.Warrior.skillObjects[1], trs.position, Quaternion.identity, trs);
        ParticleSystem ps = fire.GetComponent<ParticleSystem>();
        ps.Stop();
        var main = ps.main;
        main.duration = duration;
        main.startColor = color;
        ps.Play();  
    }



    public IEnumerator ResetTextAndDmg()
    {
        yield return new WaitForSeconds(PlayerVariables.Warrior.startDuration2[PlayerVariables.skillLevel2 - 1]);
        GetComponentInChildren<Gun>().DamageMultiplier = 1;
        TMP_Text dmgText = GameObject.Find("Skill2Text").GetComponent<TMP_Text>();
        dmgText.text = "";
    }
}