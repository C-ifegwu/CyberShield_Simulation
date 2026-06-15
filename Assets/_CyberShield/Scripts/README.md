# CyberShield Simulator

**Developer:** Victor Ifegwu  
**Program:** ALU Software Engineering

🎮 **PLAY THE SIMULATION LIVE:** [CyberShield WebGL Build](https://play.unity.com/en/games/559b7670-6eda-47ea-9f69-c0493d1e6763/cybershieldwebbuild)

## 📖 Overview

**CyberShield Simulator** is a 3D psychological and educational serious game built in Unity. It is designed to teach players—specifically teenagers—how to identify, manage, and escape real-world digital threats like sextortion and cyberbullying.

Rather than simply clicking through a quiz, players are dropped into a first-person environment. They must interact with digital interfaces, experience the psychological stress of being targeted, and learn that the only way to escape an online trap is through physical, real-world action (seeking help from trusted authorities).

## ✨ Core Features

- **Dual-Scenario Open World:** Features two distinct threat scenarios located in different areas of the city (Sextortion in the bedroom, Cyberbullying in the cafe).
- **Node-Based Narrative Engine:** A custom-built, interactive smartphone UI that types out messages dynamically and branches based on player choices.
- **Psychological Cinematics:** When a trap is sprung, the game strips away the player's comfort. Soothing background music fades into a deafening heartbeat, and the bright city lighting plunges into a claustrophobic red eclipse.
- **The "Burning Fuse" Timer:** Utilizing a dynamic mathematical Line Renderer, a glowing red circle wraps around the player and shrinks inwards, acting as a terrifying 60-second countdown timer.
- **Physical Safe Havens:** Players cannot "block" or "ignore" their way out of the late-stage traps. They must physically navigate the 3D space to find invisible trigger zones at the Police Station or Counselor's Office to survive.

## 🎮 Controls

This project utilizes the **New Unity Input System**.

- **W, A, S, D** - Move player
- **Mouse** - Look around / Aim Raycast
- **Left Mouse Button** - Interact with objects (Smartphones, UI Buttons)

## 🏗️ Technical Architecture

- **Language:** C#
- **Engine:** Unity 6 (Universal Render Pipeline)
- **Key Scripts:**
  - `ScenarioManager.cs` - Handles the node arrays and UI typewriter effects.
  - `EnvironmentManager.cs` - Manages lighting shifts and audio crossfades via Coroutines.
  - `PanicTimer.cs` - Handles the trigonometric math for the shrinking Line Renderer.
  - `RaycastInteraction.cs` - Manages center-screen physics raycasting to detect interactables.

## 📜 Credits & Asset Attribution

This project was brought to life using the following royalty-free assets and audio tracks:

**Audio:**

- **Background Music:** [Luminous Tranquility](https://pixabay.com/music/meditationspiritual-luminous-tranquility-531191/) by Pixabay
- **SFX:** [People Heartbeat](https://pixabay.com/sound-effects/people-hearbeat-71701/) by Pixabay

**3D Models & Environments:**

- **City Environment:** [SimplePoly City - Low Poly Assets](https://assetstore.unity.com/packages/3d/environments/simplepoly-city-low-poly-assets-58899)
- **Bedroom Interior:** [Furnished Cabin](https://assetstore.unity.com/packages/3d/environments/urban/furnished-cabin-71426)
- **Interactable Prop:** [Free Phone](https://assetstore.unity.com/packages/3d/props/free-phone-181455)

---

_Built as a functional prototype for educational and simulation purposes._
