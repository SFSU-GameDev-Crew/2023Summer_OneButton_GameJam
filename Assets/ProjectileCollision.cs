using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileCollision : MonoBehaviour
{
    public GameManager gameManager;

    private void OnTriggerEnter(Collider co)
    {
         Debug.Log("Collision!");
         gameManager.CheckElemental(co.gameObject);
    }
}
