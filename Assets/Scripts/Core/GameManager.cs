using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Elementals")]
    [SerializeField] private int numOfSpawns;
    [SerializeField] private List<GameObject> possibleElementals;
    
    [Header("Tick")]
    [SerializeField] private bool useMetronomeTimer;
    [SerializeField] private float interval;
    [SerializeField] private Metronome metronome;

    [Header("Path")]
    // We're using GameObject so that we can make use of Unity's tilemap
    [SerializeField] private List<GameObject> path;

    [Header("Collision")]
    [SerializeField] private GameObject collision;

    private List<Vector3> pathInVector;
    private List<GameObject> elementals;

    private List<int> elementalColorTracker;
    private GameObject prevElemental;
    private int elementalColorCount = 0;

    private void Start()
    {
        if(useMetronomeTimer)
            metronome.SetInterval(interval);

        pathInVector = new List<Vector3>();
        elementals = new List<GameObject>();
        elementalColorTracker = new List<int>();
      //  elementalColorTracker.Add(0);

        // Convert GameObject to Vector3
        foreach (GameObject go in path)
        {
            pathInVector.Add(go.transform.position);
        }
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
            followPathComponent.SetPath(pathInVector);
            
            elementals.Add(newElemental);

            //Checks if colors are repeating
            if (elementalColorTracker.Count > 0)
            {
                //Debug.Log(elementalColorTracker.Count);
                //Debug.Log(prevElemental);
                if (newElemental.GetComponent<SpriteRenderer>().color == prevElemental.GetComponent<SpriteRenderer>().color)
                {
                    elementalColorCount++;                    
                    //Debug.Log("Same color!! " + elementalColorCount + " new: " + newElemental + "   old: " + prevElemental);
                }
                else
                {
                    elementalColorCount = 0;
                }
            }
            //Add number of repeated elements in a row
            elementalColorTracker.Add(elementalColorCount);
            Debug.Log(elementalColorCount);
            //Set prev elemental to curr elemental
            prevElemental = newElemental;

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
        int rng = Random.Range(0, possibleElementals.Count);
        GameObject randomElemental = possibleElementals[rng];
        return Instantiate(randomElemental, transform.position, transform.rotation);
    }

    public void DestroyElemental(GameObject elemental)
    {
        elementals.Remove(elemental);
    }

    public void DestroyMultipleElemental(int index)
    {
        int indexStart = index - elementalColorTracker[index];
        for (int i = indexStart; i <= index; i++)
        {
            elementals.RemoveAt(i);
        }
    }

    public void CheckElemental(GameObject elemental)
    {
        int elementalIndex = elementals.IndexOf(elemental);
        if (elementalColorTracker[elementalIndex] >= 3)
        {
            DestroyMultipleElemental(elementalIndex);
        }
    }
}
