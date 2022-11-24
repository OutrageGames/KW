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
                if (spread < spreadRate)
                {
                    spread += 2f;
                }

                float bulletRot = transform.eulerAngles.z + Random.Range(-spread, spread);
                Vector2 bulletPos = spawnPoint.position;
                int bulletID = GetComponentInParent<NetworkObject>().OwnerId;
                Shoot(bulletPos, bulletRot, DamageMultiplier, bulletID);
                ServerShoot(bulletPos, bulletRot, DamageMultiplier, bulletID);

                IsShooting = true;
                
                // audioSource.pitch = Random.Range(0.99f, 1.01f);
                // audioSource.PlayOneShot(shootSound);
                //Instantiate(particleEffect, efePoint.position, Quaternion.identity, transform);
                currentBullets -= 1;
            }
            yield return new WaitForSeconds(fireRate);
        }
    }

    [ServerRpc]
    void ServerShoot(Vector2 pos, float rot, float dmg, int bulletID)
    {
        ClientsShoot(pos, rot, dmg, bulletID);        
    }

    [ObserversRpc]
    void ClientsShoot(Vector2 pos, float rot, float dmg, int bulletID)
    {
        if(!IsOwner)
        {
            Shoot(pos, rot, dmg, bulletID);
        }
    }

    void Shoot(Vector2 pos, float rot, float dmg, int bulletID)
    {
        GameObject bullet = Instantiate(_bulletPrefab, pos, Quaternion.Euler(0f, 0f, rot));
        bullet.GetComponent<Bullet>().Damage *= dmg;
        bullet.GetComponent<Bullet>().BulletID = bulletID;
        bullet.GetComponent<TrailRenderer>().startColor = GetComponentInChildren<SpriteRenderer>().color;
        bullet.GetComponent<TrailRenderer>().endColor = GetComponentInChildren<SpriteRenderer>().color;
    }
}
