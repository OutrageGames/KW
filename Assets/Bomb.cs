using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    void Start()
    {
        Destroy(gameObject, .2f);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.tag == "Player")
        {
            var enemyVars = collider.gameObject.GetComponent<PlayerVariables>();
            enemyVars.UpdateHealth(enemyVars, -20);
            Destroy(gameObject);
        }
    }
}
