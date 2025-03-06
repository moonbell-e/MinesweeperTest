# MinesweeperTest

This is a Minesweeper clone developed as a Unity Developer's test assignment.  
I recommend starting by exploring the EntryPoint script and following the game flow through method calls.

**Unity version used:** 2022.3.19f1   

## Features

- Customizable field size and mine count (configured via MinesweeperConfig in Data folder).
- First click is always safe.
- Left-click to reveal a cell, right-click to place a flag.
- Recursive reveal of empty cells with no neighboring mines.
- Game-over panel with restart button (can also restart the game by pressing a hotkey).
- Win condition: reveal all safe cells without triggering a mine.
- When the game ends:
  - On loss: all mines are revealed, with the exploded mine highlighted.
  - On win: all mines are revealed as flagged.

## Architectural Solutions

- **EntryPoint** pattern for centralized game initialization and control.
- **Zenject** is used for dependency injection to improve modularity and testability.
- **Mediator** pattern to decouple the game logic (Minefield) from the UI (TilemapView, GameResultView).
- **Factory** pattern to create different types of cells (empty, mine, numbered).
  
## Packages Used

- **Zenject** – Dependency Injection Framework.  
- **TextMeshPro** – For rendering UI text.

Enjoy!
