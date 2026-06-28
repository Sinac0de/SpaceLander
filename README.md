# Space Lander (Based on Codemonkey's 2D Crash-Course)

A Unity 2D prototype focused on precision landing, level progression, and satisfying arcade-style feedback. The project explores core gameplay systems around controlled movement, score-based objectives, and reusable level configuration.

> This repository represents an early-stage prototype and is still being expanded. The current build focuses on the core loop of launching, guiding, and landing a craft successfully.

![SpaceLander](https://i.ibb.co/SwmCjCfD/Space-Lander-Gameplay3.png)

## Overview

Space Lander is a compact Unity project built around a simple but engaging challenge: guide a spacecraft to a landing pad, manage momentum, and earn a high score through careful execution. The prototype emphasizes clear mechanics, modular scripts, and a level-driven structure that can be extended with new worlds, hazards, and gameplay systems.

The project currently explores:
- 2D player movement and physics-based flight control
- Landing pad scoring and star-based level evaluation
- Scene and level progression flow
- Pause, retry, and game-state management
- ScriptableObject-driven level configuration

## Features

### Gameplay Systems
- Responsive lander movement with camera-follow behavior
- Landing pad interaction and score multiplier support
- Level completion flow with score and star calculation
- Pause and retry support for repeated attempts
- Timer-based play tracking and progress management

### Architecture & Development
- Modular C# architecture for game flow and player logic
- ScriptableObject-based level data for reusable configuration
- Component-oriented structure for easier iteration and expansion
- Cinemachine and Input System integration for modern Unity workflows

## Project Status

This repository is an active prototype rather than a complete game experience.

Current status:
- Core lander movement and landing loop are implemented
- Level progression and scoring systems are in place
- UI and game-state management are partially integrated
- Additional content, polish, and expanded mechanics are still in progress

## In-Engine Screenshots

![Main Menu](https://i.ibb.co/Z1h7hs15/Space-Lander-Main-Menu.png)
![LevelSelection](https://i.ibb.co/ZRxNbWjN/Space-Lander-Level-Selection.png)
![Gameplay 2](https://i.ibb.co/MDQCSmxn/Space-Lander-Gameplay2.png)
![Map of a level](https://i.ibb.co/60xhr0dr/Space-Lander-Gameplay.png)


## Technologies Used

- Unity 6000.5.1f1
- C#
- Unity Input System
- Cinemachine
- Universal Render Pipeline (URP)
- ScriptableObjects
- Unity UI Toolkit / UGUI-style UI patterns

## Project Structure

- Assets/Scripts/Managers – Game flow, pause state, and progression logic
- Assets/Scripts/Lander (Player) – Player movement and state handling
- Assets/Scripts/PickUps – Collectible objects and related logic
- Assets/Scripts/UI – Interface-related components and feedback systems
- Assets/Scripts/ScriptableObjects – Reusable level and gameplay data definitions

## Future Direction

Planned areas for growth include:
- More varied and challenging levels
- Additional hazards and environmental mechanics
- Better visual feedback and polish
- More robust progression, save data, and replayability
- Expanded content for a fuller arcade-style experience
