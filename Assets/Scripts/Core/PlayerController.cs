using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private List<GameObject> possibleElementals;

    [Header("Elemental Placeholder")]
    [SerializeField] private float elementalSpawnTimer = 0.5f;
    [SerializeField] private float fireCooldownInSeconds;
    [SerializeField] private Cooldown fireCooldown;
    [SerializeField] private GameObject placeholder;
    private bool canShoot;

    [Header("Player Rotation")]
    [SerializeField] private float rotationSpeed = 1.0f;

    private ILaunch currentElemental; 
    [SerializeField] private GameObject currentElementalGameObject;

    private void Start()
    {
        SpawnElemental();
        canShoot = true;
    }

    private void OnEnable()
    {
        fireCooldown.onCooldownOver += ResetShootTimer;
    }
    
    private void OnDisable()
    {
        fireCooldown.onCooldownOver -= ResetShootTimer;
    }

    private void ResetShootTimer()
    {
        canShoot = true;
    }

    private void FixedUpdate()
    {
        // Shoots a projectile in the forward direction
        if(Input.GetButtonDown("Jump") && canShoot)
        {
            canShoot = false;
            fireCooldown.SetCooldown(fireCooldownInSeconds);
            // Choose random element and launches it forward
            // from the player's direction
            StartCoroutine("PerformShoot");
        }

        // Rotates the Camera
        if(Input.GetButton("Jump"))
        {
            transform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime * Mathf.Rad2Deg);
        }
    }

    private GameObject SpawnElemental()
    {
        // Choose random element
        int rng = Random.Range(0, possibleElementals.Count);
        GameObject randomElemental = possibleElementals[rng];
        
        // Spawn Elemental
        currentElementalGameObject = Instantiate(randomElemental, placeholder.transform.position, transform.rotation, placeholder.transform);
        
        // Launch Elemental
        currentElemental = currentElementalGameObject.GetComponent<ILaunch>();

        return currentElementalGameObject;
    }

    // Wait is needed for the projectile to leave from the placeholder
    private IEnumerator PerformShoot()
    {
        currentElementalGameObject.transform.SetParent(null, true);
        currentElemental.Launch();
        yield return new WaitForSeconds(elementalSpawnTimer);
        SpawnElemental();
    }
}
