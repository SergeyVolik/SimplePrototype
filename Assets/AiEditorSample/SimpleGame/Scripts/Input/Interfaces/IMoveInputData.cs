namespace SerV112.UtilityAI.Game
{
    public interface IMoveInputData
    {
        float Horizontal { get; }
        float Vertical { get; }

        bool IsMove { get; }
    }
}
