using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FishNet.Object;
using UnityEngine.InputSystem;


public class Ak : Gun
{
    protected override void Shooting()
    {
        if ((currentBullets > 0 || !IsOwner) && !IsReloading)
        {
            if (spread < spreadRate)
            {
                spread += 0.4f;
            }
            audioSource.pitch = Random.Range(0.99f, 1.01f);
            audioSource.PlayOneShot(shootSound);

            GameObject b = Instantiate(_bulletPrefab, spawnPoint.position, Quaternion.identity);
            Bullet bullet = b.GetComponent<Bullet>();
            //bullet.Initialize(transform.rotation.eulerAngles.z + Random.Range(-spread, spread), OwnerClientId, 1, _immediateDestroyBullet);
            b.GetComponent<TrailRenderer>().startColor = GetComponentInChildren<SpriteRenderer>().color;
            Instantiate(particleEffect, efePoint.position, Quaternion.identity, transform);
            currentBullets -= 1;
        }
    }
}
