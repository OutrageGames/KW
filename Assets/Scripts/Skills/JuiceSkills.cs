using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class JuiceSkills : Skills
{
    public bool insideMask;
    public GameObject madeBig;

    public override void Skill1()
    {
        // GameObject skill = Instantiate(playerVars.WarriorObject.skillObjects[0], GetComponentInChildren<Gun>().spawnPoint.position,
        //     Quaternion.Euler(new Vector3(0f, 0f, GetComponentInChildren<Gun>().gameObject.transform.rotation.eulerAngles.z)));

        // skill.GetComponent<Rigidbody2D>().velocity = skill.transform.right * 40f;
        // skill.GetComponent<juiceSkillshot>().size = playerVars.WarriorObject.skillDamages1[playerVars.skillLevel1 - 1];
        // skill.GetComponentInChildren<SpriteRenderer>().color = new Color(playerVars.WarriorColor.r, playerVars.WarriorColor.g, playerVars.WarriorColor.b, 1f);
        // skill.GetComponent<TrailRenderer>().startColor = new Color(playerVars.WarriorColor.r, playerVars.WarriorColor.g, playerVars.WarriorColor.b, 1f);
        // skill.GetComponent<TrailRenderer>().endColor = new Color(0f, 0f, 0f, 0f);

        // ParticleSystem ps = skill.GetComponent<ParticleSystem>();
        // ps.Stop();
        // var main = ps.main;
        // main.startColor = new Color(playerVars.WarriorColor.r, playerVars.WarriorColor.g, playerVars.WarriorColor.b, 0.4f);
        // ps.Play();

    }

    public override void Skill2()
    {
        // Vector3 scaleTmp = transform.localScale;

        // transform.localScale = transform.localScale / playerVars.WarriorObject.skillDamages2[playerVars.skillLevel2 - 1];
        // GetComponentInChildren<Gun>().transform.localScale = scaleTmp * playerVars.WarriorObject.skillDamages2[playerVars.skillLevel2 - 1];

        // GameObject dmgText = Instantiate(playerVars.WarriorObject.skillObjects[1], transform.position, Quaternion.identity, transform);
        // dmgText.GetComponentInChildren<Canvas>().GetComponentInChildren<TMP_Text>().text = "SMOL";
        // dmgText.GetComponent<destroyAfter>().timer = playerVars.WarriorObject.startDuration2[playerVars.skillLevel2 - 1];

        // StartCoroutine(BecomeBigAgain(playerVars.WarriorObject.startDuration2[playerVars.skillLevel2 - 1]));
    }

    public IEnumerator MakeSmallAgain(float timer)
    {
        yield return new WaitForSeconds(timer);

        if (madeBig != null)
        {
            madeBig.transform.localScale = new Vector2(1f, 1f);
        }
        madeBig = null;
    }

    IEnumerator BecomeBigAgain(float timer)
    {
        yield return new WaitForSeconds(timer);

        transform.localScale = new Vector2(1f, 1f);
        // GetComponentInChildren<Gun>().transform.localScale = new Vector2(1f, 1f);
    }
}
