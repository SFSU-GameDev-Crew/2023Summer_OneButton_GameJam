using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTriggerLose : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameManager.instance.Lose();
    }
}
