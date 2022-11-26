using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JuiceSkillshot : MonoBehaviour
{
    public float Size;
    public JuiceSkills JuiceSkills;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerVariables otherPlayer = collision.gameObject.GetComponent<PlayerVariables>();
        if (otherPlayer && otherPlayer.OwnerId != JuiceSkills.OwnerId)
        {
            // collision.gameObject.transform.localScale = new Vector2(Size, Size);
            JuiceSkills.ServerMakeBig(otherPlayer.transform, Size);

            JuiceSkills.BigEnemy = collision.gameObject;
            JuiceSkills.StartCoroutine(JuiceSkills.MakeSmallAgain(otherPlayer.transform));


            Destroy(gameObject);
        }
        else if (!otherPlayer)
        {
            Destroy(gameObject);
        }
    }
}
