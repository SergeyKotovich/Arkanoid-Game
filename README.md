# Test Game Arkanoid

https://github.com/user-attachments/assets/dacfa755-e16f-4a2b-9b63-427c88e71598

### Overview
This is a 2D arcade game. The player controls a paddle to bounce a ball, which breaks blocks on the screen. The goal of the game is to destroy all the blocks without letting the ball fall off the screen. The game is built using Unity and features simple yet engaging gameplay mechanics.

### Technologies and Design Patterns Used
* VContainer: Dependency Injection framework for Unity.
* Message Pipe: Messaging system for Unity that enables event handling with the publish-subscribe pattern.
* UniTask : Efficient async/await support for Unity, optimizing asynchronous operations for game development.

### Character Logic
* The platform is located at the bottom of the screen and is controlled by the player using the AD keys or the mouse.
* The character's movement is restricted to a rectangular area, with the left and right edges being the screen boundaries and the bottom edge being the loss boundary.

### Win and Lose Conditions
* The player wins when all the blocks are destroyed.
* The player loses when his life is 0.

### Interface
* In the top left corner of the screen, there is a display showing the number of lives and the score.
* After losing, a window appears with the title "Game Over" and a "Return to Main Menu" button, which restarts the game when clicked.
* After winning, a window appears with the title "You Win" and a "Return to Main Menu" button, which also restarts the game when clicked.

### Technical Requirements
* Use Unity 2022.3.20f1.
* The project must be strictly 2D.
* A script for level rendering is provided.
* The scene orientation is horizontal.
* Completion time: 1 week.
* Actual time spent: ~20 hours.
