using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinObjectNew : MonoBehaviour
{
     public float spinSpeed = 180f;

    void Update()
    {
        // Spin the object around its Y-axis
        transform.Rotate(Vector3.forward, spinSpeed * Time.deltaTime);
    }
}
