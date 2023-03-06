using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Features.Fight
{
    internal class BackView : MonoBehaviour
    {
        [SerializeField] private Button _backButton;

        public void Init(UnityAction back) =>
            _backButton.onClick.AddListener(back);

        private void OnDestroy() =>
            _backButton.onClick.RemoveAllListeners();
    }
}

