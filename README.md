# Assignment_KaustubhPrabhu

Snake 3D


## Desccription

Sanke 3D is a 3D snake game in which player's goal is to achieve maximum score as player can to beat all time max score.


## Controls

In this game, player is a snake which can move in 2 Dimensions using below keys on keyboard.

Movement:

W = up direction
A = left direction
S = down direction
D = right direction


## Flow

The first scene in game is Main Menu. You can kickstart the game by clicking on "START".

One the game is loaded, player can play. 
After player dies, a pop up appears on screen showing the all time max score and options to "RETRY" (try again and play) or go back to "MAIN MENU" (Main Menu).


## Gameplay Mechanics

When the game is loaded, the type of fruits it wants to spawn are loaded from a ".xml" file. 
The xml file contains mainly two data.

1. Color of the fruit - This is stored in 4 variables for values of Red, Green, Blue and Alpha components of color. 
                        The value of each component varies between 0 and 1.
2. Points to add to score - This stores the points that needs to be added in score when this fruit is eaten.

GameMaster fetches the above data for each type of fruit and assigns to the corresponding values when the fruit is spawned.


Game also stores player's all time max score. It is stored in binary file format.

Player can increase the score by eating the fruits spawning on the map. Every new fruit is spawned randomly when the previous one is eaten by player.
Aong with fruits bombs are also spawned on the map randomly.
If player eats the bomb or hits any edge of the wall surrounding the map, the player dies and the game is over. 
Player also dies if it tries to eat its own tail.

Everytime player eats the fruit with same color as previous one, the fruit's score is multiplied by current streak of eating the same fruit. 
Current streak is displayed on screen whenever the score is added.

# Assets used

The sanke is made of default sphere in unity with a material on it.
The fruits are also default sphere with a particle system on it.
The bomb mesh is imported asset with a particle system.
The UI elements (only images and fonts) are imported assets.