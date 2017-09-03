public delegate void GoalAmountUpdated (int amount);
public delegate void StateChanged (GameStates state);
public delegate void BrickPushed (int amount, bool isCaller);
public delegate void PlayerMoved (int amount, bool isCaller);

public static class EventManager
{
    public static event GoalAmountUpdated OnGoalAmountUpdated;
    public static event StateChanged OnStateChanged;
    public static event BrickPushed OnBrickPushed;
    public static event PlayerMoved OnPlayerMoved;

    public static void UpdateGoalAmount (int amount)
    {
        OnGoalAmountUpdated (amount);
    }

    public static void ChangeState (GameStates state)
    {
        OnStateChanged (state);
    }

    public static void PushBrick (int amount, bool isCaller)
    {
        OnBrickPushed (amount, isCaller);
    }

    public static void PlayerMoved (int amount, bool isCaller)
    {
        OnPlayerMoved (amount, isCaller);
    }
}
