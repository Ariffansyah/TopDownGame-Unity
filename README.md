# Top-Down Game Example Project (Unity)

This repository is an example Unity project demonstrating several core systems commonly used in game development, specifically for top-down games.  
It is intended for learning, experimentation, and as a reference for implementing foundational gameplay features.

## Features

- **Top-Down Player Controller**
  - Move the player character in a top-down environment using Unity's Input System.
  - Jump and interact with the game world.

- **Dialogue System**
  - Simple dialogue interactions and branching conversations.
  - Easily expandable for more complex dialogue trees (supports Ink integration).

## Getting Started

1. **Clone the repository**
   ```bash
   git clone https://github.com/Ariffansyah/TopDownGame-Unity.git
   ```

2. **Open in Unity**
   - Launch Unity Hub and add the project folder.
   - Recommended Unity version: 2020.3 or newer.

3. **Explore Example Scenes**
   - Open any scene in the `Assets/Scenes/` folder to try out player controls and dialogue.

## Folder Structure

```
Assets/
  Animations/      # Animation controllers and clips
  Art/             # Sprites, textures, and visual assets
  Dialogue/        # Dialogue scripts and Ink files
  Ink/             # Ink integration assets
  Prefabs/         # Prefabs for reusable game objects
  Scenes/          # Unity scene files
  Scripts/         # C# scripts for gameplay and systems
  Settings/        # Project and game settings
  TextMesh Pro/    # TextMesh Pro assets
  Tiles/           # Tilemaps and related assets
```

## Requirements

- Unity 2020.3 or newer
- Input System package (`com.unity.inputsystem`)
- (Optional) Ink plugin for advanced dialogue

## How to Use

- **Player Controller:**  
  Attach the provided script in `Scripts/` to your player GameObject and set up Input Actions using Unity's Input System.

- **Dialogue System:**  
  Use the dialogue manager and assets in `Dialogue/` or `Ink/` to set up interactive conversations.

## Contributing

Contributions, issues, and pull requests are welcome.  
Feel free to use this repository as a template for your own top-down Unity games.

## Credits

- Art and assets by [Cup Nooble](https://cupnooble.itch.io/)

## License

This project is licensed under the [MIT License](LICENSE).

