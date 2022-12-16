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


public class PlayerVariables : NetworkBehaviour
{
    
    [SerializeField] public WarriorObject Warrior;
    [SerializeField] public Color WarriorColor;
    private PlayerUI _playerUI;
    [SerializeField] public GunObject Gun;
    public int skillLevel1, skillLevel2;
    [SyncVar] public int playerID;
    [SyncVar] public string Username;
    [SerializeField] private HardLight2D _playerLight;
    [SyncVar] public int Kills;
    // [SyncVar] public float Health;
    [SyncVar] public bool IsImmune;
    public int DamagedBy;
    [SerializeField] private GameplayManager _gameplayManager;
    public InputMaster controls;


    public override void OnStartClient()
    {
        base.OnStartClient();

        _playerUI = GetComponent<PlayerUI>();
        _playerLight.gameObject.SetActive(base.IsOwner);
        _gameplayManager = GameObject.FindGameObjectWithTag("Manager").GetComponent<GameplayManager>();

        // if(base.IsOwner)
        //     gameObject.GetComponentInChildren<SpriteRenderer>().gameObject.layer = 10;
        // else
        //     gameObject.GetComponentInChildren<SpriteRenderer>().gameObject.layer = 11;
        
        if(!base.IsOwner)
            return;
        
        
        NetworkObject client = GetComponent<NetworkObject>();
        ClientInstance ci = ClientInstance.ReturnClientInstance(client.Owner);
        PlayerSettings ciSettings = ci.GetComponent<PlayerSettings>();
        

        SetWarrior(client, ciSettings);
        ServerSetWarrior(client, ciSettings); 


        //ServerSpawnGun();
        SpawnGun();      
        WarriorColor = Warrior.warriorColor[0];

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
        GameObject SpawnedGO = Instantiate<GameObject>(go, new Vector2(transform.position.x, transform.position.y + 0.3f), Quaternion.identity, transform);
        base.Spawn(SpawnedGO, con);
    }

    [ServerRpc]
    public void ServerChangeID(PlayerVariables var, int gab)
    {
        var.playerID = gab;
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
            _gameplayManager.OtherNames[1].text = Username;
        }
    }

    private void SetWarrior(NetworkObject client, PlayerSettings ciSettings)
    {
        Warrior = ciSettings.GetAllWarriors()[ciSettings.GetWarriorIndex()];
        Gun = ciSettings.GetAllGuns()[ciSettings.GetGunIndex()];
        Username = ciSettings.GetUsername();
        _playerUI.Username.text = Username.ToString();
    }
}