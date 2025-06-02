using Colossal.IO.AssetDatabase;
using Colossal.Logging;
using Game;
using Game.Modding;
using Game.SceneFlow;
using Game.Simulation;

namespace OutsideTrafficAdjuster
{
    public class Mod : IMod
    {
        public static ILog log = LogManager.GetLogger($"{nameof(OutsideTrafficAdjuster)}.{nameof(Mod)}").SetShowsErrorsInUI(false);
        private Setting m_Setting;

        public void OnLoad(UpdateSystem updateSystem)
        {
            log.Info(nameof(OnLoad));

            if (GameManager.instance.modManager.TryGetExecutableAsset(this, out var asset))
                log.Info($"Current mod asset at {asset.path}");

            m_Setting = new Setting(this);
            m_Setting.RegisterInOptionsUI();
            GameManager.instance.localizationManager.AddSource("en-US", new LocaleEN(m_Setting));
            AssetDatabase.global.LoadSettings(nameof(OutsideTrafficAdjuster), m_Setting, new Setting(this));

            // patch prefabs
            PrefabPatcher patcher = new PrefabPatcher();
            patcher.PatchAllSpawnRates(m_Setting.RoadMultiplier, m_Setting.TrainMultiplier, m_Setting.ShipMultiplier, m_Setting.PlaneMultiplier);

            // set up new overridden system
            //updateSystem.UpdateAt<NewTrafficSpawnerAISystem>(SystemUpdatePhase.GameSimulation);
            //updateSystem.UpdateAt<NewTrafficSpawnerAISystem>(SystemUpdatePhase.LoadSimulation);
        }

        public void OnDispose()
        {
            log.Info(nameof(OnDispose));
            if (m_Setting != null)
            {
                m_Setting.UnregisterInOptionsUI();
                m_Setting = null;
            }
        }
    }
}
