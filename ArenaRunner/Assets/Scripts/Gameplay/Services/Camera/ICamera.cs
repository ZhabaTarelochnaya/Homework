using DefaultNamespace.Gameplay.World;
using UnityEngine;

namespace Gameplay.Services
{
    public interface ICamera : IPositionUser
    {
        Vector3 Rotation { get; set; }
    }
}