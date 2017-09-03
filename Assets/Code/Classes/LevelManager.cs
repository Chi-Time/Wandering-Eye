using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public int Pushes = 0;
    public int Moves = 0;
    public Containers _Containers = new Containers ();
    public LevelGenerator _Generator = new LevelGenerator ();
    public CheckpointController _CheckpointController = new CheckpointController ();

    [SerializeField] private int _GoalAmount = 2;

    private void Awake ()
    {
        AssignReferences ();
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
        _Generator.GetLevel (0);
        _GoalAmount = _Containers.Goals.Count;
    } 

    private void UpdateGoalAmount (int amount)
    {
        _GoalAmount += amount;

        if (_GoalAmount <= 0)
        {
            EventManager.ChangeState (GameStates.LevelComplete);
            Pushes = 0;
            Moves = 0;
        }
    }

    private void BrickPushed (int amount, bool isCaller)
    {
        if (!isCaller)
            EventManager.PushBrick (Pushes += amount, true);
    }

    private void PlayerMoved (int amount, bool isCaller)
    {
        if (!isCaller)
            EventManager.PlayerMoved (Moves += amount, true);
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
