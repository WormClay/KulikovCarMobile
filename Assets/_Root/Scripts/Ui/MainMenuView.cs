using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Ui
{
    public class MainMenuView : MonoBehaviour
    {
        [SerializeField] private Button _buttonStart;
        [SerializeField] private Button _buttonSettings;
        [SerializeField] private Button _buttonRewarded;
        [SerializeField] private Button _buttonBuy;
        [SerializeField] private Button _buttonShed;


        public void Init(UnityAction startGame, UnityAction showSettings, UnityAction getRewarded, UnityAction buyGoods, UnityAction openShed)
        {
            _buttonStart.onClick.AddListener(startGame);
            _buttonSettings.onClick.AddListener(showSettings);
            _buttonRewarded.onClick.AddListener(getRewarded);
            _buttonBuy.onClick.AddListener(buyGoods);
            _buttonShed.onClick.AddListener(openShed);
        }

        public void OnDestroy()
        {
            _buttonStart.onClick.RemoveAllListeners();
            _buttonSettings.onClick.RemoveAllListeners();
            _buttonRewarded.onClick.RemoveAllListeners();
            _buttonBuy.onClick.RemoveAllListeners();
            _buttonShed.onClick.RemoveAllListeners();
        }
    }
}
