using Oxide.Core;
using System.Collections.Generic;

namespace Oxide.Plugins
{
    [Info("Auto Turret Ammo", "nuGGGz", "0.1.0")]
    [Description("Allows players to use infinite ammo on auto turrets")]
    class AutoTurretAmmo : CovalencePlugin
    {
        private void Init()
        {
            permission.RegisterPermission("autoturretammo.use", this);
        }

        #region Config
        private VersionNumber Version { get; set; }
        private ConfigData Configuration;

        // a config class
        class ConfigData
        {
            public VersionNumber Version { get; set; }
        }

        // a get base config method
        private ConfigData GetBaseConfig()
        {
            return new ConfigData
            {
                Version = new VersionNumber(1, 0, 0),
            };
        }
        protected override void LoadConfig()
        {
            base.LoadConfig();
            Configuration = Config.ReadObject<ConfigData>();

            if (Configuration.Version < Version)
                UpdateConfigValues();

            Config.WriteObject(Configuration, true);
        }
        protected override void LoadDefaultConfig() => Configuration = GetBaseConfig();
        protected override void SaveConfig() => Config.WriteObject(Configuration, true);
        private void UpdateConfigValues()
        {
            PrintWarning("Config update detected! Updating config values...");

            if (Configuration != null && Configuration.Version < new VersionNumber(1, 0, 0))
                Configuration = GetBaseConfig();

            if (Configuration != null)
                Configuration.Version = Version;

            PrintWarning("Config update completed!");
        }

        #endregion

        object OnTurretToggle(AutoTurret turret)
        {
            Puts("Turret toggled");

            if (!turret.IsOnline())
            {
                if (permission.UserHasPermission(turret.OwnerID.ToString(), "autoturretammo.use"))
                {
                    TurretLoader(turret);
                }
            }

            return null;
        }
        private object OnTurretTarget(AutoTurret turret, BasePlayer player)
        {
            if (player == null || turret.OwnerID == 0)
            {
                return null;
            }
            if (permission.UserHasPermission(turret.OwnerID.ToString(), "autoturretammo.use"))
            {
                TurretLoader(turret);
            }

            return null;
        }
        
        // TurretLoader will reload the turret,
        // then fill refill any existing ammo slots,
        // then fill any remaining slots with ammo
        private void TurretLoader(AutoTurret turret)
        {
            turret.Reload();
            turret.SendNetworkUpdateImmediate(false);

            var weapon = turret.GetAttachedWeapon();
            var ammoType = weapon.primaryMagazine.ammoType;
            var totalSlots = turret.inventory.capacity;
            var ammoId = ammoType.itemid;
            var itemDef = ItemManager.FindItemDefinition(ammoId);
            var stackLimit = itemDef.stackable;
            var contents = turret.inventory.itemList;

            foreach (var item in contents)
            {
                var count = item.amount;
                var type = item.info;
                if (count < stackLimit)
                {
                    var difference = stackLimit - count;
                    turret.inventory.AddItem(type, difference);
                }
            }

            var remainingSlots = totalSlots - contents.Count;

            for (var i = 0; i < remainingSlots; i++)
            {
                turret.inventory.AddItem(ammoType, stackLimit);
            }

            turret.Reload();
            turret.SendNetworkUpdateImmediate(false);
        }
    }
}
