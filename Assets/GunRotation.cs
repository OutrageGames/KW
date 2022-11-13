using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FishNet.Object;

public class GunRotation : NetworkBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
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
            //_spriteTransform.localScale = new Vector2(-1f, _spriteTransform.localScale.y);
            //_lookDirection = -1f;
        }
        else
        {
            transform.localScale = new Vector2(transform.localScale.x, gunY);
            //_spriteTransform.localScale = new Vector2(1f, _spriteTransform.localScale.y);
            //_lookDirection = 1f;
        }
    }
}
