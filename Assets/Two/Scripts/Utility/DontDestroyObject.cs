using UnityEngine;

namespace Two.Utility
{
    public sealed class DontDestroyObject : MonoBehaviour
    {
        private static DontDestroyObject _instance = null;

        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
                DontDestroyOnLoad(gameObject);
                return;
            }

            Destroy(gameObject);
        }
    }
}