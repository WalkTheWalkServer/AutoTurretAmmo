# AutoTurretAmmo
## Overview
This Rust plugin enhances the functionality of autoturrets in the game by providing them with infinite ammo. Turrets will automatically reload to full ammo when toggled on and when targeting a player. To ensure controlled usage, players must have the necessary permissions to enable this plugin for their turrets.

## Features
- **Infinite Ammo:** Turrets will have unlimited ammunition, making them more powerful and eliminating the need for manual reloading.
- **Automatic Reload:** When toggling on or when targeting a player, turrets will be automatically reloaded to full ammo capacity.
- **Permission System:** Players need specific permission to enable the plugin for their turrets, ensuring controlled access and usage.

## Installation
- Download the latest release from the Releases page.
- Extract the contents of the ZIP file to your Rust server's oxide/plugins directory.
- No server reload is necessary, Oxide should load the plugin and it's ready to go.

## Permissions
- `autoturretammo.use` Required for players to use the plugin for their autoturrets.

## Compatibility
This plugin is compatible with the following:
- Stack Size Controller

## Usage
1. Grant the necessary permission to players/groups using the oxide.grant command.
   - `oxide.grant user <player_name_or_id> autoturretammo.use`
   - `oxide.grant group <player_group_name_or_id> autoturretammo.use`
2. Toggle on your autoturret, and it will be automatically reloaded with infinite ammo.

## Contribution
Contributions are welcome! If you find any issues or have suggestions for improvement, please [open an issue](https://github.com/WalkTheWalkServer/AutoTurretAmmo/issues) or [submit a pull request](https://github.com/WalkTheWalkServer/AutoTurretAmmo/pulls).

## License
This plugin is licensed under the [GNU General Public License v3.0](https://www.gnu.org/licenses/gpl-3.0.en.html).
