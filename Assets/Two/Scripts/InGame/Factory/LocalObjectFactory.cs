using UnityEngine;

namespace Two.InGame.Factory
{
    public sealed class LocalObjectFactory : MonoBehaviour
    {
        [SerializeField] private GameObject deadEffect = default;

        public void GenerateDeadEffect(Vector3 position)
        {
            Instantiate(deadEffect, position, Quaternion.identity);
        }
    }
}