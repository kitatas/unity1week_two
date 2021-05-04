using System.Threading;
using Cysharp.Threading.Tasks;
using NCMB;

namespace Two.OutGame.Domain.UseCase.Interface
{
    public interface IRatingUseCase
    {
        UniTask LoadAllDataAsync(CancellationToken token);
        IScore BuildScore(string scoreText);
        UniTask<NCMBObject> LoadSelfDataAsync(CancellationToken token);
        UniTask SendNameAsync(string playerName, CancellationToken token);
        UniTask SendScoreAsync(int rate, CancellationToken token);
    }
}