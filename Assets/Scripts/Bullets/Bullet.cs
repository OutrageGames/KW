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
    public int BulletID;



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
        if (collider.tag == "Ground")
        {
            return true;
        }
        else if ((collider.tag == "Player"))
        {
            var enemyVars = collider.gameObject.GetComponent<PlayerHealth>();
            if(!enemyVars.IsImmune)
            {
                if(enemyVars.GetComponent<NetworkObject>().OwnerId != BulletID)
                {
                    enemyVars.UpdateHealth(enemyVars, -_damage);
                }

                //xp
                var players = GameObject.FindGameObjectsWithTag("Player");
                for (int i = 0; i < players.Length; i++)
                {
                    if (players[i].GetComponent<NetworkObject>().OwnerId == BulletID)
                    {
                        var playerExperience = players[i].GetComponent<PlayerExperience>();
                        playerExperience.UpdateXP(playerExperience, _damage);
                    }
                }
            }

            _stop = true;
            //Destroy(gameObject);
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
