using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField] private bool invert;
    [SerializeField] private float rotationSpeed;

    private void FixedUpdate()
    {
        transform.rotation *= Quaternion.Euler(0, 0, (invert ? -rotationSpeed : rotationSpeed) * Time.fixedDeltaTime);
    }
}
