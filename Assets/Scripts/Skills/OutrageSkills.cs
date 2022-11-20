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
        Debug.Log("gab?");

        ServerAb1(mousePos, dmg);
        Ab1(mousePos, dmg);

        //skill.GetComponentInChildren<SpriteRenderer>().color = PlayerVariables.WarriorColor;

        // ParticleSystem ps = skill.GetComponentInChildren<ParticleSystem>();
        // ps.Stop();
        // var main = ps.main;
        //main.startColor = PlayerVariables.WarriorColor;
        // ps.Play();
    }

    [ServerRpc]
    void ServerAb1(Vector2 pos, float dmg)
    {
        ClientAb1(pos, dmg);
    }
    [ObserversRpc]
    void ClientAb1(Vector2 pos, float dmg)
    {
        if(!IsOwner)
        {
            Ab1(pos, dmg);
        }
    }
    void Ab1(Vector2 pos, float dmg)
    {
        GameObject skill = Instantiate(PlayerVariables.Warrior.skillObjects[0], new Vector2(pos.x, pos.y), Quaternion.identity);
        skill.GetComponentInChildren<damager>().Damage = dmg;
    }



    public override void Skill2()
    {
        
        // GameObject fire = Instantiate(PlayerVariables.Warrior.skillObjects[1], transform.position, Quaternion.identity, transform);
        // GameObject dmgText = Instantiate(PlayerVariables.Warrior.skillObjects[2], transform.position, Quaternion.identity, transform);
        // dmgText.GetComponentInChildren<Canvas>().GetComponentInChildren<TMP_Text>().text = "x" + PlayerVariables.Warrior.skillDamages2[PlayerVariables.skillLevel2 - 1] + " DMG";
        //fire
        // ParticleSystem ps = fire.GetComponent<ParticleSystem>();
        // ps.Stop();
        // var main = ps.main;
        // main.duration = PlayerVariables.Warrior.startDuration2[PlayerVariables.skillLevel2 - 1];
        // main.startColor = PlayerVariables.Warrior.warriorColor;
        // ps.Play();
        //text
        //dmgText.GetComponent<destroyAfter>().timer = PlayerVariables.Warrior.startDuration2[PlayerVariables.skillLevel2 - 1];
    }
}