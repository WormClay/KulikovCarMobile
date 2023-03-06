using Ui;
using Game;
using Profile;
using UnityEngine;
using Services.Analytics;
using Services.Ads.UnityAds;
using Services.IAP;
using Features.Shed;
using System;
using Features.ShedSystem;
using Features.Fight;

internal class MainController : BaseController
{
    private readonly Transform _placeForUi;
    private readonly ProfilePlayer _profilePlayer;

    private MainMenuController _mainMenuController;
    private SettingsMenuController _settingsMenuController;
    private ShedContext _shedContext;
    private GameController _gameController;
//    private RewardController _rewardController;
    private FightController _fightController;
    private StartFightController _startFightController;
    private BackController _backController;

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
        DisposeControllers();
        _profilePlayer.CurrentState.UnSubscribeOnChange(OnChangeGameState);
    }

    private void OnChangeGameState(GameState state)
    {
        DisposeControllers();
        switch (state)
        {
            case GameState.Start:
                _mainMenuController = new MainMenuController(_placeForUi, _profilePlayer, _adsService, _iapService);
                break;
            case GameState.Game:
                _gameController = new GameController(_placeForUi, _profilePlayer, _analytics);
                _startFightController = new StartFightController(_placeForUi, _profilePlayer);
                _backController = new BackController(_placeForUi, _profilePlayer);
                break;
            case GameState.Settings:
                _settingsMenuController = new SettingsMenuController(_placeForUi, _profilePlayer);
                break;
            case GameState.Shed:
                _shedContext = new ShedContext(_placeForUi, _profilePlayer);
                break;
//            case GameState.DailyReward:
//                _rewardController = new RewardController(_placeForUi, _profilePlayer);
//                break;
            case GameState.Fight:
                _fightController = new FightController(_placeForUi, _profilePlayer);
                break;
        }
    }

    private void DisposeControllers()
    {
        _mainMenuController?.Dispose();
        _gameController?.Dispose();
        _settingsMenuController?.Dispose();
        _shedContext?.Dispose();
        _startFightController?.Dispose();
        _fightController?.Dispose();
        _backController?.Dispose();
    }
}
