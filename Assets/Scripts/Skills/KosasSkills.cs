using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class KosasSkills : Skills
{
    public bool insideMask;
    public GameObject abilityText, invParticle;
    private Animator anim;

    public override void Skill1()
    {
        // Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // GameObject skill = Instantiate(playerVars.WarriorObject.skillObjects[0], new Vector3(mousePos.x, mousePos.y, 0f), Quaternion.identity);
        // skill.GetComponent<destroyAfter>().timer = playerVars.WarriorObject.startDuration1[playerVars.skillLevel1 - 1];
        // skill.GetComponent<SpriteRenderer>().color = playerVars.WarriorColor;
        // anim = skill.GetComponent<Animator>();
        // StartCoroutine(shrink(playerVars.WarriorObject.startDuration1[playerVars.skillLevel1 - 1] - 0.5f));
    }

    IEnumerator shrink(float time)
    {
        yield return new WaitForSeconds(time);
        anim.SetTrigger("goto");
    }

    public override void Skill2()
    {
        // if (!playerVars.IsOwner)
        // {
        //     GetComponentInChildren<SpriteRenderer>().enabled = false;
        //     // GetComponent<playerVariables>().headUI.gameObject.SetActive(false);
        //     GetComponentInChildren<Gun>().gameObject.GetComponentInChildren<SpriteRenderer>().enabled = false;
        // }

        // GameObject abilityText2 = Instantiate(playerVars.WarriorObject.skillObjects[3], transform.position, Quaternion.identity);
        // abilityText2.GetComponentInChildren<Canvas>().GetComponentInChildren<TMP_Text>().text = "INVISIBLE";
        // abilityText2.GetComponent<destroyAfter>().timer = playerVars.WarriorObject.startDuration2[playerVars.skillLevel2 - 1];
        // StartCoroutine(becomeVisibleAgain(playerVars.WarriorObject.startDuration2[playerVars.skillLevel2 - 1]));

        // GameObject fire = Instantiate(playerVars.WarriorObject.skillObjects[2], transform.position, Quaternion.identity, transform);
        // ParticleSystem ps = fire.GetComponent<ParticleSystem>();
        // ps.Stop();
        // var main = ps.main;
        // main.duration = playerVars.WarriorObject.startDuration2[playerVars.skillLevel2 - 1];
        // main.startColor = playerVars.WarriorColor;
        // ps.Play();
    }

    IEnumerator becomeVisibleAgain(float timer)
    {
        yield return new WaitForSeconds(timer);

        if (!insideMask)
        {
            GetComponentInChildren<SpriteRenderer>().enabled = true;
            // GetComponent<playerVariables>().headUI.gameObject.SetActive(true);
            // GetComponentInChildren<Gun>().gameObject.GetComponentInChildren<SpriteRenderer>().enabled = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // if (collision.name == "KosasMask(Clone)" && playerVars.IsOwner)
        // {
        //     //invParticle = Instantiate(playerVars.playerObject.skillObjects[2], transform.position, Quaternion.identity, transform);
        //     //ParticleSystem ps = invParticle.GetComponent<ParticleSystem>();
        //     //ps.Stop();
        //     //var main = ps.main;
        //     //main.duration = playerVars.playerObject.startDuration1[playerVars.skillLevel1 - 1];
        //     //ps.Play();

        //     if (!abilityText)
        //     {
        //         abilityText = Instantiate(playerVars.WarriorObject.skillObjects[1], transform.position, Quaternion.identity);
        //         abilityText.GetComponentInChildren<Canvas>().GetComponentInChildren<TMP_Text>().text = "INVISIBLE";
        //     }

        //     insideMask = true;
        // }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // if (collision.name == "KosasMask(Clone)" && playerVars.IsOwner)
        // {
        //     //Destroy(invParticle);

        //     if (abilityText)
        //     {
        //         Destroy(abilityText);
        //     }

        //     insideMask = false;
        // }
    }

    private void Update()
    {
        base.Update();

        if (active1)
        {
            // if (!playerVars.IsOwner)
            // {
            //     if (insideMask)
            //     {
            //         GetComponentInChildren<SpriteRenderer>().enabled = false;
            //         // GetComponent<playerVariables>().headUI.gameObject.SetActive(false);
            //         GetComponentInChildren<Gun>().gameObject.GetComponentInChildren<SpriteRenderer>().enabled = false;
            //     }
            //     else
            //     {
            //         if (!active2)
            //         {
            //             GetComponentInChildren<SpriteRenderer>().enabled = true;
            //             // GetComponent<playerVariables>().headUI.gameObject.SetActive(true);
            //             GetComponentInChildren<Gun>().gameObject.GetComponentInChildren<SpriteRenderer>().enabled = true;
            //         }
            //     }
            // }
        }
    }
}
