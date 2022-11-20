using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FishNet.Object;

public class GunRotation : NetworkBehaviour
{
    [SerializeField] private Transform _warriorSprite;
    // Start is called before the first frame update
    void Start()
    {
        _warriorSprite = transform.parent.gameObject.GetComponentInChildren<SpriteRenderer>().transform;
    }

    private void Update()
    {
        if(!base.IsOwner)
            return;
            
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        var rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        var gunY = transform.localScale.x;

        transform.rotation =  Quaternion.Euler (new Vector3(0f, 0f, rotZ));


        if ((rotZ > 90 && rotZ <= 180) || (rotZ > -180 && rotZ <= -90))
        {
            transform.localScale = new Vector2(transform.localScale.x, -gunY);
            _warriorSprite.localScale = new Vector2(-1f, _warriorSprite.localScale.y);
            //_lookDirection = -1f;
        }
        else
        {
            transform.localScale = new Vector2(transform.localScale.x, gunY);
            _warriorSprite.localScale = new Vector2(1f, _warriorSprite.localScale.y);
            //_lookDirection = 1f;
        }
    }
}
