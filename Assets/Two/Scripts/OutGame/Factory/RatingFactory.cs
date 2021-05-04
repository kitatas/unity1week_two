using UnityEngine;

namespace Two.OutGame.Factory
{
    public sealed class RatingFactory : MonoBehaviour
    {
        [SerializeField] private RankingNode rankingNode = default;
        [SerializeField] private GameObject readingNode = default;
        [SerializeField] private GameObject notFoundNode = default;
        [SerializeField] private GameObject unavailableNode = default;

        public RankingNode GenerateRankingNode(Transform parent)
        {
            return Instantiate(rankingNode, parent);
        }

        public GameObject GenerateReadingNode(Transform parent)
        {
            return Instantiate(readingNode, parent);
        }

        public GameObject GenerateNotFoundNode(Transform parent)
        {
            return Instantiate(notFoundNode, parent);
        }

        public GameObject GenerateUnavailableNode(Transform parent)
        {
            return Instantiate(unavailableNode, parent);
        }
    }
}