using System.Collections.Generic;
using Two.Common.Data.DataStore;
using Two.Common.Domain.Repository.Interface;

namespace Two.Common.Domain.Repository
{
    public sealed class SoundRepository : ISoundRepository
    {
        private readonly BgmTable _bgmTable;
        private readonly SeTable _seTable;

        public SoundRepository(BgmTable bgmTable, SeTable seTable)
        {
            _bgmTable = bgmTable;
            _seTable = seTable;
        }

        public List<BgmData> bgmData => _bgmTable.dataList;

        public List<SeData> seData => _seTable.dataList;
    }
}