using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Camera camera;
    private Vector3 offset = new Vector3(2.5f, -0.5f, -20f);

    private void FixedUpdate()
    {
        camera.transform.position = gameObject.transform.position + offset;
    }
}
