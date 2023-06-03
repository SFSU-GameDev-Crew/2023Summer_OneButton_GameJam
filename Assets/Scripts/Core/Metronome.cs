using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Metronome : MonoBehaviour
{
    public Action onCooldownOver;

    public void SetInterval(float seconds)
    {
        StartCoroutine(StartTimer(seconds));
    }

    private IEnumerator StartTimer(float seconds)
    {
        while(true)
        {
            yield return new WaitForSeconds(seconds);
            onCooldownOver?.Invoke();
        }
    }
}
