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

    private Containers _Containers = null;

    public void Init (Containers containers)
    {
        _Containers = containers;
    }

    public void GetLevel (int levelNumber)
    {
        ClearLevel ();

        int[][] level = new int[8][];

        level[7] = new int[] { 1, 1, 1, 1, 0, 0, 0, 0 };
        level[6] = new int[] { 1, 2, 2, 1, 0, 0, 0, 0 };
        level[5] = new int[] { 1, 2, 2, 1, 1, 1, 1, 1 };
        level[4] = new int[] { 1, 2, 2, 2, 2, 2, 2, 1 };
        level[3] = new int[] { 1, 1, 4, 5, 1, 3, 2, 1 };
        level[2] = new int[] { 1, 2, 2, 2, 1, 2, 2, 1 };
        level[1] = new int[] { 1, 2, 2, 2, 1, 1, 1, 1 };
        level[0] = new int[] { 1, 1, 1, 1, 1, 1, 1, 1 };

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
                    CreateObject (_Player, new Vector3 (j, i, 0f));

                if (index == (int)SceneObjects.Goal)
                    _Containers.Goals.Add (CreateObject (_Goal, new Vector3 (j, i, 1f)).transform);

                if (index != (int)SceneObjects.Empty && index != (int)SceneObjects.Goal && index != (int)SceneObjects.Wall)
                    CreateObject (_Floor, new Vector3 (j, i, 1f));
            }
        }

        _Containers.TrimContainers ();
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
