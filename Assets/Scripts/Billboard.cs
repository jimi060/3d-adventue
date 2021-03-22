using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///     MonoBehaviour to add to a simple 2D sprite and make it always look at the camera
/// </summary>
public class Billboard : MonoBehaviour
{
    private void Update()
    {
        transform.LookAt(Camera.main.transform.position, -Vector3.up);
    }
}
