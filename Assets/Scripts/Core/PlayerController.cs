using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private bool isOn;
    [SerializeField] private GameObject projectile_prefab;
    [SerializeField] private float forward_offset = 1.0f;

    [Header("Player Rotation")]
    [SerializeField] private float rotationSpeed = 1.0f;

    private void Update()
    {
        // Shoots a projectile in the forward direction
        if(Input.GetButtonDown("Jump"))
        {
            Vector3 offset_position = transform.position + transform.right * forward_offset;
            Instantiate(projectile_prefab, offset_position, transform.rotation);
        }

        // Rotates the Camera
        if(Input.GetButton("Jump"))
        {
            transform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime * Mathf.Rad2Deg);
        }
    }
}
