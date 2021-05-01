using Two.Common.Application;
using UnityEngine;

namespace Two.Common.Data.DataStore
{
    [CreateAssetMenu(fileName = "BgmData", menuName = "DataTable/BgmData", order = 0)]
    public sealed class BgmData : ScriptableObject
    {
        [SerializeField] private BgmType bgmType = default;
        [SerializeField] private AudioClip audioClip = default;

        public BgmType type => bgmType;
        public AudioClip clip => audioClip;
    }
}