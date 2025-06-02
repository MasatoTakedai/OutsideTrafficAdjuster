using Colossal;
using Colossal.IO.AssetDatabase;
using Game.Modding;
using Game.Settings;
using System;
using System.Collections.Generic;

namespace OutsideTrafficAdjuster
{
    [FileLocation(nameof(OutsideTrafficAdjuster))]
    public class Setting : ModSetting
    {
        public Setting(IMod mod) : base(mod)
        {
            SetDefaults();
        }

        [SettingsUISlider(min = 0, max = 2, step = 0.1f, unit = "floatSingleFraction")]
        [SettingsUISetter(typeof(Setting), nameof(ApplyRoadSettings))]
        public float RoadMultiplier { get; set; }
        public void ApplyRoadSettings(float value)
        {
            new PrefabPatcher().PatchRoadSpawnRates(value);
            RoadMultiplier = value;
        }

        [SettingsUISlider(min = 0, max = 2, step = 0.1f, unit = "floatSingleFraction")]
        [SettingsUISetter(typeof(Setting), nameof(ApplyTrainSettings))]
        public float TrainMultiplier { get; set; }
        public void ApplyTrainSettings(float value)
        {
            new PrefabPatcher().PatchTrainSpawnRates(value);
            TrainMultiplier = value;
        }

        [SettingsUISlider(min = 0, max = 2, step = 0.1f, unit = "floatSingleFraction")]
        [SettingsUISetter(typeof(Setting), nameof(ApplyShipSettings))]
        public float ShipMultiplier { get; set; }
        public void ApplyShipSettings(float value)
        {
            new PrefabPatcher().PatchShipSpawnRates(value);
            ShipMultiplier = value;
        }

        [SettingsUISlider(min = 0, max = 2, step = 0.1f, unit = "floatSingleFraction")]
        [SettingsUISetter(typeof(Setting), nameof(ApplyPlaneSettings))]
        public float PlaneMultiplier { get; set; }
        public void ApplyPlaneSettings(float value)
        {
            new PrefabPatcher().PatchPlaneSpawnRates(value);
            PlaneMultiplier = value;
        }

        public sealed override void SetDefaults()
        {
            RoadMultiplier = 1;
            TrainMultiplier = 1;
            ShipMultiplier = 1;
            PlaneMultiplier = 1;
        }
    }

    public class LocaleEN : IDictionarySource
    {
        private readonly Setting m_Setting;
        public LocaleEN(Setting setting)
        {
            m_Setting = setting;
        }
        public IEnumerable<KeyValuePair<string, string>> ReadEntries(IList<IDictionaryEntryError> errors, Dictionary<string, int> indexCounts)
        {
            return new Dictionary<string, string>
            {
                { m_Setting.GetSettingsLocaleID(), "Outside Traffic Adjuster" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.RoadMultiplier)), "Vehicle Spawnrate Multiplier" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.RoadMultiplier)),
                    "Multiplier for the spawnrate of outside connection to outside connection road traffic. Set to 0 to disable."
                },
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.TrainMultiplier)), "Train Spawnrate Multiplier" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.TrainMultiplier)),
                    "Multiplier for the spawnrate of outside connection to outside connection train traffic. Set to 0 to disable"
                },
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ShipMultiplier)), "Ship Spawnrate Multiplier" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ShipMultiplier)),
                    "Multiplier for the spawnrate of outside connection to outside connection ship traffic. Set to 0 to disable"
                },
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PlaneMultiplier)), "Airplane Spawnrate Multiplier" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.PlaneMultiplier)),
                    "Multiplier for the spawnrate of outside connection to outside connection plane traffic. Set to 0 to disable"
                },
            };
        }

        public void Unload()
        {

        }
    }
}
