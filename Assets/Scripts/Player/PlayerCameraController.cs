using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.Rendering;
using FishNet.Object;

public class PlayerCameraController : NetworkBehaviour
{
    [SerializeField] private CinemachineVirtualCamera _cmCam;
    private CinemachineBrain _mainCamera;
    private CinemachineBasicMultiChannelPerlin _cameraPerlin;

    [SerializeField]
    private GameObject _headUI, _fow;

    private Coroutine _stopShakeCoroutine;

    public override void OnStartClient()
    {
        base.OnStartClient();

        _cmCam.gameObject.GetComponent<CinemachineConfiner>().m_BoundingShape2D = GameObject.Find("Confiner").GetComponent<CompositeCollider2D>();
        _cameraPerlin = GetComponentInChildren<CinemachineBasicMultiChannelPerlin>();

        if (IsOwner)
        {
            //Instantiate(allObjects.FOW, transform.position, Quaternion.identity, transform);

            
            _cmCam.Follow = gameObject.transform;

            // tag = "Player";
            //_headUI.GetComponent<SortingGroup>().sortingOrder = 1000;
            // foreach (Transform child in transform)
            // {
            //     if (child.GetComponent<Gun>())
            //     {
            //         foreach (Transform child2 in transform)
            //         {
            //             if (child.GetComponent<Animator>())
            //             {
            //                 child.gameObject.layer = 20;
            //             }
            //         }
            //     }
            // }
            // foreach (Transform child in _headUI.transform)
            // {
            //     child.gameObject.layer = 20;
            // }
            // _headUI.layer = 20;
            // gameObject.layer = 13;
        }
        else
        {
            _cmCam.gameObject.SetActive(false);
            //_fow.SetActive(false);
            //tag = "Enemy";
            //gameObject.layer = 17;
            //GetComponent<AudioListener>().enabled = false;
            // GetComponent<FieldOfView>().enabled = false;
            // GetComponentInChildren<Gun>().enabled = false;
            //headUI.layer = 13;
            // _headUI.GetComponent<SortingGroup>().sortingOrder = (int)this.OwnerClientId;
        }
    }

    public void CameraShake(float amount, float time)
    {
        _cameraPerlin.m_AmplitudeGain = amount;

        if (_stopShakeCoroutine != null)
        {
            StopCoroutine(_stopShakeCoroutine);
            _stopShakeCoroutine = null;
        }

        _stopShakeCoroutine = StartCoroutine(StopShakeCoroutine(time));
    }

    public void StopShake()
    {
        if (_stopShakeCoroutine != null)
        {
            StopCoroutine(_stopShakeCoroutine);
            _stopShakeCoroutine = null;
        }
        _cameraPerlin.m_AmplitudeGain = 0f;
    }

    private IEnumerator StopShakeCoroutine(float time)
    {
        yield return new WaitForSeconds(time);
        _cameraPerlin.m_AmplitudeGain = 0f;
        _stopShakeCoroutine = null;
    }
}
