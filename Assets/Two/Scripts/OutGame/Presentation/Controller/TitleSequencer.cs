using System.Threading;
using Cysharp.Threading.Tasks;
using Two.Common.Application;
using Two.Common.Presentation.Controller.Sound;
using Two.OutGame.Domain.UseCase.Interface;
using Two.OutGame.Presentation.View;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace Two.OutGame.Presentation.Controller
{
    public sealed class TitleSequencer : MonoBehaviour
    {
        [SerializeField] private Image loadingImage = default;
        [SerializeField] private RatingView ratingView = default;
        [SerializeField] private NameRegisterView nameRegisterView = default;

        private string _currentPlayerName;
        private string _currentPlayerRate;

        private BgmController _bgmController;
        private SeController _seController;
        private IRatingUseCase _ratingUseCase;

        [Inject]
        private void Construct(BgmController bgmController, SeController seController, IRatingUseCase ratingUseCase)
        {
            _bgmController = bgmController;
            _seController = seController;
            _ratingUseCase = ratingUseCase;
        }

        private void Awake()
        {
            _bgmController.Play(BgmType.Main);
        }

        private void Start()
        {
            var token = this.GetCancellationTokenOnDestroy();
            InitAsync(token).Forget();

            nameRegisterView
                .RegisterAsObservable()
                .Subscribe(_ =>
                {
                    var inputName = nameRegisterView.GetInputName();

                    // 入力値が空 or 同じ値の場合は処理しない
                    if (string.IsNullOrWhiteSpace(inputName) ||
                        _currentPlayerName == inputName)
                    {
                        nameRegisterView.Registered();
                    }
                    else
                    {
                        RegisterNameAsync(inputName, token).Forget();
                    }
                })
                .AddTo(nameRegisterView);
        }

        private async UniTask InitAsync(CancellationToken token)
        {
            await _ratingUseCase.LoadAllDataAsync(token);

            var ncmbObject = await _ratingUseCase.LoadSelfDataAsync(token);
            _currentPlayerName = $"{ncmbObject[Ncmb.COLUMN_NAME]}";
            _currentPlayerRate = $"{ncmbObject[Ncmb.COLUMN_SCORE]}";

            nameRegisterView.SetName(_currentPlayerName);
            ratingView.SetPlayerInfo(_currentPlayerName, _currentPlayerRate);

            loadingImage.gameObject.SetActive(false);
        }

        private async UniTask RegisterNameAsync(string newPlayerName, CancellationToken token)
        {
            _seController.Play(SeType.Decision);
            nameRegisterView.Registering();

            await _ratingUseCase.SendNameAsync(newPlayerName, token);

            await InitAsync(token);

            _seController.Play(SeType.Decision);
            nameRegisterView.Registered();
        }
    }
}