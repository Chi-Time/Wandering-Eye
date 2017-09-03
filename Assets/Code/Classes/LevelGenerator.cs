using UnityEngine;

enum SceneObjects
{
    Empty = 0,
    Wall = 1,
    Floor = 2,
    Brick = 3,
    Player = 4,
    Goal = 5
};

[System.Serializable]
public class LevelGenerator
{
    [SerializeField] private GameObject _Floor = null;
    [SerializeField] private GameObject _Player = null;
    [SerializeField] private GameObject _Brick = null;
    [SerializeField] private GameObject _Goal = null;
    [SerializeField] private GameObject _Wall = null;

    private Levels _Levels = new Levels ();
    private Containers _Containers = null;
    private CheckpointController _CheckpointController = null;

    public void Init (Containers containers, CheckpointController checkpointController)
    {
        _Containers = containers;
        _CheckpointController = checkpointController;
    }

    public void GetNewLevel (int levelNumber)
    {
        ClearLevel ();

        int[][] level = _Levels.GetLevel (levelNumber);

        GenerateLevel (level);
    }

    private void GenerateLevel (int[][] level)
    {
        new GameObject ("Level");

        for(int i = 0; i < level.Length; i++)
        {
            for(int j = 0; j < level[i].Length; j++)
            {
                int index = (int)level[i].GetValue (j);

                if (index == (int)SceneObjects.Wall)
                    _Containers.Walls.Add (CreateObject (_Wall, new Vector3 (j, i, 0f)).transform);

                if (index == (int)SceneObjects.Brick)
                    _Containers.Bricks.Add (CreateObject (_Brick, new Vector3 (j, i, 0f)).transform);

                if (index == (int)SceneObjects.Player)
                    _Containers.Player = CreateObject (_Player, new Vector3 (j, i, 0f));

                if (index == (int)SceneObjects.Goal)
                    _Containers.Goals.Add (CreateObject (_Goal, new Vector3 (j, i, 1f)).transform);

                if (index != (int)SceneObjects.Empty && index != (int)SceneObjects.Goal && index != (int)SceneObjects.Wall)
                    CreateObject (_Floor, new Vector3 (j, i, 1f));
            }
        }

        _Containers.TrimContainers ();
        _CheckpointController.StoreStartPositions ();
        EventManager.ChangeState (GameStates.InGame);
    }

    private GameObject CreateObject (GameObject prefab, Vector3 position)
    {
        var go = Object.Instantiate (prefab, position, Quaternion.identity);
        go.transform.SetParent (GetHolder (prefab.name + "s"));
        _Containers.Game.Add (go);
        return go;
    }

    private Transform GetHolder (string holderName)
    {
        var holder = GameObject.Find (holderName);

        if (holder != null)
            return holder.transform;

        var newHolder = new GameObject (holderName).transform;
        newHolder.SetParent (GameObject.Find ("Level").transform);
        return newHolder;
    }

    private void ClearLevel ()
    {
        _Containers.ClearContainers ();
    }
}
