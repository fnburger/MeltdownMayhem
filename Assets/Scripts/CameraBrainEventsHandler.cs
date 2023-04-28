using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.Events;

[RequireComponent(typeof(CinemachineBrain))]
public class CameraBrainEventsHandler : MonoBehaviour
{

    public event UnityAction<ICinemachineCamera> OnBlendStarted;
    public event UnityAction<ICinemachineCamera> OnBlendFinished;

    CinemachineBrain _cmBrain;
    Coroutine _trackingBlend;

    void Awake()
    {
        _cmBrain = GetComponent<CinemachineBrain>();
        _cmBrain.m_CameraActivatedEvent.AddListener(OnCameraActivated);
    }

    void OnCameraActivated(ICinemachineCamera newCamera, ICinemachineCamera previousCamera)
    {
        if (newCamera == null)
        {
            Debug.Log("new camera is null");
            return;
        }
        if (previousCamera == null)
        {
            Debug.Log("previous camera is null");
            return;
        }

        Debug.Log($"Blending from {previousCamera.Name} to {newCamera.Name}");

        if (_trackingBlend != null)
            StopCoroutine(_trackingBlend);

        OnBlendStarted?.Invoke(previousCamera);
        _trackingBlend = StartCoroutine(WaitForBlendCompletion());

        IEnumerator WaitForBlendCompletion()
        {
            while (_cmBrain.IsBlending)
            {
                yield return null;
            }

            OnBlendFinished?.Invoke(newCamera);
            _trackingBlend = null;
        }
    }

    void BlendBackToPlayerOne(ICinemachineCamera newCamera, ICinemachineCamera previousCamera)
    {
        // set newCamera to previous Camera
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
