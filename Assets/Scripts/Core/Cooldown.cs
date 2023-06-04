using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Cooldown : MonoBehaviour
{
    public Action onCooldownOver;

    public void SetCooldown(float seconds)
    {
        StartCoroutine(StartTimer(seconds));
    }

    private IEnumerator StartTimer(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        onCooldownOver?.Invoke();
    }
}
