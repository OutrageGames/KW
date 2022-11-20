using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private LayerMask _hitLayerMask;
    [SerializeField] private float _damage;
    public float Damage { get => _damage; set => _damage = value; }


    void Update()
    {
        var travelDistance = Time.deltaTime * _speed;
        var hit = Physics2D.Raycast(transform.position, transform.right, travelDistance, _hitLayerMask);
        if (hit.collider is null)
            transform.position += (transform.right * Time.deltaTime * _speed);
        else
        {
            if (CheckHit(hit.collider))
            {
                _speed = 0f;
                transform.position += (transform.right * hit.distance);
            }
            else
            {
                transform.position += (transform.right * Time.deltaTime * _speed);
            }
        }
    }

    private bool CheckHit(Collider2D collider)
    {
        if (collider.tag == "Ground")
        {
            return true;
        }
        else if ((collider.tag == "Player"))
        {
            var enemyVars = collider.gameObject.GetComponent<PlayerHealth>();
            enemyVars.UpdateHealth(enemyVars, -_damage);
            Destroy(gameObject);
            return true;
        }
        // else if (collider.tag == "Enemy" || collider.tag == "Player")
        // {
        //     //var statsController = collider.GetComponent<PlayerStatsController>();
        //     //var otherId = statsController?.OwnerClientId;
        //     // if (otherId != _ownerId && !_dealtDamage)
        //     // {
        //     //     // Deal damage
        //     //     statsController.TakeDamage(_damage);
        //     //     _dealtDamage = true;
        //     //     return true;
        //     // }
        // }
        return false;
    }
}
