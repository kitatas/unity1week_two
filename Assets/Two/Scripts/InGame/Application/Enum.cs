namespace Two.InGame.Application
{
    public enum PlayerType
    {
        None = 0,
        Master = 1,
        Client = 2,
    }

    public enum GameState
    {
        None,
        Matching,
        Ready,
        Battle,
        Result,
    }

    public enum MatchingState
    {
        None,
        Connecting,
        Matching,
        Matched,
        Disconnected,
    }
}