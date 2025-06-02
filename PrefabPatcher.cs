using Game.Prefabs;
using Unity.Entities;

namespace OutsideTrafficAdjuster
{
    // Adapted from Infixo/CS2-RealPop
    internal class PrefabPatcher
    {
        private EntityManager m_EntityManager;
        private PrefabSystem m_PrefabSystem;

        internal PrefabPatcher()
        {
            m_PrefabSystem = World.DefaultGameObjectInjectionWorld.GetOrCreateSystemManaged<PrefabSystem>();
            m_EntityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
        }

        internal bool TryGetPrefab(string prefabType, string prefabName, out PrefabBase prefabBase, out Entity entity)
        {
            prefabBase = null;
            entity = default;
            PrefabID prefabID = new PrefabID(prefabType, prefabName);
            return m_PrefabSystem.TryGetPrefab(prefabID, out prefabBase) && m_PrefabSystem.TryGetEntity(prefabBase, out entity);
        }

        internal void PatchAllSpawnRates(float roadMultiplier, float trainMultiplier, float shipMultiplier, float planeMultiplier)
        {
            PatchRoadSpawnRates(roadMultiplier);
            PatchTrainSpawnRates(trainMultiplier);
            PatchShipSpawnRates(shipMultiplier);
            PatchPlaneSpawnRates(planeMultiplier);
        }

        internal void PatchRoadSpawnRates(float multiplier)
        {
            if (TryGetPrefab(nameof(MarkerObjectPrefab), "Road Outside Connection - Oneway", out PrefabBase prefabBase, out Entity entity) && m_PrefabSystem.TryGetComponentData<TrafficSpawnerData>(prefabBase, out TrafficSpawnerData comp))
            {
                comp.m_SpawnRate = 0.3f * multiplier;
                m_PrefabSystem.AddComponentData(prefabBase, comp);
            }
            if (TryGetPrefab(nameof(MarkerObjectPrefab), "Road Outside Connection - Twoway", out prefabBase, out entity) && m_PrefabSystem.TryGetComponentData<TrafficSpawnerData>(prefabBase, out comp))
            {
                comp.m_SpawnRate = 0.3f * multiplier;
                m_PrefabSystem.AddComponentData(prefabBase, comp);
            }
        }

        internal void PatchTrainSpawnRates(float multipliler)
        {
            if (TryGetPrefab(nameof(MarkerObjectPrefab), "Train Outside Connection - Oneway", out PrefabBase prefabBase, out Entity entity) && m_PrefabSystem.TryGetComponentData<TrafficSpawnerData>(prefabBase, out TrafficSpawnerData comp))
            {
                comp.m_SpawnRate = 0.003f * multipliler;
                m_PrefabSystem.AddComponentData(prefabBase, comp);
            }
            if (TryGetPrefab(nameof(MarkerObjectPrefab), "Train Outside Connection - Twoway", out prefabBase, out entity) && m_PrefabSystem.TryGetComponentData<TrafficSpawnerData>(prefabBase, out comp))
            {
                comp.m_SpawnRate = 0.003f * multipliler;
                m_PrefabSystem.AddComponentData(prefabBase, comp);
            }
        }

        internal void PatchShipSpawnRates(float multiplier)
        {
            if (TryGetPrefab(nameof(MarkerObjectPrefab), "Train Outside Connection - Oneway", out PrefabBase prefabBase, out Entity entity) && m_PrefabSystem.TryGetComponentData<TrafficSpawnerData>(prefabBase, out TrafficSpawnerData comp))
            {
                comp.m_SpawnRate = 0.002f * multiplier;
                m_PrefabSystem.AddComponentData(prefabBase, comp);
            }
        }

        internal void PatchPlaneSpawnRates(float multiplier)
        {
            if (TryGetPrefab(nameof(MarkerObjectPrefab), "Airplane Outside Connection", out PrefabBase prefabBase, out Entity entity) && m_PrefabSystem.TryGetComponentData<TrafficSpawnerData>(prefabBase, out TrafficSpawnerData comp))
            {
                comp.m_SpawnRate = 0.005f * multiplier;
                m_PrefabSystem.AddComponentData(prefabBase, comp);
            }
        }
    }
}