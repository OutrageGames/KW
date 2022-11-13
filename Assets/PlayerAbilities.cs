using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FishNet.Object;
using UnityEngine.InputSystem;

public class PlayerAbilities : NetworkBehaviour
{
    [SerializeField] private GameObject _bomb;

    void Start()
    {
        
    }

    public override void OnStartClient()
    {
        base.OnStartClient();
        if(!base.IsOwner)
        {
            GetComponent<PlayerAbilities>().enabled = false;
        }
    }

    [ServerRpc]
    void ServerAbility1(Vector2 pos)
    {
        ClientsAbility1(pos);        
    }

    [ObserversRpc]
    void ClientsAbility1(Vector2 pos)
    {
        if(!IsOwner)
        {
            Ability1(pos);
        }
    }

    void Ability1(Vector2 pos)
    {
        GameObject asd = Instantiate(_bomb, pos, Quaternion.identity);
    }
    
    public void Ability1Callback(InputAction.CallbackContext context)
    {
        if(!base.IsOwner)
            return;

        if (context.performed)
        {
            Vector2 _mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            DoAbility1(_mousePos);
        }
    }

    private void DoAbility1(Vector2 pos)
    {
        Ability1(pos);
        ServerAbility1(pos);
    }
}
