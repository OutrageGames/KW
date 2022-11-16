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

public class PlayerAnimator : NetworkBehaviour
{
    [SerializeField] private PlayerVariables _playerVariables;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    
    
    public override void OnStartClient()
    {
        base.OnStartClient();
        _playerVariables = GetComponent<PlayerVariables>();
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();

        
        if(!base.IsOwner)
            return;

        
        SetSprite(_playerVariables);
        ServerSetSprite(_playerVariables);
    }

    [ServerRpc]
    public void ServerSetSprite(PlayerVariables playerVariables)
    {
        ClientSetSprite(playerVariables);
    }

    [ObserversRpc]
    public void ClientSetSprite(PlayerVariables playerVariables)
    {
        if(!IsOwner)
        {
            SetSprite(playerVariables);
        }
    }

    private void SetSprite(PlayerVariables playerVariables)
    {
        _spriteRenderer.sprite = playerVariables.Warrior.warriorSprite;
    }
}