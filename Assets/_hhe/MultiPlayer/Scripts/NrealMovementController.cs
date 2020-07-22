using NRKernal;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NrealMovementController : MonoBehaviour
{
    void Update()
    {
        Transform _playerTransform = NRInput.AnchorsHelper.GetAnchor(ControllerAnchorEnum.GazePoseTrackerAnchor);
        transform.position = Camera.main.transform.position;
        transform.rotation = Quaternion.Euler(0, Camera.main.transform.eulerAngles.y, 0.0f);
    }
}
