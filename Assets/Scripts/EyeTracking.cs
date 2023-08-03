using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;

public class EyeTracking : MonoBehaviour
{
    XRIDefaultInputActions Actions;

    // Start is called before the first frame update
    void Start()
    {
        Actions = new XRIDefaultInputActions();
        Actions.XRIHead.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        /* Debug.Log($"EyePosition: {Actions.XRIHead.EyeGazePosition.ReadValue<Vector3>()}"); */
        /* var centerPosition = EyeCenterPosition.action.ReadValue<Vector3>(); */
        /* var gazePose = EyeGazePose.action.ReadValue<PoseState>(); */
    }
}
