using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ErebossSkills : Skills
{
    private bool insideShield;
    private Animator anim;
    public GameObject abilityText;

    public override void Skill1()
    {
        // Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // GameObject skill = Instantiate(playerVars.WarriorObject.skillObjects[0], new Vector3(mousePos.x, mousePos.y, 0f), Quaternion.identity);
        // skill.GetComponent<destroyAfter>().timer = playerVars.WarriorObject.startDuration1[playerVars.skillLevel1 - 1];
        // skill.GetComponentInChildren<SpriteRenderer>().color = playerVars.WarriorColor;

        // ParticleSystem ps = skill.GetComponentInChildren<ParticleSystem>();
        // ps.Stop();
        // var main = ps.main;
        // main.startColor = playerVars.WarriorColor;
        // ps.Play();

        // anim = skill.GetComponentInChildren<Animator>();
        // StartCoroutine(shrink(playerVars.WarriorObject.startDuration1[playerVars.skillLevel1 - 1] - 0.5f));
    }

    IEnumerator shrink(float time)
    {
        yield return new WaitForSeconds(time);
        anim.SetTrigger("goto");
    }

    public override void Skill2()
    {
        // GameObject abilityText2 = Instantiate(playerVars.WarriorObject.skillObjects[2], transform.position, Quaternion.identity);
        // abilityText2.GetComponentInChildren<Canvas>().GetComponentInChildren<TMP_Text>().text = "IMMUNE";
        // abilityText2.GetComponent<destroyAfter>().timer = playerVars.WarriorObject.startDuration2[playerVars.skillLevel2 - 1];

        // GameObject particle = Instantiate(playerVars.WarriorObject.skillObjects[3], transform.position, Quaternion.identity, transform);
        // ParticleSystem ps = particle.GetComponent<ParticleSystem>();
        // ps.Stop();
        // var main = ps.main;
        // main.duration = playerVars.WarriorObject.startDuration2[playerVars.skillLevel2 - 1];
        // main.startColor = playerVars.WarriorColor;
        // ps.Play();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "erebossCircleShield")
        {
            //playerVars.damage = -1;
            insideShield = true;

            // if (!abilityText)
            // {
            //     abilityText = Instantiate(playerVars.WarriorObject.skillObjects[1], transform.position, Quaternion.identity);
            //     abilityText.GetComponentInChildren<Canvas>().GetComponentInChildren<TMP_Text>().text = "UNDYING";
            // }

            //undyingParticle = Instantiate(playerVars.playerObject.skillObjects[4], transform.position, Quaternion.identity, transform);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.name == "erebossCircleShield")
        {
            insideShield = false;
            // playerVars.damage = 0;
            if (abilityText)
            {
                Destroy(abilityText);
            }
            //Destroy(undyingParticle);
        }
    }

    private void Update()
    {
        // base.Update();

        // if (!active1)
        // {
        //     insideShield = false;
        // }

        // if (insideShield)
        // {
        //     if (_playerStatsController.CurrentPlayerHealth <= 10)
        //     {
        //         playerVars.damage = -1;
        //     }
        // }

        // if (active2)
        // {
        //     playerVars.damage = -1;
        // }

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
