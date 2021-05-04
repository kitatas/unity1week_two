using System.Threading;
using Cysharp.Threading.Tasks;

namespace Two.InGame.Domain.UseCase.Interface
{
    public interface IRatingUseCase
    {
        UniTask SendScoreAsync(int rate, CancellationToken token);
    }
}