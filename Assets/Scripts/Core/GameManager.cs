using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Elementals")]
    [SerializeField] private int numOfSpawns;
    [SerializeField] private GameObject elemental;
    
    [Header("Tick")]
    [SerializeField] private bool useMetronomeTimer;
    [SerializeField] private float interval;
    [SerializeField] private Metronome metronome;

    [SerializeField] private List<Vector2> path;

    [SerializeField] private List<GameObject> elementals;

    private void Start()
    {
        if(useMetronomeTimer)
            metronome.SetInterval(interval);
    }

    private void OnEnable()
    {
        metronome.onCooldownOver += Tick;
    }

    private void OnDisable()
    {
        metronome.onCooldownOver -= Tick;
    }

    private void Tick()
    {
        if(numOfSpawns > 0)
        {
            GameObject newElemental = SpawnElemental();
            IFollowPath followPathComponent = newElemental.GetComponent<IFollowPath>();
            followPathComponent.SetPath(path);
            
            elementals.Add(newElemental);
            numOfSpawns--;
        }

        // Move each elemental
        foreach (GameObject element in elementals)
        {
            IFollowPath followPathComponent = element.GetComponent<IFollowPath>();
            followPathComponent.MoveToNextNode();
        }
    }

    private GameObject SpawnElemental()
    {
        return Instantiate(elemental, transform.position, transform.rotation);
    }

    public void DestroyElemental(GameObject elemental)
    {
        elementals.Remove(elemental);
    }
}
