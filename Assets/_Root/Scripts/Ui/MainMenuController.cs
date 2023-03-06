using Profile;
using Tool;
using UnityEngine;
using Object = UnityEngine.Object;
using Services.Ads.UnityAds;
using Services.IAP;

namespace Ui
{
    internal class MainMenuController : BaseController
    {
        private readonly ResourcePath _resourcePath = new ResourcePath("Prefabs/Ui/MainMenu");
        private readonly ProfilePlayer _profilePlayer;
        private readonly MainMenuView _view;

        private UnityAdsService _adsService;
        private IAPService _iapService;

        public MainMenuController(Transform placeForUi, ProfilePlayer profilePlayer, UnityAdsService adsService, IAPService iapService)
        {
            _profilePlayer = profilePlayer;
            _view = LoadView(placeForUi);
            _view.Init(StartGame, ShowSettings, GetRewarded, BuyGoods, OpenShed);

            _adsService = adsService;
            _iapService = iapService;
        }

        private MainMenuView LoadView(Transform placeForUi)
        {
            GameObject prefab = ResourcesLoader.LoadPrefab(_resourcePath);
            GameObject objectView = Object.Instantiate(prefab, placeForUi, false);
            AddGameObject(objectView);

            return objectView.GetComponent<MainMenuView>();
        }

        private void StartGame() =>
            _profilePlayer.CurrentState.Value = GameState.Game;

        private void ShowSettings() =>
            _profilePlayer.CurrentState.Value = GameState.Settings;

        private void GetRewarded() =>
            _adsService.RewardedPlayer.Play();

        private void OpenShed() =>
            _profilePlayer.CurrentState.Value = GameState.Shed;


        private void BuyGoods()
        {
            if (_iapService.IsInitialized) OnIapInitialized();
            else _iapService.Initialized.AddListener(OnIapInitialized);
        }

        protected override void OnDispose() 
        {
            _iapService.Initialized.RemoveListener(OnIapInitialized);
        }

        private void OnIapInitialized() => _iapService.Buy("1_test");

    }
}
