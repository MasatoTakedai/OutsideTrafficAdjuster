// testing if a more frequent update interval can bring in more traffic - didn't seem to work

using Game;
using Game.Simulation;

namespace OutsideTrafficAdjuster
{
    public partial class NewTrafficSpawnerAISystem : TrafficSpawnerAISystem
    {
        protected override void OnCreate()
        {
            base.OnCreate();
            base.World.GetOrCreateSystemManaged<TrafficSpawnerAISystem>().Enabled = false;
        }

        public override int GetUpdateInterval(SystemUpdatePhase phase)
        {
            return 4;
        }

        public override int GetUpdateOffset(SystemUpdatePhase phase)
        {
            return 2;
        }
    }
}
