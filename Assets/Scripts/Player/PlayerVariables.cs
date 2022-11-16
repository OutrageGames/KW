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
    [SyncVar] public float Health;
    [SerializeField] public TMP_Text _healthText;
    [SerializeField] public WarriorObject Warrior;
    [SerializeField] public int skillLevel1, skillLevel2;

    public override void OnStartClient()
    {
        base.OnStartClient();
        
        if(!base.IsOwner)
            return;

        NetworkObject client = GetComponent<NetworkObject>();
        ClientInstance ci = ClientInstance.ReturnClientInstance(client.Owner);
        PlayerSettings ciSettings = ci.GetComponent<PlayerSettings>();

        SetWarrior(client, ciSettings);
        ServerSetWarrior(client, ciSettings);
    }
    void Start()
    {
        
    }
    
    [ServerRpc]
    public void ServerSetWarrior(NetworkObject client, PlayerSettings ciSettings)
    {
        ClientSetWarrior(client, ciSettings);
    }

    [ObserversRpc]
    public void ClientSetWarrior(NetworkObject client, PlayerSettings ciSettings)
    {
        if(!IsOwner)
        {
            SetWarrior(client, ciSettings);
        }
    }

    private void SetWarrior(NetworkObject client, PlayerSettings ciSettings)
    {
        Warrior = ciSettings.GetAllWarriors()[ciSettings.GetWarriorIndex()];
    }

    

    void Update()
    {
        _healthText.text = Health.ToString();
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
    public void ServerUpdateHealth(PlayerVariables script, float changeAmmount)
    {
        ClientUpdateHealth(this, changeAmmount);
    }

    [ObserversRpc]
    public void ClientUpdateHealth(PlayerVariables script, float changeAmmount)
    {
        if(!IsOwner)
        {
            UpdateHealth(this, changeAmmount);
        }
    }
    
    [ServerRpc]
    public void UpdateHealth(PlayerVariables script, float changeAmmount)
    {
        Health += changeAmmount;
        _healthText.text = Health.ToString();
    }
}