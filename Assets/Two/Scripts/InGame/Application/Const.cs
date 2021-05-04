namespace Two.InGame.Application
{
    public sealed class PlayerParam
    {
        public const int MAX_STOCK_COUNT = 2;
        public const int MAX_HP = 2;
    }

    public sealed class GameParam
    {
        public const int UPDATE_RATE_VALUE = 16;
        public const GameState INIT_GAME_STATE = GameState.Matching;
        public const MatchingState INIT_MATCHING_STATE = MatchingState.None;
    }
}