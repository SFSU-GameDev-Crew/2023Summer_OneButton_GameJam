using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private List<GameObject> possibleElementals;
    [SerializeField] private float forward_offset = 1.0f;

    [Header("Player Rotation")]
    [SerializeField] private float rotationSpeed = 1.0f;

    private void Update()
    {
        // Shoots a projectile in the forward direction
        if(Input.GetButtonDown("Jump"))
        {
            // Choose random element and launches it forward
            // from the player's direction
            SpawnAndLaunchElemental();
        }

        // Rotates the Camera
        if(Input.GetButton("Jump"))
        {
            transform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime * Mathf.Rad2Deg);
        }
    }

    private GameObject SpawnAndLaunchElemental()
    {
        // Choose random element
        int rng = Random.Range(0, possibleElementals.Count);
        GameObject randomElemental = possibleElementals[rng];
        
        // Sets the offset position
        Vector3 offset_position = transform.position + transform.right * forward_offset;
        return Instantiate(randomElemental, offset_position, transform.rotation);
    }
}
