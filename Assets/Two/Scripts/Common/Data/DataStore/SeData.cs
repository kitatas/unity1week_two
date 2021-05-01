using Two.Common.Application;
using UnityEngine;

namespace Two.Common.Data.DataStore
{
    [CreateAssetMenu(fileName = "SeData", menuName = "DataTable/SeData", order = 0)]
    public sealed class SeData : ScriptableObject
    {
        [SerializeField] private SeType seType = default;
        [SerializeField] private AudioClip audioClip = default;

        public SeType type => seType;
        public AudioClip clip => audioClip;
    }
}