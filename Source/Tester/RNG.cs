class RNG
{
    static readonly Random rnd = new();
    public static (int, int) RollDices()
    {
        return (
            rnd.Next(1, 7),
            rnd.Next(1, 7)
        );
    }
}