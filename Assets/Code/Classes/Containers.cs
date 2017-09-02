using UnityEngine;
using System.Collections.Generic;

/// Responsible for holding and manipulating all board containers.
[System.Serializable]
public class Containers
{
    /// The starting positions of all moveable objects.
    public Vector3[] StartingPositions = null;
    /// The checkpointed positions of all moveable objects.
    public Vector3[] CheckpointPositions = null;
    public List<GameObject> Game = new List<GameObject> ();
    /// All current walls positions within the level.
    public List<Transform> Walls = new List<Transform> ();
    /// All current bricks positions within the level.
    public List<Transform> Bricks = new List<Transform> ();
    /// All current goals positions within the level.
    public List<Transform> Goals = new List<Transform> ();
    /// Contains the current number of Bricks that are in goal zones.
    public List<Transform> BricksInGoal = new List<Transform> ();

    public void TrimContainers ()
    {
        Walls.TrimExcess ();
        Bricks.TrimExcess ();
        Goals.TrimExcess ();
    }

    /// Clears the list's of any data held from the last level.
    public void ClearContainers ()
    {
        RemoveObjects ();
        Walls.Clear ();
        Bricks.Clear ();
        Goals.Clear ();
        Game.Clear ();
    }

    private void RemoveObjects ()
    {
        for (int i = 0; i < Game.Count; i++)
            Object.Destroy (Game[i]);
    }
}
