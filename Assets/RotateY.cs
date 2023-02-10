using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateY : MonoBehaviour
{
    void Update()
    {
        transform.Rotate(0f, 0.01f, 0f, Space.World);
    }
}
