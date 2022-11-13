using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FishNet.Object;
using FishNet.Object.Synchronizing;
using FishNet.Transporting;
using TMPro;

public class PlayerVariables : NetworkBehaviour
{
    [SyncVar] public int Health;
    [SerializeField] public TMP_Text _healthText;
    //[SyncVar] public int health = 10;

    public override void OnStartClient()
    {
        base.OnStartClient();
        if(!base.IsOwner)
        {
            //GetComponent<PlayerVariables>().enabled = false;
        }
    }
    void Start()
    {
        
    }

    void Update()
    {
        _healthText.text = Health.ToString();
        //Debug.Log(TimeManager.RoundTripTime);
        if(Health <= 0)
        {
            Health = 100;
            transform.position = new Vector2(Random.Range(-8f, 8f), Random.Range(-2f, 4f));
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