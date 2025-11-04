# RPG_OOD_Game

## Project Description

This repository contains the source code for a C# based RPG game project, utilizing Object-Oriented Design (OOD) principles. While no initial description was provided, this README will serve to outline the project structure, potential features, and contribute to a better understanding of its purpose.

## Key Features & Benefits

*   **Object-Oriented Design:** Employs OOD principles for modularity, reusability, and maintainability.
*   **Extensible Effect System:** The `EffectBase` and related classes demonstrate an extensible system for applying various effects (Health, Luck, Power) to entities.
*   **Command Pattern Implementation:** Utilizes the command pattern (`PlayerCommand`) for managing player actions.
*   **Input Handling:** A basic input handling structure using `BaseInputHandler` and `InputHandlerChain` is present.
*   **Entity Management:** Basic Entity framework defined (`IEntity`, `Player`, `Enemy`).

## Prerequisites & Dependencies

*   .NET SDK (version 6.0 or later recommended)
*   A C# IDE (e.g., Visual Studio, VS Code with C# extension, Rider)

## Installation & Setup Instructions

1.  **Clone the Repository:**
    ```bash
    git clone https://github.com/Piotereko/RPG_OOD_Game.git
    cd RPG_OOD_Game
    ```

2.  **Build the Project:**
    *   **Using the .NET CLI:**
        ```bash
        dotnet build
        ```
    *   **Using Visual Studio:** Open the solution file in Visual Studio and build the project.

3.  **Run the Project:**
    ```bash
    dotnet run
    ```
    *   If the project contains multiple executables, specify the target project:
    ```bash
    dotnet run --project <PathToExecutableProject>
    ```

## Usage Examples & API Documentation

As this is a preliminary project, comprehensive API documentation is not yet available.  However, the structure of the code provides some insight.

*   **Applying an Effect:**

    ```csharp
    // Example: Applying a HealthEffect to an Enemy
    Enemy goblin = new Goblin();
    IEffect healthEffect = new HealthEffect(10); // Increase health by 10
    ActionExecutor executor = new ActionExecutor();
    executor.ApplyEffect(goblin, healthEffect);
    // Goblin's health is now increased by 10.
    ```

*   **Handling Player Input (Conceptual):**

    The `InputHandlerChain` suggests a chain-of-responsibility pattern for handling player input.  Specific input handlers would be added to the chain to process different commands.

## Configuration Options

Currently, there are no configurable options available directly within the code. Further development would allow the introduction of configuration files for parameters like enemy stats, game difficulty, etc.

## Contributing Guidelines

We welcome contributions to enhance this RPG project. Please follow these guidelines:

1.  **Fork the Repository:** Create your own fork of the repository.
2.  **Create a Branch:** Create a feature branch for your changes.
    ```bash
    git checkout -b feature/your-feature-name
    ```
3.  **Make Changes:** Implement your desired improvements or bug fixes.
4.  **Commit Changes:** Commit your changes with clear and descriptive commit messages.
    ```bash
    git commit -m "Add: Description of your changes"
    ```
5.  **Push to Your Fork:** Push your branch to your forked repository.
    ```bash
    git push origin feature/your-feature-name
    ```
6.  **Create a Pull Request:** Submit a pull request from your branch to the main branch of the original repository.

Please ensure your code adheres to existing coding standards and includes appropriate unit tests.

## License Information

License not specified. All rights reserved by Piotereko.
If you wish to use any part of this code, please contact the owner.

## Acknowledgments

While no specific third-party resources were used at the time of this README creation, gratitude is expressed for the general open-source ecosystem and .NET community which provide invaluable resources for software development.
