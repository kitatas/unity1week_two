using Photon.Realtime;
using Two.Common.Application;

namespace Two.Utility
{
    public static class PunExtension
    {
        public static string GetName(this Player player)
        {
            return player.CustomProperties[SaveKey.PLAYER_NAME] is string name ? name : GameConfig.DEFAULT_NAME;
        }

        public static int GetRate(this Player player)
        {
            return player.CustomProperties[SaveKey.PLAYER_RATE] is int rate ? rate : GameConfig.DEFAULT_RATE;
        }
    }
}