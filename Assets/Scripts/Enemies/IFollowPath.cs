using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IFollowPath
{
    public void SetStartOffset(int offset);
    
    public void SetPath(List<Vector3> path);
    
    public void MoveToNextNode();
}
