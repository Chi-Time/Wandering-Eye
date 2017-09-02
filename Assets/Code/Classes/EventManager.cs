public delegate void GoalAmountUpdated (int amount);

public static class EventManager
{
    public static event GoalAmountUpdated OnGoalAmountUpdated;

    public static void UpdateGoalAmount (int amount)
    {
        OnGoalAmountUpdated (amount);
    }
}