using Two.Common.Application;
using Two.OutGame.Domain.Repository.Interface;
using UnityEngine;

namespace Two.OutGame.Domain.Repository
{
    public sealed class NcmbRepository : INcmbRepository
    {
        private string _objectId = null;

        public string objectId
        {
            get => _objectId ?? (_objectId = PlayerPrefs.GetString(SaveKey.OBJECT_ID, null));
            set
            {
                if (_objectId == value)
                {
                    return;
                }

                PlayerPrefs.SetString(SaveKey.OBJECT_ID, _objectId = value);
            }
        }
    }
}