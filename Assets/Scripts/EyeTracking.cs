using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
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
        System.Array itsValues = System.Enum.GetValues(typeof(InputTrackingState));
    }

    // Update is called once per frame
    void Update()
    {
        int trackingStateValue = Actions.XRIHead.EyeGazeTrackingState.ReadValue<int>();
        Debug.Log($"EyeGazeTrackingState: {trackingStateValue}");
        Debug.Log($"EyePosition: {Actions.XRIHead.EyeGazePosition.ReadValue<Vector3>()}");
        Debug.Log($"EyeRotation: {Actions.XRIHead.EyeGazeRotation.ReadValue<Quaternion>()}");
    }
}
