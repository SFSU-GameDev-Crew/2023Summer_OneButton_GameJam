using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// On Collision Enter tell the gamemanager to remove this component
/// </summary>
public class OnCollisionRemoveElement : MonoBehaviour
{
    private GameManager gameManager;

    private void Start()
    {
        gameManager = GameManager.instance;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        gameManager.DestroyElemental(this.gameObject);
    }
}
