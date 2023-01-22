using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using FishNet.Object;
using UnityEngine.InputSystem;

public class LokerSkills : Skills
{
    private float _normalSpeed, _normalJumpSpeed, _normalFirerate, _normalReloadStarttime, _normalReloadtime;
    private PlayerMovement playerMovement;

    public override void Skill1()
    {
        //Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        ServerAb1(PlayerVariables.WarriorColor);
        Ab1(PlayerVariables.WarriorColor);
    }
    IEnumerator Shrink(Animator anim, float time)
    {
        yield return new WaitForSeconds(time);
        anim.SetTrigger("goto");
    }
    [ServerRpc]
    void ServerAb1(Color color)
    {
        ClientAb1(color);
    }
    [ObserversRpc]
    void ClientAb1(Color color)
    {
        if(!IsOwner)
        {
            Ab1(color);
        }
    }
    void Ab1(Color color)
    {
        GameObject skill = Instantiate(PlayerVariables.Warrior.skillObjects[0], new Vector2(transform.position.x, transform.position.y), Quaternion.identity);

        skill.GetComponent<Trampoline>().jumpSpeed = PlayerVariables.Warrior.skillDamages1[PlayerVariables.skillLevel1 - 1];
        skill.GetComponent<DestroyAfterDelay>().Delay = PlayerVariables.Warrior.startDuration1[PlayerVariables.skillLevel1 - 1];

        Animator anim = skill.GetComponentInChildren<Animator>();
        StartCoroutine(Shrink(anim, PlayerVariables.Warrior.startDuration1[PlayerVariables.skillLevel1 - 1] - 0.5f));
    }

    public override void Skill2()
    {
        playerMovement = GetComponent<PlayerMovement>();
        Gun gun = GetComponentInChildren<Gun>();

        _normalSpeed = playerMovement.MoveSpeed;
        _normalJumpSpeed = playerMovement.JumpSpeed;
        _normalFirerate = gun.fireRate;
        _normalReloadStarttime = gun.ReloadStartTime;
        _normalReloadtime = gun.ReloadTime;

        gun.fireRate /= PlayerVariables.Warrior.skillDamages2[PlayerVariables.skillLevel2 - 1];
        gun.ReloadStartTime /= PlayerVariables.Warrior.skillDamages2[PlayerVariables.skillLevel2 - 1];
        gun.ReloadTime /= PlayerVariables.Warrior.skillDamages2[PlayerVariables.skillLevel2 - 1];
        gun.GetComponentInChildren<Animator>().speed *= PlayerVariables.Warrior.skillDamages2[PlayerVariables.skillLevel2 - 1];
        playerMovement.MoveSpeed *= PlayerVariables.Warrior.skillDamages2[PlayerVariables.skillLevel2 - 1];
        playerMovement.JumpSpeed *= PlayerVariables.Warrior.skillDamages2[PlayerVariables.skillLevel2 - 1];

        // playerMovement.MoveSpeed += PlayerVariables.Warrior.skillDamages2[PlayerVariables.skillLevel2 - 1];
        // playerMovement.JumpSpeed += PlayerVariables.Warrior.skillDamages2[PlayerVariables.skillLevel2 - 1];

        StartCoroutine(NormalSpeed(PlayerVariables.Warrior.startDuration2[PlayerVariables.skillLevel2 - 1]));

        ServerTrail(3);
        Trail(3);

        StartCoroutine(ResetText());
        TMP_Text dmgText = GameObject.Find("Skill2Text").GetComponent<TMP_Text>();
        if(PlayerVariables.skillLevel2 == 1)
            dmgText.text = "fast";
        else if(PlayerVariables.skillLevel2 == 2)
            dmgText.text = "faster";
        else
            dmgText.text = "the fasterestest";
    }

    IEnumerator NormalSpeed(float timer)
    {
        yield return new WaitForSeconds(timer);
        playerMovement = GetComponent<PlayerMovement>();
        Gun gun = GetComponentInChildren<Gun>();
        playerMovement.MoveSpeed = _normalSpeed;
        playerMovement.JumpSpeed = _normalJumpSpeed;
        gun.fireRate = _normalFirerate;
        gun.ReloadStartTime = _normalReloadStarttime;
        gun.ReloadTime = _normalReloadtime;
        gun.GetComponentInChildren<Animator>().speed = 1f;
        ServerTrail(0.1f);
        Trail(0.1f);
    }

    [ServerRpc]
    void ServerTrail(float time)
    {
        ClientTrail(time);
    }
    [ObserversRpc]
    void ClientTrail(float time)
    {
        if(!IsOwner)
        {
            Trail(time);
        }
    }

    void Trail(float time)
    {
        GetComponent<TrailRenderer>().time = time;
    }

    

    IEnumerator ResetText()
    {
        yield return new WaitForSeconds(PlayerVariables.Warrior.startDuration2[PlayerVariables.skillLevel2 - 1]);
        TMP_Text dmgText = GameObject.Find("Skill2Text").GetComponent<TMP_Text>();
        dmgText.text = "";
    }
}
