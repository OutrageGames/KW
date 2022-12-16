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
    [SerializeField] private SpriteRenderer[] _spriteRenderers;
    
    
    public override void OnStartClient()
    {
        base.OnStartClient();
        _playerVariables = GetComponentInParent<PlayerVariables>();

        
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
        _spriteRenderers[0].sprite = playerVariables.Warrior.warriorSprites[0];
        _spriteRenderers[1].sprite = playerVariables.Warrior.warriorSprites[1];
        _spriteRenderers[2].sprite = playerVariables.Warrior.warriorSprites[2];
        _spriteRenderers[3].sprite = playerVariables.Warrior.warriorSprites[2];
    }
}