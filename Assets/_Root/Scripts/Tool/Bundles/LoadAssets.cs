using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Tool.Bundles.Examples
{
    internal class LoadAssets : LoadAssetsBase
    {
        [Header("Asset Bundles")]
        [SerializeField] private Button _loadAssetsButton;

        [Header("Addressables")]
        [SerializeField] private Button _loadBackground;
        [SerializeField] private Button _unloadBackground;
        [SerializeField] private AssetReference _backPrefab;
        [SerializeField] private RectTransform _backgroundContainer;

        [SerializeField] private Image _background;


        private AsyncOperationHandle<GameObject> _addressablePrefab;
        private AsyncOperationHandle<GameObject> _nullObject;

        private void Start()
        {
            _loadAssetsButton.onClick.AddListener(LoadBackImage);
            _loadBackground.onClick.AddListener(LoadBackground);
            _unloadBackground.onClick.AddListener(UnLoadBackground);
        }

        private void OnDestroy()
        {
            _loadAssetsButton.onClick.RemoveAllListeners();
            _loadBackground.onClick.RemoveAllListeners();
            _unloadBackground.onClick.RemoveAllListeners();

            UnLoadBackground();
        }


        private void LoadBackImage()
        {
            _loadAssetsButton.interactable = false;
            StartCoroutine(DownloadAndSetAssetBundles());
        }

        private void LoadBackground()
        {
            if (_addressablePrefab.Equals(_nullObject))
                _addressablePrefab = Addressables.InstantiateAsync(_backPrefab, _backgroundContainer);
        }

        private void UnLoadBackground()
        {
            if (!_addressablePrefab.Equals(_nullObject))
            {
                Addressables.ReleaseInstance(_addressablePrefab);
                _addressablePrefab = _nullObject;
            }
        }
    }
}
