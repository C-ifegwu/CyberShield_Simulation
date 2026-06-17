# CyberShield Simulator

**Developer:** Victor Ifegwu  
**Program:** ALU / ALX Software Engineering

🎮 **PLAY THE SIMULATION LIVE:** [CyberShield WebGL Build](https://play.unity.com/en/games/559b7670-6eda-47ea-9f69-c0493d1e6763/cybershieldwebbuild)

## 🌍 GCGO Statement & Problem Context

**GCGO:** Digital Safety and Youth Mental Wellbeing.

**The Problem:** The dramatic rise in teenage sextortion and cyberbullying. Young people targeted by digital threats often feel entirely trapped inside their devices, leading them to attempt to handle extortion or mob harassment alone. This isolation often results in severe mental health crises.
**The Solution:** This simulation gamifies the trap of online extortion, educating users on the mechanics of cyber-threats while reinforcing the only actionable solution: stepping away from the screen and seeking real-world help from trusted authorities.

## 📖 Simulation Overview

**CyberShield Simulator** is a 3D first-person serious game designed for teenagers. Rather than a standard quiz, players are dropped into a 3D environment (a bedroom and a cafe). They must interact with digital interfaces (smartphones) and navigate branching text narratives. Once a digital trap is sprung (Sextortion or Cyberbullying), the environment becomes hostile, and a shrinking timer forces the player to physically run to a Safe Haven (Police Station or Counselor's Office) to survive.

## ⚙️ Unity Mechanics Implemented

- **New Unity Input System:** Used exclusively for all player movement (WASD) and UI/Raycast interactions, ensuring cross-platform compatibility without legacy input code.
- **User Interface (UI):** Utilized TextMeshPro panels and dynamic buttons to create a branching, node-based smartphone messaging system.
- **Scripting:** Built modular C# architecture, including a custom `ScenarioManager` to handle array-based data structures for the narrative, and `EnvironmentManager` to handle asynchronous coroutines.
- **Raycasting:** Fired `Physics.Raycast` from the camera center to detect 3D smartphones and trigger the UI storylines via a `PhoneData` component.
- **Collision:** Placed Box Colliders set to `Is Trigger` on Safe Havens. Used `OnTriggerEnter` to detect the player, stop the panic timer, and resolve the game loop.
- **Line Renderer:** Programmed a dynamic, 50-point mathematical circle using `Mathf.Sin` and `Mathf.Cos` that acts as a physical, shrinking "fuse" timer around the player during Panic Mode.

## 🚀 Additional Features (Beyond Module Scope)

To elevate the technical quality of the simulation, I implemented four features outside the standard curriculum:

1. **Audio Crossfading (Coroutines):** Used C# `IEnumerator` and `Mathf.Lerp` to smoothly crossfade peaceful background music into a stressful heartbeat audio source without pausing the game thread.
2. **Animation State Machines:** Mapped boolean parameters (`isOpen`) to Animator transitions, using a `ProximityDoor` script to automatically animate doors opening when the player approaches.
3. **Data Persistence:** Utilized `PlayerPrefs` to save the player's survival score in the City Scene and retrieve it to display dynamic educational messaging in the Results Scene.
4. **Advanced Scene Management:** Created a fully looping application with a Main Menu, City Scene, and End Screen.

## 🛠️ Build Information & Instructions

- **WebGL Deployment:** [Click Here to Play](https://play.unity.com/en/games/559b7670-6eda-47ea-9f69-c0493d1e6763/cybershieldwebbuild)
- **Android Build (APK):** Included in the submission `.zip` file as `CyberShieldApp.apk`.

**How to Run the Project:**

- **Browser:** Click the WebGL link above. Use Mouse to look around, WASD to move, and Left Click to interact with phones and UI buttons.
- **Android:** Transfer the `.apk` file to an Android device. Enable "Install from unknown sources" in your security settings, install the app, and tap the icon to play. Use the touchscreen joystick to move and tap the center of the screen to interact.

## 🧠 Reflection

Building this simulation bridged the gap between theoretical C# programming and interactive user psychology. The most challenging aspect was mastering asynchronous operations (Coroutines) to manipulate the environment's lighting and audio smoothly, and ensuring the Android package identification was properly configured in the Build Settings. By strictly implementing the New Unity Input System and decoupled raycast logic, I learned how to build scalable architecture. Ultimately, this project successfully demonstrates how Unity can be used as a powerful educational tool to combat real-world digital dangers.

## 📜 Credits & Asset Attribution

- **Audio:** [Luminous Tranquility](https://pixabay.com/music/meditationspiritual-luminous-tranquility-531191/) & [People Heartbeat](https://pixabay.com/sound-effects/people-hearbeat-71701/) by Pixabay.
- **3D Models:** [SimplePoly City](https://assetstore.unity.com/packages/3d/environments/simplepoly-city-low-poly-assets-58899), [Furnished Cabin](https://assetstore.unity.com/packages/3d/environments/urban/furnished-cabin-71426), [Free Phone](https://assetstore.unity.com/packages/3d/props/free-phone-181455) from the Unity Asset Store.
