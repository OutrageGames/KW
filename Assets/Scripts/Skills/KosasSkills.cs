using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using FishNet.Object;
using FishNet.Object.Synchronizing;
using UnityEngine.InputSystem;

public class KosasSkills : Skills
{
    // private bool _insideMask;
    public GameObject invParticle;
    private Animator anim;
    [SerializeField] private GameObject _headUI;
    // public bool IsInsideMask;

    public override void Skill1()
    {
        // Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // float duration = PlayerVariables.Warrior.startDuration1[PlayerVariables.skillLevel1 - 1];
        int id = GetComponent<PlayerVariables>().playerID;   
        float dmg = PlayerVariables.Warrior.skillDamages1[PlayerVariables.skillLevel1 - 1]; 
        Vector2 pos = transform.position; 

        ServerAb1(pos, dmg, PlayerVariables.WarriorColor, id);
        Ab1(pos, dmg, PlayerVariables.WarriorColor, id);
    }

    // [ServerRpc]
    // public void GetInMask(bool inMask)
    // {
    //     IsInsideMask = inMask;
    // }

    [ServerRpc]
    void ServerAb1(Vector2 pos, float dmg, Color color, int id)
    {
        ClientAb1(pos, dmg, color, id);
    }
    [ObserversRpc]
    void ClientAb1(Vector2 pos, float dmg, Color color, int id)
    {
        if(!IsOwner)
        {
            Ab1(pos, dmg, color, id);
        }
    }
    void Ab1(Vector2 pos, float dmg, Color color, int id)
    {
        GameObject skill = Instantiate(PlayerVariables.Warrior.skillObjects[0], new Vector3(pos.x, pos.y, 0f), Quaternion.identity);
        // skill.GetComponent<DestroyAfterDelay>().Delay = duration;
        skill.GetComponentInChildren<SpriteRenderer>().color = color;
        skill.GetComponentInChildren<Damager>().Damage = dmg;
        skill.GetComponentInChildren<Damager>().DamagerID = id;

        // anim = skill.GetComponent<Animator>();
        // StartCoroutine(Shrink(duration - 0.3f));        
    }

    // IEnumerator Shrink(float time)
    // {
    //     yield return new WaitForSeconds(time);
    //     anim.SetTrigger("goto");
    // }

    public override void Skill2()
    {
        ServerBecomeInvisible();
        

        TMP_Text dmgText = GameObject.Find("Skill2Text").GetComponent<TMP_Text>();
        dmgText.text = "invisible";
        StartCoroutine(ResetText());

        //Invoke(nameof(ServerBecomeVisible), PlayerVariables.Warrior.startDuration2[PlayerVariables.skillLevel2 - 1]);
        StartCoroutine(RunVisible());

        // GameObject fire = Instantiate(PlayerVariables.Warrior.skillObjects[2], transform.position, Quaternion.identity, transform);
        // ParticleSystem ps = fire.GetComponent<ParticleSystem>();
        // ps.Stop();
        // var main = ps.main;
        // main.duration = PlayerVariables.Warrior.startDuration2[PlayerVariables.skillLevel2 - 1];
        // main.startColor = PlayerVariables.WarriorColor;
        // ps.Play();
    }

    public IEnumerator ResetText()
    {
        yield return new WaitForSeconds(PlayerVariables.Warrior.startDuration2[PlayerVariables.skillLevel2 - 1]);
        TMP_Text dmgText = GameObject.Find("Skill2Text").GetComponent<TMP_Text>();
        dmgText.text = "";
    }

    [ServerRpc]
    void ServerBecomeInvisible()
    {
        ClientBecomeInvisible();
    }

    [ObserversRpc]
    void ClientBecomeInvisible()
    {
        if(!IsOwner)
        {
            // GetComponentInChildren<Animator>().GetComponentInChildren<SpriteRenderer>().enabled = false;
            foreach(Transform child in GetComponentInChildren<Animator>().gameObject.transform)
            {
                child.GetComponent<SpriteRenderer>().enabled = false;
            }
            _headUI.gameObject.SetActive(false);
            GetComponentInChildren<Gun>().gameObject.GetComponentInChildren<SpriteRenderer>().enabled = false;
        }
    }

    IEnumerator RunVisible()
    {
        yield return new WaitForSeconds(PlayerVariables.Warrior.startDuration2[PlayerVariables.skillLevel2 - 1]);
        ServerBecomeVisible();
    }

    [ServerRpc]
    void ServerBecomeVisible()
    {
        ClientBecomeVisible();
    }

    [ObserversRpc]
    void ClientBecomeVisible()
    {
        if(!IsOwner)
        {
            BecomeVisible();
        }
    }

    void BecomeVisible()
    {
        foreach(Transform child in GetComponentInChildren<Animator>().gameObject.transform)
        {
            child.GetComponent<SpriteRenderer>().enabled = true;
        }
        _headUI.gameObject.SetActive(true);
        GetComponentInChildren<Gun>().gameObject.GetComponentInChildren<SpriteRenderer>().enabled = true;
    }

    // private void OnTriggerEnter2D(Collider2D collision)
    // {
    //     if (IsOwner && collision.gameObject.name == "KosasMask(Clone)" && collision.gameObject.GetComponent<PassId>().id == OwnerId)
    //     {
    //         //invParticle = Instantiate(playerVars.playerObject.skillObjects[2], transform.position, Quaternion.identity, transform);
    //         //ParticleSystem ps = invParticle.GetComponent<ParticleSystem>();
    //         //ps.Stop();
    //         //var main = ps.main;
    //         //main.duration = playerVars.playerObject.startDuration1[playerVars.skillLevel1 - 1];
    //         //ps.Play();
    //         IsInsideMask = true;

    //         TMP_Text dmgText = GameObject.Find("Skill1Text").GetComponent<TMP_Text>();
    //         dmgText.text = "invisible";
            
    //         ServerBecomeInvisible();
    //     }
    // }

    // private void OnTriggerExit2D(Collider2D collision)
    // {
    //     if (IsOwner && collision.gameObject.name == "KosasMask(Clone)" && collision.gameObject.GetComponent<PassId>().id == OwnerId)
    //     {
    //         //Destroy(invParticle);

    //         TMP_Text dmgText = GameObject.Find("Skill1Text").GetComponent<TMP_Text>();
    //         dmgText.text = "";

    //         IsInsideMask = false;
            
    //         if(!active2)
    //         {
    //             ServerBecomeVisible();
    //         }
    //         // BecomeVisible();
    //     }
    // }
}
