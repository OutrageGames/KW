using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FishNet.Object;

public class damager : MonoBehaviour
{
    [SerializeField] private float _damage;
    public int DamagerID;
    public float Damage { get => _damage; set => _damage = value; }

    private void Start()
    {

    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.tag == "Player")
        {
            var enemyVars = collider.gameObject.GetComponent<PlayerHealth>();
            if(enemyVars.GetComponent<NetworkObject>().OwnerId != DamagerID)
            {
                enemyVars.UpdateHealth(enemyVars, -_damage);    

                //xp
                var players = GameObject.FindGameObjectsWithTag("Player");
                for (int i = 0; i < players.Length; i++)
                {
                    if (players[i].GetComponent<NetworkObject>().OwnerId == DamagerID)
                    {
                        var playerExperience = players[i].GetComponent<PlayerExperience>();
                        playerExperience.UpdateXP(playerExperience, _damage * 2f);
                    }
                }
            }
            
            Destroy(gameObject);
        }
    }
}
