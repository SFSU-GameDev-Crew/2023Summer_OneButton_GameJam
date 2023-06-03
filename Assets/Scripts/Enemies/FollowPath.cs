using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPath : MonoBehaviour, IFollowPath
{
    [SerializeField] private GameObject navigatingAgent;
    
    [SerializeField] private List<Vector3> path;
    private int offset = 0;

    public void SetStartOffset(int offset)
    {
        this.offset = offset;
    }
    
    public void SetPath(List<Vector3> path)
    {
        this.path = path;
    }
    
    public void MoveToNextNode()
    {
        if(offset >= 0 && offset < path.Count)
        {
            // Set position
            // Must convert to 3D coordinate
            navigatingAgent.transform.position = path[offset];
        }
        offset++;
    }
}
