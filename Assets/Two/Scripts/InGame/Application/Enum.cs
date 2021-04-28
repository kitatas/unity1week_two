namespace Two.InGame.Application
{
    public enum PlayerType
    {
        None,
        Master,
        Client,
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