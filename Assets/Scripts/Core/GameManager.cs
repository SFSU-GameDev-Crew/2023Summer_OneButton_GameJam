using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance; // Singleton

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
    private List<Vector3> pathInVector;

    private List<GameObject> elementals;

    private int tickCounter = 0;
    private int spawnCounter = 0;

    [Header("Scene Transition")]
    [SerializeField] private string nextScene;
    [SerializeField] private string loseScene;

    private void Start()
    {
        if (instance != null)
        {
            Destroy(this.gameObject);
        }
        instance = this;

        spawnCounter = numOfSpawns;
        if(useMetronomeTimer)
            metronome.SetInterval(interval);

        pathInVector = new List<Vector3>();
        elementals = new List<GameObject>();
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
        if (tickCounter > path.Count + numOfSpawns)
        {
            SceneManager.LoadScene(loseScene);
        }
        else if (elementals.Count == 0 && spawnCounter == 0)
        {
            SceneManager.LoadScene(nextScene);
        }


        if(spawnCounter > 0)
        {
            GameObject newElemental = SpawnElemental();
            IFollowPath followPathComponent = newElemental.GetComponent<IFollowPath>();
            followPathComponent.SetPath(pathInVector);
            
            elementals.Add(newElemental);
            spawnCounter--;
        }

        // Move each elemental
        foreach (GameObject element in elementals)
        {
            IFollowPath followPathComponent = element.GetComponent<IFollowPath>();
            followPathComponent.MoveToNextNode();
        }

        tickCounter++;
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
}
