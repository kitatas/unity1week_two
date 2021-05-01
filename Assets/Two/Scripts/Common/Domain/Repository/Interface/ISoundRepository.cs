using System.Collections.Generic;
using Two.Common.Data.DataStore;

namespace Two.Common.Domain.Repository.Interface
{
    public interface ISoundRepository
    {
        List<BgmData> bgmData { get; }
        List<SeData> seData { get; }
    }
}