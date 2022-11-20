using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FishNet.Object;
using FishNet.Object.Synchronizing;
using FishNet.Transporting;
using TMPro;
using FishNet.Connection;
using FishNet;
using FirstGearGames.LobbyAndWorld.Clients;
using FirstGearGames.LobbyAndWorld.Demos.KingOfTheHill;
using System;

public class PlayerVariables : NetworkBehaviour
{
    
    [SerializeField] public WarriorObject Warrior;
    [SerializeField] public GunObject Gun;
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


        //ServerSpawnGun();
        SpawnGun();      

        //Instantiate(Gun.prefab, transform.position, Quaternion.identity, transform);
        // GameObject go = Instantiate(Gun.prefab, transform.position, Quaternion.identity, transform);
        // base.Spawn(go, client.LocalConnection);
        //gameObject.AddComponent(Type.GetType(Warrior.skillSet));  
    }

    void SpawnGun()
    {
        ServerSpawnGun(Gun.prefab, LocalConnection);
    }
    [ServerRpc]
    void ServerSpawnGun(GameObject go, NetworkConnection con)
    {
        GameObject SpawnedGO = Instantiate<GameObject>(go, transform.position, Quaternion.identity, transform);
        base.Spawn(SpawnedGO, con);
    }

    // [ServerRpc]
    // public void ServerSpawnGun()
    // {
    //     ClientSpawnGun();
    // }

    // [ObserversRpc]
    // public void ClientSpawnGun()
    // {
    //     if(!IsOwner)
    //     {
    //         SpawnGun();
    //     }
    // }

    // private void SpawnGun()
    // {
    //     GameObject go = Instantiate(Gun.prefab, transform.position, Quaternion.identity, transform);
    //     //base.Spawn(go, client.LocalConnection);
    //     ServerManager.Spawn(go);
    // }





    
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
        Gun = ciSettings.GetAllGuns()[ciSettings.GetGunIndex()];
    }
}