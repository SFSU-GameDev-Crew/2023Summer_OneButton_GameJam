using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistentAudio : MonoBehaviour
{
    [SerializeField] private bool shouldNeverDestroy;
    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}
