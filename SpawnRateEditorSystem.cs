using Colossal.Serialization.Entities;
using Game;
using Game.Prefabs;
using Unity.Entities;

namespace OutsideTrafficAdjuster
{
    public partial class SpawnRateEditorSystem : GameSystemBase
    {
        private PrefabSystem m_PrefabSystem;

        private float roadMultiplier;
        private float trainMultiplier;
        private float shipMultiplier;
        private float planeMultiplier;

        protected override void OnCreate()
        {
            base.OnCreate();
            m_PrefabSystem = World.DefaultGameObjectInjectionWorld.GetOrCreateSystemManaged<PrefabSystem>();
            ResetMultipliers();
            Mod.INSTANCE.m_Setting.onSettingsApplied += settings =>
            {
                if (settings.GetType() == typeof(Setting))
                {
                    this.UpdateAndApplyMultipliers((Setting)settings);
                }
            };
            this.Enabled = false;
        }

        protected override void OnUpdate()
        {
            this.Enabled = false;
        }

        protected override void OnGameLoadingComplete(Purpose purpose, GameMode mode)
        {
            base.OnGameLoadingComplete(purpose, mode);
            if (mode == GameMode.Game)
            {
                InitializeMultipliers(Mod.INSTANCE.m_Setting);
                ApplyAllSpawnRates();
            }
            else
            {
                ResetMultipliers();
                ApplyAllSpawnRates();
            }
        }

        private void InitializeMultipliers(Setting settings)
        {
            roadMultiplier = settings.RoadMultiplier;
            trainMultiplier = settings.TrainMultiplier;
            shipMultiplier = settings.ShipMultiplier;
            planeMultiplier = settings.PlaneMultiplier;
        }

        private void ResetMultipliers()
        {
            roadMultiplier = 1.0f;
            trainMultiplier = 1.0f;
            shipMultiplier = 1.0f;
            planeMultiplier = 1.0f;
        }

        private void UpdateAndApplyMultipliers(Setting settings)
        {
            if (roadMultiplier != settings.RoadMultiplier)
            {
                roadMultiplier = settings.RoadMultiplier;
                ApplyRoadSpawnRates();
            }
            if (trainMultiplier != settings.TrainMultiplier)
            {
                trainMultiplier = settings.TrainMultiplier;
                ApplyTrainSpawnRates();
            }
            if (shipMultiplier != settings.ShipMultiplier)
            {
                shipMultiplier = settings.ShipMultiplier;
                ApplyShipSpawnRates();
            }
            if (planeMultiplier != settings.PlaneMultiplier)
            {
                planeMultiplier = settings.PlaneMultiplier;
                ApplyPlaneSpawnRates();
            }
        }

        private void ApplyAllSpawnRates()
        {
            ApplyRoadSpawnRates();
            ApplyTrainSpawnRates();
            ApplyShipSpawnRates();
            ApplyPlaneSpawnRates();
        }

        private bool TryGetPrefab(string prefabType, string prefabName, out PrefabBase prefabBase, out Entity entity)
        {
            prefabBase = null;
            entity = default;
            PrefabID prefabID = new PrefabID(prefabType, prefabName);
            return m_PrefabSystem.TryGetPrefab(prefabID, out prefabBase) && m_PrefabSystem.TryGetEntity(prefabBase, out entity);
        }

        private void ApplyRoadSpawnRates()
        {
            if (TryGetPrefab(nameof(MarkerObjectPrefab), "Road Outside Connection - Oneway", out PrefabBase prefabBase, out Entity entity) && m_PrefabSystem.TryGetComponentData<TrafficSpawnerData>(prefabBase, out TrafficSpawnerData comp))
            {
                comp.m_SpawnRate = 0.3f * roadMultiplier;
                m_PrefabSystem.AddComponentData(prefabBase, comp);
            }
            if (TryGetPrefab(nameof(MarkerObjectPrefab), "Road Outside Connection - Twoway", out prefabBase, out entity) && m_PrefabSystem.TryGetComponentData<TrafficSpawnerData>(prefabBase, out comp))
            {
                comp.m_SpawnRate = 0.3f * roadMultiplier;
                m_PrefabSystem.AddComponentData(prefabBase, comp);
            }
        }

        private void ApplyTrainSpawnRates()
        {
            if (TryGetPrefab(nameof(MarkerObjectPrefab), "Train Outside Connection - Oneway", out PrefabBase prefabBase, out Entity entity) && m_PrefabSystem.TryGetComponentData<TrafficSpawnerData>(prefabBase, out TrafficSpawnerData comp))
            {
                comp.m_SpawnRate = 0.003f * trainMultiplier;
                m_PrefabSystem.AddComponentData(prefabBase, comp);
            }
            if (TryGetPrefab(nameof(MarkerObjectPrefab), "Train Outside Connection - Twoway", out prefabBase, out entity) && m_PrefabSystem.TryGetComponentData<TrafficSpawnerData>(prefabBase, out comp))
            {
                comp.m_SpawnRate = 0.003f * trainMultiplier;
                m_PrefabSystem.AddComponentData(prefabBase, comp);
            }
        }

        private void ApplyShipSpawnRates()
        {
            if (TryGetPrefab(nameof(MarkerObjectPrefab), "Ship Outside Connection - Twoway", out PrefabBase prefabBase, out Entity entity) && m_PrefabSystem.TryGetComponentData<TrafficSpawnerData>(prefabBase, out TrafficSpawnerData comp))
            {
                comp.m_SpawnRate = 0.002f * shipMultiplier;
                m_PrefabSystem.AddComponentData(prefabBase, comp);
            }
        }

        private void ApplyPlaneSpawnRates()
        {
            if (TryGetPrefab(nameof(MarkerObjectPrefab), "Airplane Outside Connection", out PrefabBase prefabBase, out Entity entity) && m_PrefabSystem.TryGetComponentData<TrafficSpawnerData>(prefabBase, out TrafficSpawnerData comp))
            {
                comp.m_SpawnRate = 0.005f * planeMultiplier;
                m_PrefabSystem.AddComponentData(prefabBase, comp);
            }
        }
    }
}