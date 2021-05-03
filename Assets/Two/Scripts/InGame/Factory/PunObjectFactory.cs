using Photon.Pun;
using Two.InGame.Application;
using Two.InGame.Presentation.View;
using UnityEngine;

namespace Two.InGame.Factory
{
    public sealed class PunObjectFactory : MonoBehaviour
    {
        [SerializeField] private GameObject player = default;
        [SerializeField] private BallView ballView = default;

        private readonly Vector3 _masterPosition = new Vector3(-5.0f, 0.0f, 0.0f);
        private readonly Vector3 _clientPosition = new Vector3(5.0f, 0.0f, 0.0f);

        public void Generate(PlayerType playerType)
        {
            var position = playerType == PlayerType.Master ? _masterPosition : _clientPosition;
            GeneratePlayer(position);

            if (playerType == PlayerType.Master)
            {
                GenerateBall();
            }
        }

        private void GeneratePlayer(Vector3 position)
        {
            PhotonNetwork.Instantiate(player.name, position, Quaternion.identity);
        }

        private void GenerateBall()
        {
            PhotonNetwork.Instantiate(ballView.name, new Vector3(0.0f, 0.0f, 5.0f), Quaternion.identity);
            PhotonNetwork.Instantiate(ballView.name, new Vector3(0.0f, 0.0f, -5.0f), Quaternion.identity);
        }
    }
}