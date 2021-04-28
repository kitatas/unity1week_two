using Two.InGame.Application;
using Two.InGame.Domain.Repository.Interface;
using UnityEngine;

namespace Two.InGame.Domain.Repository
{
    public sealed class NameRepository : INameRepository
    {
        public string Load()
        {
            return PlayerPrefs.HasKey(SaveKey.PLAYER_NAME)
                ? PlayerPrefs.GetString(SaveKey.PLAYER_NAME)
                : PlayerParam.DEFAULT_NAME;
        }

        public void Save(string name)
        {
            PlayerPrefs.SetString(SaveKey.PLAYER_NAME, name);
        }
    }
}