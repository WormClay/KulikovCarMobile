using Tool;
using Features.AbilitySystem.Abilities;

namespace EntryPointSystem
{
    internal class EntryPointContext : BaseContext
    {
        private readonly ResourcePath _dataSourcePath = new ResourcePath("Configs/EntryPointConfig");

        public EntryPointConfig LoadConfigs() =>
       ContentDataSourceLoader.LoadEntryPointConfigs(_dataSourcePath);
    }
}
