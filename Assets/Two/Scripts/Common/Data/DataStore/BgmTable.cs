using System.Collections.Generic;
using UnityEngine;

namespace Two.Common.Data.DataStore
{
    [CreateAssetMenu(fileName = "BgmTable", menuName = "DataTable/BgmTable", order = 0)]
    public sealed class BgmTable : ScriptableObject
    {
        [SerializeField] private List<BgmData> list = default;
        public List<BgmData> dataList => list;
    }
}