using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FishNet.Object;

public class EnterTrigger : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "Player")
        {
            var enemyVars = col.gameObject.GetComponent<PlayerVariables>();
            if(enemyVars.playerID != GetComponentInParent<Damager>().DamagerID)
            {
                GetComponentInParent<Animator>().SetTrigger("trigger");
            }
        }
    }
}
