namespace Two.InGame.Application
{
    public class PlayerParam
    {
        public const int MAX_STOCK_COUNT = 2;
        public const int MAX_HP = 2;
        public const string DEFAULT_NAME = "no name";
    }

    public class GameParam
    {
        public const GameState INIT_GAME_STATE = GameState.Matching;
        public const MatchingState INIT_MATCHING_STATE = MatchingState.None;
    }

    public class SaveKey
    {
        public const string PLAYER_NAME = "PlayerName";
    }
}