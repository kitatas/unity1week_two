using Photon.Pun;
using Two.InGame.Application;
using Two.InGame.Presentation.View;
using UnityEngine;

namespace Two.InGame.Presentation.Controller
{
    public sealed class PlayerGenerator : MonoBehaviour
    {
        [SerializeField] private GameObject playerController = default;
        [SerializeField] private BallView ballView = default;

        private readonly Vector3 _masterPosition = new Vector3(-5.0f, 0.0f, 0.0f);
        private readonly Vector3 _clientPosition = new Vector3(5.0f, 0.0f, 0.0f);

        public void Generate(PlayerType playerType)
        {
            var position = playerType == PlayerType.Master ? _masterPosition : _clientPosition;
            PhotonNetwork.Instantiate(playerController.name, position, Quaternion.identity);

            if (playerType == PlayerType.Master)
            {
                PhotonNetwork.Instantiate(ballView.name, new Vector3(0.0f, 0.0f, 5.0f), Quaternion.identity);
                PhotonNetwork.Instantiate(ballView.name, new Vector3(0.0f, 0.0f, -5.0f), Quaternion.identity);
            }
        }
    }
}