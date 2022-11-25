using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using FishNet.Object;

public class KosasSkills : Skills
{
    public bool insideMask;
    public GameObject invParticle;
    private Animator anim;
    [SerializeField] private GameObject _headUI;

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
        skill.GetComponent<SpriteRenderer>().color = color;
        skill.GetComponent<PassId>().id = playerID;
        anim = skill.GetComponent<Animator>();
        StartCoroutine(Shrink(duration - 0.5f));        
    }

    IEnumerator Shrink(float time)
    {
        yield return new WaitForSeconds(time);
        anim.SetTrigger("goto");
    }

    public override void Skill2()
    {
        ServerBecomeInvisible();
        

        TMP_Text dmgText = GameObject.Find("Skill2Text").GetComponent<TMP_Text>();
        dmgText.text = "invisible";
        StartCoroutine(ResetText());

        Invoke(nameof(ServerBecomeVisible), PlayerVariables.Warrior.startDuration2[PlayerVariables.skillLevel2 - 1]);

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
            GetComponentInChildren<SpriteRenderer>().enabled = false;
            _headUI.gameObject.SetActive(false);
            GetComponentInChildren<Gun>().gameObject.GetComponentInChildren<SpriteRenderer>().enabled = false;
        }
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
            GetComponentInChildren<SpriteRenderer>().enabled = true;
            _headUI.gameObject.SetActive(true);
            GetComponentInChildren<Gun>().gameObject.GetComponentInChildren<SpriteRenderer>().enabled = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (IsOwner && collision.gameObject.name == "KosasMask(Clone)" && collision.gameObject.GetComponent<PassId>().id == OwnerId)
        {
            //invParticle = Instantiate(playerVars.playerObject.skillObjects[2], transform.position, Quaternion.identity, transform);
            //ParticleSystem ps = invParticle.GetComponent<ParticleSystem>();
            //ps.Stop();
            //var main = ps.main;
            //main.duration = playerVars.playerObject.startDuration1[playerVars.skillLevel1 - 1];
            //ps.Play();

            TMP_Text dmgText = GameObject.Find("Skill1Text").GetComponent<TMP_Text>();
            dmgText.text = "invisible";
            
            ServerBecomeInvisible();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (IsOwner && collision.gameObject.name == "KosasMask(Clone)" && collision.gameObject.GetComponent<PassId>().id == OwnerId)
        {
            //Destroy(invParticle);

            TMP_Text dmgText = GameObject.Find("Skill1Text").GetComponent<TMP_Text>();
            dmgText.text = "";

            if(!active2)
            {
                ServerBecomeVisible();
            }
            // BecomeVisible();
        }
    }

    // private void Update()
    // {
    //     base.Update();

    //     // if(insideMask)
    //     // {
    //     //     BecomeInvisible();
    //     // }

    //     if (active1)
    //     {
    //         // if (!playerVars.IsOwner)
    //         // {
    //         //     if (insideMask)
    //         //     {
    //         //         GetComponentInChildren<SpriteRenderer>().enabled = false;
    //         //         // GetComponent<playerVariables>().headUI.gameObject.SetActive(false);
    //         //         GetComponentInChildren<Gun>().gameObject.GetComponentInChildren<SpriteRenderer>().enabled = false;
    //         //     }
    //         //     else
    //         //     {
    //         //         if (!active2)
    //         //         {
    //         //             GetComponentInChildren<SpriteRenderer>().enabled = true;
    //         //             // GetComponent<playerVariables>().headUI.gameObject.SetActive(true);
    //         //             GetComponentInChildren<Gun>().gameObject.GetComponentInChildren<SpriteRenderer>().enabled = true;
    //         //         }
    //         //     }
    //         // }
    //     }
    // }
}
