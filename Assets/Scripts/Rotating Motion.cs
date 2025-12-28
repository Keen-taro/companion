using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingMotion : MonoBehaviour
{
    void Update()
    {
        transform.Rotate(0f, 0f, 45f * Time.deltaTime);
    }
}
