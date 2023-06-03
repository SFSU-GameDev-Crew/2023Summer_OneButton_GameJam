using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IFollowPath
{
    public void SetStartOffset(int offset);
    
    public void SetPath(List<Vector2> path);
    
    public void MoveToNextNode();
}
