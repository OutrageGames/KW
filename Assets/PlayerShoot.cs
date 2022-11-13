using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FishNet.Object;
using UnityEngine.InputSystem;


public class PlayerShoot : NetworkBehaviour
{
    [SerializeField] private Transform _shootPoint;
    [SerializeField] private GameObject _bullet, _gun;

    public override void OnStartClient()
    {
        base.OnStartClient();
        if(!base.IsOwner)
        {
            GetComponent<PlayerShoot>().enabled = false;
        }
    }

    [ServerRpc]
    void ServerShoot()
    {
        ClientsShoot();        
    }

    [ObserversRpc]
    void ClientsShoot()
    {
        if(!IsOwner)
        {
            Shoot();
        }
    }

    void Shoot()
    {
        GameObject asd = Instantiate(_bullet, _shootPoint.transform.position, Quaternion.identity);
        asd.transform.Rotate(0.0f, 0.0f, _gun.transform.eulerAngles.z);
    }
    public void ShootCallback(InputAction.CallbackContext context)
    {
        if(!base.IsOwner)
            return;

        if (context.performed)
        {
            // Shoot();
            // ServerShoot();
            StartCoroutine(StartShoot());
        }
        if (context.canceled)
        {
            StopAllCoroutines();
        }
    }

    IEnumerator StartShoot()
    {
        while(true)
        {
            Shoot();
            ServerShoot();
            yield return new WaitForSeconds(.1f);
        }
    }
}
