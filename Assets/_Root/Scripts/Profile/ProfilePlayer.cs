using Tool;
using Game.Car;
using Features.Inventory;
using Features.AbilitySystem.Abilities;

namespace Profile
{
    internal class ProfilePlayer
    {
        public readonly SubscriptionProperty<GameState> CurrentState;
        public readonly CarModel CurrentCar;
        public readonly InventoryModel Inventory;


        public ProfilePlayer(EntryPointConfig entryPointConfig) : this(entryPointConfig.SpeedCar, entryPointConfig.JumpHeight)
        {
            CurrentState.Value = entryPointConfig.InitialState;
        }

        public ProfilePlayer(float speedCar, float jumpHeight)
        {
            CurrentState = new SubscriptionProperty<GameState>();
            CurrentCar = new CarModel(speedCar, jumpHeight);
            Inventory = new InventoryModel();
        }
    }
}
