using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class damager : MonoBehaviour
{
    private float _damage;
    public float Damage { get => _damage; set => _damage = value; }

    private void Start()
    {

    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.tag == "Player")
        {
            var enemyVars = collider.gameObject.GetComponent<PlayerVariables>();
            enemyVars.UpdateHealth(enemyVars, -_damage);
            Destroy(gameObject);
        }
    }
}
