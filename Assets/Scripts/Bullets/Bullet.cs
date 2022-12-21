using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using FishNet.Object;
using FishNet.Object.Synchronizing;
using FishNet.Transporting;
using FishNet.Connection;
using FishNet;
using FirstGearGames.LobbyAndWorld.Clients;
using FirstGearGames.LobbyAndWorld.Demos.KingOfTheHill;
using System;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private LayerMask _hitLayerMask;
    [SerializeField] private float _damage;
    public float Damage { get => _damage; set => _damage = value; }
    private bool _stop = false;
    public GameObject Shooter;
    [SerializeField] private GameObject _blood;


    void Update()
    {
        if(!_stop)
        {
            var travelDistance = Time.deltaTime * _speed;
            var hit = Physics2D.Raycast(transform.position, transform.right, travelDistance, _hitLayerMask);
        
            if (hit.collider is null)
                transform.position += (transform.right * Time.deltaTime * _speed);
            else
            {
                if (CheckHit(hit.collider))
                {
                    _speed = 0f;
                    transform.position += (transform.right * hit.distance);
                    
                    if(hit.collider.tag == "Player")
                    {
                        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
                        var enemyHealth = hit.collider.gameObject.GetComponent<PlayerHealth>();
                        if(!enemyHealth.IsImmune)
                        {
                            if(enemyHealth.gameObject.GetComponent<PlayerVariables>().playerID != Shooter.GetComponent<PlayerVariables>().playerID)
                            {
                                enemyHealth.BulletDmg(enemyHealth, -_damage);
                                enemyHealth.DamagedBy = Shooter;
                                GameObject blood = Instantiate(_blood, transform.position, Quaternion.identity);
                                blood.GetComponent<PassId>().id = Shooter.GetComponent<PlayerVariables>().playerID;
                                //blood sound
                                for (int i = 0; i < players.Length; i++)
                                {
                                    if (players[i].GetComponent<PlayerVariables>().IsOwner && players[i].GetComponent<PlayerVariables>().playerID == blood.GetComponent<PassId>().id)
                                    {
                                        blood.GetComponent<AudioSource>().Play();
                                    }
                                }
                            }

                            //xp
                            for (int i = 0; i < players.Length; i++)
                            {
                                if (players[i].GetComponent<PlayerVariables>().playerID == Shooter.GetComponent<PlayerVariables>().playerID)
                                {
                                    var playerExperience = players[i].GetComponent<PlayerExperience>();
                                    playerExperience.UpdateXP(playerExperience, _damage * 2f);
                                }
                            }
                        }

                        _stop = true;
                    }
                }
                else
                {
                    transform.position += (transform.right * Time.deltaTime * _speed);
                }
            }
        }
        else
        {
            GetComponent<CircleCollider2D>().enabled = false;
            ///stop
        }
    }

    private bool CheckHit(Collider2D collider)
    {
        if (collider.tag == "Ground" || collider.tag == "Player")
        {
            return true;
        }

        // else if (collider.tag == "Enemy" || collider.tag == "Player")
        // {
        //     //var statsController = collider.GetComponent<PlayerStatsController>();
        //     //var otherId = statsController?.OwnerClientId;
        //     // if (otherId != _ownerId && !_dealtDamage)
        //     // {
        //     //     // Deal damage
        //     //     statsController.TakeDamage(_damage);
        //     //     _dealtDamage = true;
        //     //     return true;
        //     // }
        // }
        return false;
    }
}
