using Two.Common.Application;
using Two.Common.Domain.Repository.Interface;
using UnityEngine;

namespace Two.Common.Domain.Repository
{
    public sealed class PlayerDataRepository : IPlayerDataRepository
    {
        public string LoadName()
        {
            return PlayerPrefs.HasKey(SaveKey.PLAYER_NAME)
                ? PlayerPrefs.GetString(SaveKey.PLAYER_NAME)
                : GameConfig.DEFAULT_NAME;
        }

        public void SaveName(string name)
        {
            PlayerPrefs.SetString(SaveKey.PLAYER_NAME, name);
        }

        public int LoadRate()
        {
            return PlayerPrefs.HasKey(SaveKey.PLAYER_RATE)
                ? PlayerPrefs.GetInt(SaveKey.PLAYER_RATE)
                : GameConfig.DEFAULT_RATE;
        }

        public void SaveRate(int rate)
        {
            PlayerPrefs.SetInt(SaveKey.PLAYER_RATE, rate);
        }
    }
}