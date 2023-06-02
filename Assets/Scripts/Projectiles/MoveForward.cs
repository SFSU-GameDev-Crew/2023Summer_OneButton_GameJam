using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForward : MonoBehaviour
{
    [SerializeField] private float force_magnitude = 1.0f;
    [SerializeField] private Rigidbody2D rigid_body_component;

    private void Start()
    {
        Vector3 force_direction = transform.right;
        Vector3 force = force_magnitude * force_direction;
        rigid_body_component.AddForce(force, ForceMode2D.Impulse);
    }
}