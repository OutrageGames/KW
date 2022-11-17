using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using FishNet.Object;

using UnityEngine.InputSystem;

public class OutrageSkills : Skills
{
    public override void Ability1()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float dmg = PlayerVariables.Warrior.skillDamages1[PlayerVariables.skillLevel1 - 1];

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



    public override void Ability2()
    {
        // GameObject fire = Instantiate(playerVars.WarriorObject.skillObjects[1], transform.position, Quaternion.identity, transform);
        // GameObject dmgText = Instantiate(playerVars.WarriorObject.skillObjects[2], transform.position, Quaternion.identity, transform);
        // dmgText.GetComponentInChildren<Canvas>().GetComponentInChildren<TMP_Text>().text = "x" + playerVars.WarriorObject.skillDamages2[playerVars.skillLevel2 - 1] + " DMG";
        // //fire
        // ParticleSystem ps = fire.GetComponent<ParticleSystem>();
        // ps.Stop();
        // var main = ps.main;
        // main.duration = playerVars.WarriorObject.startDuration2[playerVars.skillLevel2 - 1];
        // main.startColor = playerVars.WarriorColor;
        // ps.Play();
        // //text
        // dmgText.GetComponent<destroyAfter>().timer = playerVars.WarriorObject.startDuration2[playerVars.skillLevel2 - 1];
    }
}