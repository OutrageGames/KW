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
using UnityEngine.InputSystem;


public class PlayerHealth : NetworkBehaviour
{
    [SyncVar] public float Health;
    [SyncVar] public bool IsImmune;
    [SerializeField] private PlayerUI _playerUI;
    public GameObject DamagedBy;
    [SerializeField] private GameplayManager _gameplayManager;
    [SerializeField] private PlayerVariables _playerVariables;
    [SerializeField] private GameObject _deathParticle;

    public override void OnStartClient()
    {
        base.OnStartClient();
        _gameplayManager = GameObject.FindGameObjectWithTag("Manager").GetComponent<GameplayManager>();
        _playerVariables = GetComponent<PlayerVariables>();

        if(!base.IsOwner)
            return;
    }


    void Start()
    {
        transform.position = _gameplayManager.SpawnPoints[_playerVariables.playerID].position;
    }

    [ServerRpc]
    public void BecomeImmune(PlayerHealth script, bool immunity)
    {
        script.IsImmune = immunity;
    }

    public void BulletDmg(PlayerHealth script, float changeAmmount)
    {
        UpdateHealth(script, changeAmmount);
    }

    public void ShowHealth()
    {
        _playerUI.HealthBar.transform.localScale = new Vector2(Health / 100f, transform.localScale.y);   
    }

    [ServerRpc]
    public void serhealth100(PlayerHealth script)
    {
        script.Health = 100;
    }

    [ServerRpc]
    public void UpdateHealth(PlayerHealth script, float changeAmmount)
    {
        if(!IsOwner && !script.IsImmune)
        {
            script.Health += changeAmmount;
        }
    }

    void Update()
    {
        ShowHealth();
        if(Health <= 0)
        {
            _gameplayManager.ShowKillText(DamagedBy, gameObject);
            DamagedBy.GetComponent<PlayerVariables>().Kills++;
            DamagedBy.transform.position = _gameplayManager.SpawnPoints[DamagedBy.GetComponent<PlayerVariables>().playerID].position;

            GameObject deathP = Instantiate(_deathParticle, transform.position, Quaternion.identity);
            ParticleSystem ps = deathP.GetComponent<ParticleSystem>();
            ps.Stop();
            var main = ps.main;
            main.startColor = _playerVariables.WarriorColor;
            ps.Play();  

            if(DamagedBy.GetComponent<PlayerVariables>().Kills >= 10)
            {
                _gameplayManager.Win(DamagedBy.GetComponent<PlayerVariables>().Username);
            }
            transform.position = _gameplayManager.SpawnPoints[_playerVariables.playerID].position;
            Health = 100;
            serhealth100(this);
        }
    }

    
}
