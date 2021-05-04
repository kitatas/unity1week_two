namespace Two.Common.Application
{
    public sealed class AnimationTime
    {
        public const float UI_MOVE = 0.5f;
        public const float READY = 1.5f;
    }

    public sealed class GameConfig
    {
        public const string DEFAULT_NAME = "no name";
        public const int DEFAULT_RATE = 1500;

        public const string GAME_ID = "dodzwei";
        public const string HASH_TAG1 = "unityroom";
        public const string HASH_TAG2 = "unity1week";
    }

    public sealed class SaveKey
    {
        public const string PLAYER_NAME = "PlayerName";
        public const string PLAYER_RATE = "PlayerRate";
        public const string OBJECT_ID = "ObjectId";
    }

    public sealed class Ncmb
    {
        public const string COLUMN_OBJECT_ID = "objectId";
        public const string COLUMN_SCORE = "score";
        public const string COLUMN_NAME = "name";
        public const string ERROR = "エラー";
    }
}