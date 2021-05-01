using Two.Common.Application;
using UnityEngine;

namespace Two.Common.Domain.UseCase.Interface
{
    public interface IBgmUseCase
    {
        AudioClip FindClip(BgmType type);
    }
}