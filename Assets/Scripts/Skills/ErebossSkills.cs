using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using FishNet.Object;
using UnityEngine.InputSystem;

public class ErebossSkills : Skills
{
    [SerializeField] private bool insideShield;
    private Animator anim;
    public GameObject abilityText;
    [SerializeField] float _steadyHP;

    public override void Skill1()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float duration = PlayerVariables.Warrior.startDuration1[PlayerVariables.skillLevel1 - 1];
        int playerID = GetComponent<NetworkObject>().OwnerId;

        ServerAb1(mousePos, duration, PlayerVariables.WarriorColor, playerID);
        Ab1(mousePos, duration, PlayerVariables.WarriorColor, playerID);
        
    }


    [ServerRpc]
    void ServerAb1(Vector2 pos, float duration, Color color, int playerID)
    {
        ClientAb1(pos, duration, color, playerID);
    }
    [ObserversRpc]
    void ClientAb1(Vector2 pos, float duration, Color color, int playerID)
    {
        if(!IsOwner)
        {
            Ab1(pos, duration, color, playerID);
        }
    }
    void Ab1(Vector2 pos, float duration, Color color, int playerID)
    {
        GameObject skill = Instantiate(PlayerVariables.Warrior.skillObjects[0], new Vector3(pos.x, pos.y, 0f), Quaternion.identity);
        skill.GetComponent<DestroyAfterDelay>().Delay = duration;
        skill.GetComponentInChildren<SpriteRenderer>().color = color;
        skill.GetComponent<PassId>().id = playerID;

        ParticleSystem ps = skill.GetComponentInChildren<ParticleSystem>();
        ps.Stop();
        var main = ps.main;
        main.startColor = color;
        ps.Play();

        anim = skill.GetComponentInChildren<Animator>();
        StartCoroutine(Shrink(duration - 0.5f));
    }

    IEnumerator Shrink(float time)
    {
        yield return new WaitForSeconds(time);
        anim.SetTrigger("goto");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Immune")
        {
            if(collision.GetComponentInParent<PassId>().id == GetComponent<NetworkObject>().OwnerId)
                insideShield = true;
                
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Immune")
        {
            if(collision.GetComponentInParent<PassId>().id == GetComponent<NetworkObject>().OwnerId)
            {
                insideShield = false;
            }
        }
    }

    ///////skill2/////

    public override void Skill2()
    {
        StartCoroutine(ResetText());
        TMP_Text dmgText = GameObject.Find("Skill2Text").GetComponent<TMP_Text>();
        dmgText.text = "immune";

        //shield
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

    public IEnumerator ResetText()
    {
        yield return new WaitForSeconds(PlayerVariables.Warrior.startDuration2[PlayerVariables.skillLevel2 - 1]);
        TMP_Text dmgText = GameObject.Find("Skill2Text").GetComponent<TMP_Text>();
        dmgText.text = "";
    }

    

    private void Update()
    {
        base.Update();

        if(insideShield)
        {
            if(GetComponent<PlayerHealth>().Health <= 30)
            {
                GetComponent<PlayerHealth>().BecomeImmune(GetComponent<PlayerHealth>(), true);
            }
        }            

        if (!active1)
        {
            insideShield = false;
        }

        if(insideShield == false && !active2)
        {
            GetComponent<PlayerHealth>().BecomeImmune(GetComponent<PlayerHealth>(), false);
        }

        // if (insideShield)
        // {
        //     if (_playerStatsController.CurrentPlayerHealth <= 10)
        //     {
        //         playerVars.damage = -1;
        //     }
        // }

        if (active2)
        {
            GetComponent<PlayerHealth>().BecomeImmune(GetComponent<PlayerHealth>(), true);
        }

        // if (!active2 && _playerStatsController.CurrentPlayerHealth > 10)
        // {
        //     playerVars.damage = 0;
        // }

        // if (!active2 && !active1)
        // {
        //     playerVars.damage = 0;
        // }
    }
}
