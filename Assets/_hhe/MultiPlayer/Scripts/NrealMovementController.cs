using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NrealMovementController : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        transform.position = Camera.main.transform.position;
        transform.rotation = Camera.main.transform.rotation;
    }
}
