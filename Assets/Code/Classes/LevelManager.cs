using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private int _GoalAmount = 2;
    [SerializeField] private Containers _Containers = new Containers ();
    [SerializeField] private LevelGenerator _Generator = new LevelGenerator ();

    private void Awake ()
    {
        AssignReferences ();

        _Generator.GetLevel (0);
    }

    private void AssignReferences ()
    {
        _Generator.Init (_Containers);

        EventManager.OnGoalAmountUpdated += UpdateGoalAmount;
    }

    private void Start ()
    {
        _GoalAmount = _Containers.Goals.Count;
    } 

    private void UpdateGoalAmount (int amount)
    {
        _GoalAmount += amount;

        if (_GoalAmount <= 0)
            print ("Level Complete!");
    }

    private void Update ()
    {
        GetInput ();
    }

    private void GetInput ()
    {

    }

    private void OnDestroy ()
    {
        EventManager.OnGoalAmountUpdated -= UpdateGoalAmount;
    }
}
