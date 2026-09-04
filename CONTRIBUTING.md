# Contributing to Grand Life Adventures

## Development Setup

### Requirements
- Visual Studio 2019 or later
- GTA V SDK/Headers
- ScriptHook V SDK
- .NET Framework 4.7.2+

### Getting Started

1. Clone the repository
2. Install dependencies from the `dependencies/` folder
3. Open the solution in Visual Studio
4. Build the project
5. Run the deployment batch script

## Project Structure

- **src/core/** - Core mod initialization and management
- **src/systems/** - Individual gameplay systems (relationships, career, family)
- **src/ui/** - User interface and menu systems
- **src/utils/** - Helper functions and utilities
- **scripts/** - Lua/Script files for game logic
- **config/** - Configuration files

## Coding Standards

- Use descriptive variable and function names
- Comment complex logic
- Follow existing code style
- Test thoroughly before submitting PRs

## Pull Request Process

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/amazing-feature`)
3. Commit your changes
4. Push to the branch
5. Open a Pull Request

## Reporting Bugs

Include:
- GTA V version and installation method
- ScriptHook V version
- Reproduction steps
- Error logs if available
