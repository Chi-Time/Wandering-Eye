using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private int _CurrentLevel = 1;

    private LevelManager _LevelManager = null;

    private void Awake ()
    {
        AssignReferences ();
    }

    private void AssignReferences ()
    {
        _LevelManager = GetComponent<LevelManager> ();
    }

    public void NextLevel ()
    {
        _LevelManager.NewLevel (_CurrentLevel += 1);
    }

    public void LastLevel ()
    {
        _LevelManager.NewLevel (_CurrentLevel += 1);
    }
}
