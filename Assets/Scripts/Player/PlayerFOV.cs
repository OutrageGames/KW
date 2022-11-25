using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using FishNet.Object;
using FishNet.Object.Synchronizing;
using FishNet.Transporting;
using TMPro;
using FishNet.Connection;
using FishNet;
using FirstGearGames.LobbyAndWorld.Clients;
using FirstGearGames.LobbyAndWorld.Demos.KingOfTheHill;

public class PlayerFOV : NetworkBehaviour
{
    [SerializeField] private float _viewRadius = 35, _viewAngle = 360;
    [SerializeField] private LayerMask _targetMask, _obstacleMask;
    private Vector2 _eyesPosition;
    private Transform _target;
    [SerializeField] private GameObject[] _uiObjects;
    private List<Transform> visibleTargets = new List<Transform>();

    void Start()
    {
        StartCoroutine("FindTargetsWithDelay", 0f);
    }

    private void Update()
    {
        _eyesPosition = new Vector2(transform.position.x, transform.position.y + 0.7f);
    }

    IEnumerator FindTargetsWithDelay(float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            FindVisibleTargets();
        }
    }

    void FindVisibleTargets()
    {
        visibleTargets.Clear();
        Collider2D[] targetsInViewRadius = Physics2D.OverlapCircleAll(_eyesPosition, _viewRadius, _targetMask);

        for (int i = 0; i < targetsInViewRadius.Length; i++)
        {
            _target = targetsInViewRadius[i].transform;
            Vector3 dirToTarget = (_target.position - transform.position).normalized;
            if (Vector3.Angle(transform.forward, dirToTarget) < _viewAngle / 2)
            {
                float distanceToTarget = Vector3.Distance(_eyesPosition, _target.position);

                if (_target.GetComponent<PlayerVariables>() && !_target.gameObject.GetComponent<NetworkObject>().IsOwner)
                {
                    if (!Physics2D.Raycast(_eyesPosition, dirToTarget, distanceToTarget, _obstacleMask))
                    {
                        for (int j = 0; j < _uiObjects.Length; j++)
                        {
                            _target.GetComponent<PlayerFOV>()._uiObjects[j].SetActive(true);
                        }
                        visibleTargets.Add(_target);
                    }
                    else
                    {
                        for (int j = 0; j < _uiObjects.Length; j++)
                        {
                            _target.GetComponent<PlayerFOV>()._uiObjects[j].SetActive(false);
                        }
                    }
                }
            }
        }
    }

    public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
        {
            angleInDegrees += transform.eulerAngles.y;
        }
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), Mathf.Cos(angleInDegrees * Mathf.Deg2Rad), 0);
    }
}