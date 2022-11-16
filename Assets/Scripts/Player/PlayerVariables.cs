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

public class PlayerVariables : NetworkBehaviour
{
    [SyncVar] public int Health;
    [SerializeField] public TMP_Text _healthText;
    //[SyncVar] public int health = 10;

    public override void OnStartClient()
    {
        base.OnStartClient();
        
        if(!base.IsOwner)
            return;

        NetworkObject client = GetComponent<NetworkObject>();
        ClientInstance ci = ClientInstance.ReturnClientInstance(client.Owner);
        PlayerSettings ciSettings = ci.GetComponent<PlayerSettings>();

        SetSprite(client, ciSettings);
        ServerSetSprite(client, ciSettings);
    }
    void Start()
    {
        
    }
    
    [ServerRpc]
    public void ServerSetSprite(NetworkObject client, PlayerSettings ciSettings)
    {
        ClientSetSprite(client, ciSettings);
    }

    [ObserversRpc]
    public void ClientSetSprite(NetworkObject client, PlayerSettings ciSettings)
    {
        if(!IsOwner)
        {
            SetSprite(client, ciSettings);
        }
    }

    private void SetSprite(NetworkObject client, PlayerSettings ciSettings)
    {
        GetComponentInChildren<SpriteRenderer>().sprite = ciSettings.GetAllWarriors()[ciSettings.GetWarriorIndex()].warriorSprite;
    }

    void Update()
    {
        _healthText.text = Health.ToString();
        //Debug.Log(TimeManager.RoundTripTime);
        if(Health <= 0)
        {
            float x = Random.Range(3, 60);
            float y = 25;
            Vector2 next = new Vector2(x, y);
            transform.position = next;

            Health = 100;
        }
    }
    
    [ServerRpc]
    public void ServerUpdateHealth(PlayerVariables script, int changeAmmount)
    {
        ClientUpdateHealth(this, changeAmmount);
    }

    [ObserversRpc]
    public void ClientUpdateHealth(PlayerVariables script, int changeAmmount)
    {
        if(!IsOwner)
        {
            UpdateHealth(this, changeAmmount);
        }
    }
    
    [ServerRpc]
    public void UpdateHealth(PlayerVariables script, int changeAmmount)
    {
        Health += changeAmmount;
        _healthText.text = Health.ToString();
    }
}