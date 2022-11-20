using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FishNet.Object;
using UnityEngine.InputSystem;


public class Ak : Gun
{
    public override IEnumerator Shooting()
    {
        while(true)
        {
            if (currentBullets > 0 && !IsReloading)
            {
                IsShooting = true;
                if (spread < spreadRate)
                {
                    spread += 0.4f;
                }
                // audioSource.pitch = Random.Range(0.99f, 1.01f);
                // audioSource.PlayOneShot(shootSound);
                float bulletRot = transform.eulerAngles.z + Random.Range(-spread, spread);
                Vector2 bulletPos = spawnPoint.position;
                ServerShoot(bulletPos, bulletRot);
                Shoot(bulletPos, bulletRot);

                //b.GetComponent<TrailRenderer>().startColor = GetComponentInChildren<SpriteRenderer>().color;
                //Instantiate(particleEffect, efePoint.position, Quaternion.identity, transform);
                currentBullets -= 1;
            }
            yield return new WaitForSeconds(fireRate);
        }
    }

    [ServerRpc]
    void ServerShoot(Vector2 pos, float rot)
    {
        ClientsShoot(pos, rot);        
    }

    [ObserversRpc]
    void ClientsShoot(Vector2 pos, float rot)
    {
        if(!IsOwner)
        {
            Shoot(pos, rot);
        }
    }

    void Shoot(Vector2 pos, float rot)
    {
        Instantiate(_bulletPrefab, spawnPoint.position, Quaternion.Euler(0f, 0f, rot));
    }
}
