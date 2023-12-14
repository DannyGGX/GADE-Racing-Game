using System;
using Cinemachine;
using UnityEngine;

public class ChaseCameraTargetLocator : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera cinemachineVirtualCamera;
    private CinemachineTransposer transposer;
    private CinemachineComposer composer;
    [SerializeField] private float speed = 5;
    private void Start()
    {
        transposer = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineTransposer>();
        transposer.m_XDamping = speed;
        transposer.m_YDamping = speed;
        transposer.m_ZDamping = speed;
        composer = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineComposer>();
        composer.m_HorizontalDamping = speed;
        composer.m_VerticalDamping = speed;
        Invoke(nameof(TargetPlayer), 0.0001f);
    }

    private void TargetPlayer()
    {
        Transform player = GameObject.FindWithTag("Player").transform;
        cinemachineVirtualCamera.LookAt = player.transform.Find("Camera LookAt Rotator").transform.Find("Camera LookAt");
        cinemachineVirtualCamera.Follow = player.transform.Find("Camera Follow Target");
        Invoke(nameof(SetDampingBackToNormal), 2.2f);
    }

    private void SetDampingBackToNormal()
    {
        transposer.m_XDamping = 0.8f;
        transposer.m_YDamping = 0.6f;
        transposer.m_ZDamping = 0.8f;
        composer.m_HorizontalDamping = 0.5f;
        composer.m_VerticalDamping = 0.5f;
        Destroy(gameObject);
    }
}

