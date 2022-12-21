using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeScreen : MonoBehaviour
{
    [SerializeField] private bool shakeInTheStart;
    [SerializeField] private float[] shakeStages;
    private CinemachineVirtualCamera cameraObj;
    [SerializeField] private Damager _damager;
    
    private void Start()
    {
        cameraObj = GameObject.Find("CM vcam1").GetComponent<CinemachineVirtualCamera>();

        if(shakeInTheStart)
        {
            Shake();
            Invoke(nameof(StopShake), 0.1f);
        }
    }

    public void Shake()
    {
        //Debug.Log(Vector2.Distance(transform.position, cameraObj.transform.position));

        if (Vector2.Distance(transform.position, cameraObj.transform.position) <= 16f)
        {
            cameraObj.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = shakeStages[0] * _damager.Damage;
        }
        else if (Vector2.Distance(transform.position, cameraObj.transform.position) <= 30f)
        {
            cameraObj.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = shakeStages[1] * _damager.Damage;
        }
        else
        {
            cameraObj.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = shakeStages[2] * _damager.Damage;
        }
    }
    public void StopShake()
    {
        cameraObj.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 0;
    }
}