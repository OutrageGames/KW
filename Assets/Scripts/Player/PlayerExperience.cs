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

public class PlayerExperience : NetworkBehaviour
{
    [SyncVar] public int Level;
    [SyncVar] public float CurrentXP, NextXP;
    [SerializeField] private PlayerUI _playerUI;

    public override void OnStartClient()
    {
        base.OnStartClient();

        // if(!base.IsOwner)
        //     GetComponent<PlayerExperience>().enabled = false;

    }

    [ServerRpc]
    public void UpdateXP(PlayerExperience script, float changeAmmount)
    {
        if(!IsOwner)
        {
            if(Level < 7)
                script.CurrentXP += changeAmmount;

            if (CurrentXP >= NextXP)
            {
                CurrentXP = CurrentXP - NextXP;
                Level += 1;
            }    

            if (Level == 7)
            {
                CurrentXP = 699;
            }
            NextXP = (Level * 50) + (100 / Level);
        }   
    }

    void ShowXP()
    {
        DoChanges();       
        ChangeXP();
    }

    [ObserversRpc]
    public void ChangeXP()
    {
        if(!IsOwner)
        {
            DoChanges();
        }        
    }

    void Update()
    {
        ShowXP();
    }

    void DoChanges()
    {
        _playerUI.LevelText.text = Level.ToString();

        if (Level < 7)
        {
            _playerUI.ExperienceBar.localScale = new Vector2(CurrentXP / NextXP, _playerUI.ExperienceBar.localScale.y);
        }
        else
        {
            _playerUI.ExperienceBar.localScale = new Vector2(1f, _playerUI.ExperienceBar.localScale.y);
        }

        if (_playerUI.ExperienceBar.localScale.x <= 0)
        {
            _playerUI.ExperienceBar.localScale = new Vector2(0f, transform.localScale.y);
        }
        else
        {
            _playerUI.ExperienceBar.localScale = new Vector2(CurrentXP / NextXP, transform.localScale.y);
        }
    }

    


}
