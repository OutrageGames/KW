using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FishNet.Object;

public class Damager : MonoBehaviour
{
    [SerializeField] private float _damage;
    public int DamagerID;
    public float Damage { get => _damage; set => _damage = value; }
    [SerializeField] private GameObject _blood;

    private void Start()
    {

    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.tag == "Player")
        {
            var enemyVars = collider.gameObject.GetComponent<PlayerHealth>();
            if(enemyVars.GetComponent<PlayerVariables>().playerID != DamagerID)
            {
                enemyVars.UpdateHealth(enemyVars, -_damage);

                GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
                GameObject blood = Instantiate(_blood, enemyVars.transform.position, Quaternion.identity);
                blood.GetComponent<PassId>().id = DamagerID;
                //blood sound
                for (int i = 0; i < players.Length; i++)
                {
                    if (players[i].GetComponent<PlayerVariables>().IsOwner && players[i].GetComponent<PlayerVariables>().playerID == blood.GetComponent<PassId>().id)
                    {
                        blood.GetComponent<AudioSource>().Play();
                    }
                }

                //xp
                for (int i = 0; i < players.Length; i++)
                {
                    if (players[i].GetComponent<PlayerVariables>().playerID == DamagerID)
                    {
                        var playerExperience = players[i].GetComponent<PlayerExperience>();
                        playerExperience.UpdateXP(playerExperience, _damage * 2f);
                    }
                }
                this.enabled = false;
            }
            
            
        }
    }
}
