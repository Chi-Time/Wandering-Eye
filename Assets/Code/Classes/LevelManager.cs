using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private int _GoalAmount = 2;

    private void Awake ()
    {
        AssignReferences ();
    }

    private void AssignReferences ()
    {
        EventManager.OnGoalAmountUpdated += UpdateGoalAmount;
    }

    private void UpdateGoalAmount (int amount)
    {
        _GoalAmount += amount;
    }

    private void OnDestroy ()
    {
        EventManager.OnGoalAmountUpdated -= UpdateGoalAmount;
    }
}
