using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using FishNet.Object;
using UnityEngine.InputSystem;

public class JuiceSkills : Skills
{
    public GameObject BigEnemy;

    public override void Skill1()
    {
        GameObject skill = Instantiate(PlayerVariables.Warrior.skillObjects[0], GetComponentInChildren<Gun>().spawnPoint.position,
            Quaternion.Euler(new Vector3(0f, 0f, GetComponentInChildren<Gun>().gameObject.transform.rotation.eulerAngles.z)));

        skill.GetComponent<Rigidbody2D>().velocity = skill.transform.right * 40f;
        skill.GetComponent<JuiceSkillshot>().Size = PlayerVariables.Warrior.skillDamages1[PlayerVariables.skillLevel1 - 1];
        skill.GetComponent<JuiceSkillshot>().JuiceSkills = this;
        // skill.GetComponentInChildren<SpriteRenderer>().color = new Color(PlayerVariables.WarriorColor.r, PlayerVariables.WarriorColor.g, PlayerVariables.WarriorColor.b, 1f);
        // skill.GetComponent<TrailRenderer>().startColor = new Color(PlayerVariables.WarriorColor.r, PlayerVariables.WarriorColor.g, PlayerVariables.WarriorColor.b, 1f);
        // skill.GetComponent<TrailRenderer>().endColor = new Color(0f, 0f, 0f, 0f);

        // ParticleSystem ps = skill.GetComponent<ParticleSystem>();
        // ps.Stop();
        // var main = ps.main;
        // main.startColor = new Color(playerVars.WarriorColor.r, playerVars.WarriorColor.g, playerVars.WarriorColor.b, 0.4f);
        // ps.Play();

    }

    public IEnumerator MakeSmallAgain(Transform trs)
    {
        yield return new WaitForSeconds(PlayerVariables.Warrior.startDuration1[PlayerVariables.skillLevel1 - 1]);
        ServerMakeSmall(trs);
        MakeSmall(trs);
    }

    [ServerRpc]
    public void ServerMakeSmall(Transform trs)
    {
        ClientMakeSmall(trs);
    }
    [ObserversRpc]
    void ClientMakeSmall(Transform trs)
    {
        if(!IsOwner)
        {
            MakeSmall(trs);
        }
    }
    void MakeSmall(Transform trs)
    {
        trs.localScale = new Vector2(1f, 1f);
    }

    // IEnumerator BecomeBigAgain(float timer)
    // {
    //     yield return new WaitForSeconds(timer);

    //     transform.localScale = new Vector2(1f, 1f);
    //     // GetComponentInChildren<Gun>().transform.localScale = new Vector2(1f, 1f);
    // }

    [ServerRpc]
    public void ServerMakeBig(Transform trs, float scale)
    {
        ClientMakeBig(trs, scale);
    }
    [ObserversRpc]
    void ClientMakeBig(Transform trs, float scale)
    {
        if(!IsOwner)
        {
            MakeBig(trs, scale);
        }
    }
    void MakeBig(Transform trs, float scale)
    {
        trs.localScale = new Vector2(scale, scale);
    }





    //skill2

    public override void Skill2()
    {
        //Vector2 scaleTmp = transform.localScale;
        float scale = PlayerVariables.Warrior.skillDamages2[PlayerVariables.skillLevel2 - 1];

        ServerAb2(transform, scale);
        Ab2(transform, scale);

        StartCoroutine(CorBecomeBig(transform));

        //GetComponentInChildren<Gun>().transform.localScale = scaleTmp * PlayerVariables.Warrior.skillDamages2[PlayerVariables.skillLevel2 - 1];

        StartCoroutine(ResetText());
        TMP_Text dmgText = GameObject.Find("Skill2Text").GetComponent<TMP_Text>();
        dmgText.text = "small";        

        
    }

    //become small
    [ServerRpc]
    void ServerAb2(Transform trs, float scale)
    {
        ClientAb2(trs, scale);
    }
    [ObserversRpc]
    void ClientAb2(Transform trs, float scale)
    {
        if(!IsOwner)
        {
            Ab2(trs, scale);
        }
    }
    void Ab2(Transform trs, float scale)
    {
        trs.localScale = trs.localScale / scale;
    }

    //become big
    IEnumerator CorBecomeBig(Transform trs)
    {
        yield return new WaitForSeconds(PlayerVariables.Warrior.startDuration2[PlayerVariables.skillLevel2 - 1]);
        ServerBecomeBig(trs);
        BecomeBig(trs);
    }

    [ServerRpc]
    void ServerBecomeBig(Transform trs)
    {
        ClientBecomeBig(trs);
    }
    [ObserversRpc]
    void ClientBecomeBig(Transform trs)
    {
        if(!IsOwner)
        {
            BecomeBig(trs);
        }
    }
    void BecomeBig(Transform trs)
    {
        trs.localScale = new Vector2(1f, 1f);
    }


    IEnumerator ResetText()
    {
        yield return new WaitForSeconds(PlayerVariables.Warrior.startDuration2[PlayerVariables.skillLevel2 - 1]);
        TMP_Text dmgText = GameObject.Find("Skill2Text").GetComponent<TMP_Text>();
        dmgText.text = "";
    }

    
}
