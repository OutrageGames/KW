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
}