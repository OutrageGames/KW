using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class OutrageSkills : Skills
{
    public override void Ability1()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        GameObject skill = Instantiate(PlayerVariables.Warrior.skillObjects[0], new Vector3(mousePos.x, mousePos.y, 0f), Quaternion.identity);
        skill.GetComponentInChildren<damager>().Damage = PlayerVariables.Warrior.skillDamages1[PlayerVariables.skillLevel1 - 1];
        //skill.GetComponentInChildren<SpriteRenderer>().color = PlayerVariables.WarriorColor;

        // ParticleSystem ps = skill.GetComponentInChildren<ParticleSystem>();
        // ps.Stop();
        // var main = ps.main;
        //main.startColor = PlayerVariables.WarriorColor;
        // ps.Play();
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