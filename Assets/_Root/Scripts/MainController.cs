using Ui;
using Game;
using Profile;
using UnityEngine;
using Services.Analytics;
using Services.Ads.UnityAds;
using Services.IAP;
using Features.Shed;

internal class MainController : BaseController
{
    private readonly Transform _placeForUi;
    private readonly ProfilePlayer _profilePlayer;

    private MainMenuController _mainMenuController;
    private SettingsMenuController _settingsMenuController;
    private ShedController _shedController;
    private GameController _gameController;

    private AnalyticsManager _analytics;
    private UnityAdsService _adsService;
    private IAPService _iapService;

    public MainController(Transform placeForUi, ProfilePlayer profilePlayer, AnalyticsManager analytics, UnityAdsService adsService, IAPService iapService)
    {
        _placeForUi = placeForUi;
        _profilePlayer = profilePlayer;
        _analytics = analytics;
        _adsService = adsService;
        _iapService = iapService;

        profilePlayer.CurrentState.SubscribeOnChange(OnChangeGameState);
        OnChangeGameState(_profilePlayer.CurrentState.Value);
    }

    protected override void OnDispose()
    {
        _mainMenuController?.Dispose();
        _gameController?.Dispose();
        _settingsMenuController?.Dispose();
        _shedController?.Dispose();
        _profilePlayer.CurrentState.UnSubscribeOnChange(OnChangeGameState);
    }


    private void OnChangeGameState(GameState state)
    {
        switch (state)
        {
            case GameState.Start:
                _gameController?.Dispose();
                _settingsMenuController?.Dispose();
                _mainMenuController = new MainMenuController(_placeForUi, _profilePlayer, _adsService, _iapService);
                _shedController?.Dispose();
                break;
            case GameState.Game:
                _mainMenuController?.Dispose();
                _settingsMenuController?.Dispose();
                _gameController = new GameController(_placeForUi, _profilePlayer, _analytics);
                _shedController?.Dispose();
                break;
            case GameState.Settings:
                _mainMenuController?.Dispose();
                _gameController?.Dispose();
                _settingsMenuController = new SettingsMenuController(_placeForUi, _profilePlayer);
                _shedController?.Dispose();
                break;
            case GameState.Shed:
                _shedController = new ShedController(_placeForUi, _profilePlayer);
                _mainMenuController?.Dispose();
                _settingsMenuController?.Dispose();
                _gameController?.Dispose();
                break;
            default:
                _mainMenuController?.Dispose();
                _gameController?.Dispose();
                _settingsMenuController?.Dispose();
                _shedController?.Dispose();
                break;
        }
    }
}
