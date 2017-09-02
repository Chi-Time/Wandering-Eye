using UnityEngine;
using System.Collections;

enum SceneObjects
{
    Empty = 0,
    Wall = 1,
    Floor = 2,
    Brick = 3,
    Player = 4,
    Goal = 5
};

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] private GameObject _Floor = null;
    [SerializeField] private GameObject _Player = null;
    [SerializeField] private GameObject _Brick = null;
    [SerializeField] private GameObject _Goal = null;
    [SerializeField] private GameObject _Wall = null;

    private void Awake ()
    {
        GenerateLevel (GetLevel ());
    }

    private int[][] GetLevel ()
    {
        int[][] level = new int[8][];

        level[7] = new int[] { 1, 1, 1, 1, 0, 0, 0, 0 };
        level[6] = new int[] { 1, 2, 2, 1, 0, 0, 0, 0 };
        level[5] = new int[] { 1, 2, 2, 1, 1, 1, 1, 1 };
        level[4] = new int[] { 1, 2, 2, 2, 2, 2, 2, 1 };
        level[3] = new int[] { 1, 1, 4, 5, 1, 3, 2, 1 };
        level[2] = new int[] { 1, 2, 2, 2, 1, 2, 2, 1 };
        level[1] = new int[] { 1, 2, 2, 2, 1, 1, 1, 1 };
        level[0] = new int[] { 1, 1, 1, 1, 1, 1, 1, 1 };

        return level;
    }

    private void GenerateLevel (int[][] level)
    {
        for(int i = 0; i < level.Length; i++)
        {
            for(int j = 0; j < level[i].Length; j++)
            {
                int index = (int)level[i].GetValue (j);

                if (index == (int)SceneObjects.Wall)
                    CreateObject (_Wall, new Vector3 (j, i, 0f));

                if (index == (int)SceneObjects.Brick)
                    CreateObject (_Brick, new Vector3 (j, i, 0f));

                if (index == (int)SceneObjects.Player)
                    CreateObject (_Player, new Vector3 (j, i, 0f));

                if (index == (int)SceneObjects.Goal)
                    CreateObject (_Goal, new Vector3 (j, i, 1f));

                if (index != (int)SceneObjects.Empty)
                    CreateObject (_Floor, new Vector3 (j, i, 1f));
            }
        }
    }

    private GameObject CreateObject (GameObject prefab, Vector3 position)
    {
        return Instantiate (prefab, position, Quaternion.identity);
    }
}
