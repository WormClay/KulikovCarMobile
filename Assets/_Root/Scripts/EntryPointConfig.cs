using UnityEngine;
using Profile;

namespace Features.AbilitySystem.Abilities
{
    [CreateAssetMenu(fileName = nameof(EntryPointConfig), menuName = "Configs/" + nameof(EntryPointConfig))]
    internal class EntryPointConfig : ScriptableObject
    {

        [field: SerializeField] public GameState InitialState { get; private set; }
        [field: SerializeField] public float SpeedCar { get; private set; }
        [field: SerializeField] public float JumpHeight { get; private set; }
    }
}
