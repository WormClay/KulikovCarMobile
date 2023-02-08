using Tool;
using System;
using UnityEngine;
using JetBrains.Annotations;
using Object = UnityEngine.Object;
using Features.Shed;
using Profile;
using Features.Inventory;
using Features.Shed.Upgrade;

namespace Features.ShedSystem
{
    internal class ShedContext : BaseContext
    {
        private readonly ResourcePath _viewPath = new ResourcePath("Prefabs/Shed/ShedView");
        private readonly ResourcePath _dataSourcePath = new ResourcePath("Configs/Shed/UpgradeItemConfigDataSource");

        private readonly InventoryContext _inventoryContext;
        private readonly UpgradeHandlersRepository _upgradeHandlersRepository;

        public ShedContext([NotNull] Transform placeForUi, [NotNull] ProfilePlayer profilePlayer)
        {
            if (placeForUi == null)
                throw new ArgumentNullException(nameof(placeForUi));

            _inventoryContext = CreateInventoryContext(placeForUi, profilePlayer.Inventory);
            _upgradeHandlersRepository = CreateRepository();
            CreateController(placeForUi, profilePlayer);
        }


        private ShedController CreateController(Transform placeForUi, ProfilePlayer profilePlayer)
        {
            ShedView view = LoadView(placeForUi);
            var controller = new ShedController(view, placeForUi, profilePlayer, _upgradeHandlersRepository);
            AddController(controller);

            return controller;
        }

        private ShedView LoadView(Transform placeForUi)
        {
            GameObject prefab = ResourcesLoader.LoadPrefab(_viewPath);
            GameObject objectView = Object.Instantiate(prefab, placeForUi, false);
            AddGameObject(objectView);

            return objectView.GetComponent<ShedView>();
        }

        private InventoryContext CreateInventoryContext(Transform placeForUi, IInventoryModel model)
        {
            var context = new InventoryContext(placeForUi, model);
            AddContext(context);

            return context;
        }

        private UpgradeHandlersRepository CreateRepository()
        {
            UpgradeItemConfig[] upgradeConfigs = ContentDataSourceLoader.LoadUpgradeItemConfigs(_dataSourcePath);
            var repository = new UpgradeHandlersRepository(upgradeConfigs);
            AddRepository(repository);

            return repository;
        }

    }
}