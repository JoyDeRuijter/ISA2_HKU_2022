using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.InputSystem;
using StarterAssets;

public class CinemachineShake : MonoBehaviour
{
    public static CinemachineShake Instance { get; private set; }

    [SerializeField] private float intensity;
    [SerializeField] private float shakeTime;
    private CinemachineVirtualCamera cinemachineVC;
    private CinemachineBasicMultiChannelPerlin cinemachineBMCP;
    private bool isShaking;
    [SerializeField] private ThirdPersonShooterController shooterController;

    private void Awake()
    {
        Instance = this;
        cinemachineVC = GetComponent<CinemachineVirtualCamera>();
        cinemachineBMCP = cinemachineVC.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    private void Update()
    {
        if (shooterController.shoot)
        {
            StartCoroutine(StartShaking(0f));
            StartCoroutine(StopShaking());
        }

        if (isShaking)
            cinemachineBMCP.m_AmplitudeGain = intensity;
        else
            cinemachineBMCP.m_AmplitudeGain = 0f;
    }

    public IEnumerator StopShaking()
    {
        yield return new WaitForSeconds(shakeTime);
        isShaking = false;
    }

    public IEnumerator StartShaking(float inBetweenTime)
    {
        yield return new WaitForSeconds(inBetweenTime);
        isShaking = true;
    }
}
