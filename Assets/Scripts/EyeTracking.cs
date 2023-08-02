using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;

public class EyeTracking : MonoBehaviour
{
    [SerializeField] private InputActionReference EyeCenterPosition;
    [SerializeField] private InputActionReference EyeGazePose;
    [SerializeField] private InputActionAsset EyeAsset;
    // Start is called before the first frame update
    void Start()
    {
        if (EyeAsset != null)
        {
            EyeAsset.Enable();
        }
    }

    // Update is called once per frame
    void Update()
    {
        var centerPosition = EyeCenterPosition.action.ReadValue<Vector3>();
        Debug.Log(centerPosition);
        var gazePose = EyeGazePose.action.ReadValue<PoseState>();
    }
}
