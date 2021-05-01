using System.Collections.Generic;
using UnityEngine;

namespace Two.Common.Data.DataStore
{
    [CreateAssetMenu(fileName = "SeTable", menuName = "DataTable/SeTable", order = 0)]
    public sealed class SeTable : ScriptableObject
    {
        [SerializeField] private List<SeData> list = default;
        public List<SeData> dataList => list;
    }
}