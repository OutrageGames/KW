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
    [SerializeField] private TMP_Text HealthText;

    public override void OnStartClient()
    {
        base.OnStartClient();

        if(!base.IsOwner)
            GetComponent<PlayerHealth>().enabled = false;

    }

    [ServerRpc]
    public void UpdateHealth(PlayerHealth script, float changeAmmount)
    {
        if(!IsOwner)
        {
            script.Health += changeAmmount;
        }
    }

    void ShowHealth()
    {
        HealthText.text = Health.ToString();
        ChangeHealth();
    }

    [ObserversRpc]
    public void ChangeHealth()
    {
        if(!IsOwner)
        {
            HealthText.text = Health.ToString();
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
