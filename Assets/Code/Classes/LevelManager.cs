using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour
{
    public int Pushes = 0;
    public int Moves = 0;

    [SerializeField] private int _GoalAmount = 2;
    [SerializeField] private Containers _Containers = new Containers ();
    [SerializeField] private LevelGenerator _Generator = new LevelGenerator ();
    [SerializeField] private CheckpointController _CheckpointController = new CheckpointController ();

    private void Awake ()
    {
        AssignReferences ();

        _Generator.GetLevel (0);
    }

    private void AssignReferences ()
    {
        _Generator.Init (_Containers, _CheckpointController);
        _CheckpointController.Init (_Containers, this);

        EventManager.OnGoalAmountUpdated += UpdateGoalAmount;
        EventManager.OnBrickPushed += BrickPushed;
        EventManager.OnPlayerMoved += PlayerMoved;
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

    private void BrickPushed (int amount, bool isCaller)
    {
        if (!isCaller)
            EventManager.PushBrick (Pushes += amount, true);
    }

    private void PlayerMoved (int amount, bool isCaller)
    {
        if (!isCaller)
            EventManager.PushBrick (Moves += amount, true);
    }

    private void Update ()
    {
        _CheckpointController.GetInput ();
    }

    private void OnDestroy ()
    {
        EventManager.OnGoalAmountUpdated -= UpdateGoalAmount;
        EventManager.OnBrickPushed -= BrickPushed;
        EventManager.OnPlayerMoved -= PlayerMoved;
    }
}
