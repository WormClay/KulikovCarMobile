using Profile;
using Tool;
using UnityEngine;

namespace Features.Fight
{
    internal class BackController : BaseController
    {
        private readonly ResourcePath _resourcePath = new ResourcePath("Prefabs/Fight/BackView");

        private readonly BackView _view;
        private readonly ProfilePlayer _profilePlayer;


        public BackController(Transform placeForUi, ProfilePlayer profilePlayer)
        {
            _profilePlayer = profilePlayer;
            _view = LoadView(placeForUi);
            _view.Init(Back);
        }


        private BackView LoadView(Transform placeForUi)
        {
            GameObject prefab = ResourcesLoader.LoadPrefab(_resourcePath);
            GameObject objectView = Object.Instantiate(prefab, placeForUi, false);
            AddGameObject(objectView);

            return objectView.GetComponent<BackView>();
        }

        private void Back() =>
            _profilePlayer.CurrentState.Value = GameState.Start;
    }
}
