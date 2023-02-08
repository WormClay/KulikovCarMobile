using Profile;
using UnityEngine;
using Services.IAP;
using Services.Analytics;
using Services.Ads.UnityAds;
using Tool;
using Features.AbilitySystem.Abilities;
using EntryPointSystem;

internal class EntryPoint : MonoBehaviour
{
    [SerializeField] private Transform _placeForUi;
    [SerializeField] private IAPService _iapService;
    [SerializeField] private UnityAdsService _adsService;
    [SerializeField] private AnalyticsManager _analytics;

    private MainController _mainController;


    private void Start()
    {
        var entryPointConfig = new EntryPointContext().LoadConfigs();
        var profilePlayer = new ProfilePlayer(entryPointConfig);
        _mainController = new MainController(_placeForUi, profilePlayer, _analytics, _adsService, _iapService);

        _analytics.SendMainMenuOpened();

        if (_adsService.IsInitialized) OnAdsInitialized();
        else _adsService.Initialized.AddListener(OnAdsInitialized);
    }

    private void OnDestroy()
    {
        _adsService.Initialized.RemoveListener(OnAdsInitialized);
        _mainController.Dispose();
    }

    private void OnAdsInitialized() => _adsService.InterstitialPlayer.Play();
}
