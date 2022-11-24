using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FishNet.Object;
using FishNet.Object.Synchronizing;
using FishNet.Transporting;
using TMPro;
using FishNet.Connection;
using FirstGearGames.LobbyAndWorld.Clients;
using FirstGearGames.LobbyAndWorld.Demos.KingOfTheHill;

public class PlayerHealth : NetworkBehaviour
{
    [SyncVar] public float Health;
    [SyncVar] public bool IsImmune;
    [SerializeField] private PlayerUI _playerUI;

    public override void OnStartClient()
    {
        base.OnStartClient();

        if(!base.IsOwner)
            GetComponent<PlayerHealth>().enabled = false;

    }

    [ServerRpc]
    public void BecomeImmune(PlayerHealth script, bool immunity)
    {
        script.IsImmune = immunity;
    }

    [ServerRpc]
    public void UpdateHealth(PlayerHealth script, float changeAmmount)
    {
        if(!IsOwner && !script.IsImmune)
        {
            script.Health += changeAmmount;

            if(Health <= 0)
            {
                Health = 100;
            }
        }
    }

    void ShowHealth()
    {
        _playerUI.HealthBar.transform.localScale = new Vector2(Health / 100f, transform.localScale.y);
        ChangeHealth();
    }

    [ObserversRpc]
    public void ChangeHealth()
    {
        if(!IsOwner)
        {
            if (_playerUI.HealthBar.transform.localScale.x <= 0)
            {
                _playerUI.HealthBar.transform.localScale = new Vector2(0f, transform.localScale.y);
            }
            else
            {
                _playerUI.HealthBar.transform.localScale = new Vector2(Health / 100f, transform.localScale.y);
            }

            

        }        
    }

    void Update()
    {
        ShowHealth();

        if(Health <= 0)
        {
            float x = Random.Range(3, 60);
            float y = 25;
            Vector2 next = new Vector2(x, y);
            transform.position = next;

            Health = 100;
        }
        
    }

    


}
