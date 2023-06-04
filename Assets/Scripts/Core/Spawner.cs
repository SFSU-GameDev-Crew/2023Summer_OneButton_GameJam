using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject prefab;

    [Header("Start Timer")]
    [SerializeField] private bool useStartTimer;
    [SerializeField] private float startAfterSeconds;
    [SerializeField] private Cooldown spawnerCooldown;
    
    [Header("Metronome Timer")]
    [SerializeField] private bool useMetronomeTimer;
    [SerializeField] private float interval;
    [SerializeField] private Metronome metronome;

    private void Start()
    {
        if(useStartTimer)
            spawnerCooldown.SetCooldown(startAfterSeconds);
        if(useMetronomeTimer)
            metronome.SetInterval(interval);
    }

    private void OnEnable()
    {
        spawnerCooldown.onCooldownOver += Spawn;
        metronome.onCooldownOver += Spawn;
    }

    private void OnDisable()
    {
        spawnerCooldown.onCooldownOver -= Spawn;
        metronome.onCooldownOver -= Spawn;
    }

    public virtual void Spawn()
    {
        Instantiate(prefab, transform.position, transform.rotation);
    }
}
