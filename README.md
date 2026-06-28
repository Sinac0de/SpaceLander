# Space Lander: Physics-Based Arcade Simulation 🚀

[![Unity Version](https://img.shields.io/badge/Unity-6000.5.1f1-blue.svg)](https://unity.com/)
[![Language](https://img.shields.io/badge/Language-C%23-green.svg)](https://docs.microsoft.com/en-us/dotnet/csharp/)
[![Graphics](https://img.shields.io/badge/Graphics-URP_2D-lightgrey.svg)]()

A polished, physics-driven 2D space simulation game built in Unity 6. Heavily inspired by CodeMonkey's architectural concepts, this project focuses on precision landing physics, state-driven gameplay mechanics, resource management, and clean, decoupled C# software patterns.

> 📦 **Repository Status:** Fully functional core gameplay loop featuring multi-level progression, persistent save states, and data-driven level architecture.

---

## 📸 In-Engine Previews & Interface


| Main Menu | Data-Driven Level Selection |
| --- | --- |
| ![Main Menu](https://i.ibb.co/Z1h7hs15/Space-Lander-Main-Menu.png) | ![Level Selection](https://i.ibb.co/ZRxNbWjN/Space-Lander-Level-Selection.png) |
| **Precision Landing & UI HUD** | **Structural Environment Layout** |
| ![Gameplay 2](https://i.ibb.co/MDQCSmxn/Space-Lander-Gameplay2.png) | ![Level Design Map](https://i.ibb.co/60xhr0dr/Space-Lander-Gameplay.png) |

---

## 🧠 Software Engineering & Features

### 🛸 Lander Kinematics & Gameplay Systems
- **Precision Flight Controls:** Responsive, physics-based spacecraft handling optimized via Unity's **New Input System** and tracked with **Cinemachine 2D** smart cameras[cite: 1].
- **Resource & Progression Inventories:** Integrated dynamic mechanics for global item pickups (`FuelPickup`, `CoinPickup`) and an encrypted color-coded key security gateway (`DoubleDoor` gating system)[cite: 1].
- **Physics Landing Pads:** Multi-tier landing pad scoring logic with individual star calculations based on landing velocities, dynamic score multipliers ($x3, x5$), and clock timers[cite: 1].

### 📐 Structural Game Architecture
- **Data-Driven Workflows:** Extensive use of `ScriptableObjects` (`LevelConfigSO`, `KeyDataSO`, `LevelDatabaseSO`) to completely isolate level parameters and logic data from game behavior[cite: 1].
- **Robust Management Layer:** Decoupled structural managers handling independent game states:
  - `GameManager`: Controls core global game state machines[cite: 1].
  - `SaveManager`: Manages secure dynamic user profiles and persistent progression tracking[cite: 1].
  - `SceneLoader` & `SoundManager`: Handles async scene transitions and sound effects routing safely[cite: 1].

### 📺 Modular UI/UX Setup
- Fully decoupled, event-driven user interfaces (`StatsUI`, `InventoryUI`, `LandedUI`, `PauseUI`) seamlessly bridging core script states into visual canvas layouts[cite: 1].

---

## 🛠️ Technologies Used

- **Game Engine:** Unity 6000.5.1f1 Ecosystem[cite: 1]
- **Render Pipeline:** Universal Render Pipeline (URP 2D Suite)[cite: 1]
- **Frameworks:** Unity New Input System, Cinemachine Camera Modules[cite: 1]
- **UI Logic:** Component-oriented UGUI Canvas & Layout groups[cite: 1]

---

## 📂 Project Structure

```text
Assets/Scripts/
├── Lander (Player)/    # Kinematics controller, dynamic visual node states, and sound events[cite: 1]
├── Managers/           # Singletons regulating game states, persistent saving, and scene loading[cite: 1]
├── PickUps/            # Modular collectible classes (Fuel, Coins, Dynamic Keys)[cite: 1]
├── ScriptableObjects/  # Data blueprints driving dynamic level assets and key configuration data[cite: 1]
└── UI/                 # Presentation layers separated entirely from the player's logical state[cite: 1]
