using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LokerSkills : Skills
{
    private float spd, jumpspd;
    private Animator anim;
    private GameObject lokerTrail;
    public override void Skill1()
    {
        // Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // GameObject skill = Instantiate(playerVars.WarriorObject.skillObjects[0], new Vector3(mousePos.x, mousePos.y, 0f), Quaternion.identity);

        // skill.GetComponent<Trampoline>().jumpSpeed = playerVars.WarriorObject.skillDamages1[playerVars.skillLevel1 - 1];
        // skill.GetComponent<destroyAfter>().timer = playerVars.WarriorObject.startDuration1[playerVars.skillLevel1 - 1];

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
        // spd = GetComponent<PlayerMovement>().speed;
        // jumpspd = GetComponent<PlayerMovement>().jumpSpeed;
        // GetComponent<PlayerMovement>().speed += playerVars.playerObject.skillDamages2[playerVars.skillLevel2 - 1];
        // GetComponent<PlayerMovement>().jumpSpeed += playerVars.playerObject.skillDamages2[playerVars.skillLevel2 - 1];



        // StartCoroutine(NormalSpeed(playerVars.WarriorObject.startDuration2[playerVars.skillLevel2 - 1]));

        // lokerTrail = Instantiate(playerVars.WarriorObject.skillObjects[2], transform.position, Quaternion.identity, transform);
        // lokerTrail.GetComponent<TrailRenderer>().startColor = playerVars.WarriorColor;
    }

    IEnumerator NormalSpeed(float timer)
    {
        yield return new WaitForSeconds(timer);

        Destroy(lokerTrail);
        // GetComponent<PlayerMovement>().speed = spd;
        // GetComponent<PlayerMovement>().jumpSpeed = jumpspd;
    }

    //private void Update()
    //{
    //    base.Update();
    //}
}
