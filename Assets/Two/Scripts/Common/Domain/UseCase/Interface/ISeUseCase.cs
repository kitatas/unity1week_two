using Two.Common.Application;
using UnityEngine;

namespace Two.Common.Domain.UseCase.Interface
{
    public interface ISeUseCase
    {
        AudioClip FindClip(SeType type);
    }
}