# Time Zone

Time Zone is a 2D outer-space shooter developed in Unity for the Individual 2D Project.

The player controls a spaceship, destroys asteroids, earns points, survives with three lives, and progresses through increasingly difficult levels.

## Controls

- W or Up Arrow: Move up
- S or Down Arrow: Move down
- A or Left Arrow: Move left
- D or Right Arrow: Move right
- Z: Fire weapon

## Gameplay Features

2D outer-space shooter built in Unity
Four-direction player movement using WASD or arrow keys
Z key fires the player weapon
Player starts with 3 lives
Regular asteroids require 2 hits to destroy
Score increases as asteroids are hit and destroyed
Level progression is based on score
Asteroids spawn more frequently as the level increases
Asteroids also fall faster as the player reaches higher levels
Weapon progression:
Level 1: single shot
Levels 2–3: double shot
Level 4+: three-shot spread
Special health asteroids restore one life when the player is below 3 lives
Player briefly becomes invincible after taking damage
Player flashes during the invincibility period
Low-life warning sound when the player reaches 1 life
Player crash/fire effect when all lives are lost
Game Over screen
Play Again system resets the game
Multiple sound effects for lasers, asteroid hits, explosions, damage, health pickups, upgrades, level-ups, game over, and support ships
Level-up celebration ships appear when advancing levels
Celebration ships increase from 1 ship to 2 ships to a maximum of 3
Celebration ships scatter slightly in different upward directions
Scrolling background was added instead of using only a static space background
The background scrolls faster as the level increases
Persistent high-score system was added
Game Over now shows both Final Score and High Score
“NEW HIGH SCORE” feedback was added when the player breaks the saved record
Play Again text now flashes on the Game Over screen
Scoring was adjusted so points are awarded on each successful asteroid hit
A hidden support-squad Easter egg was added
Entering Up, Up, Down, Down summons 3 temporary support ships
Support ships scatter in slightly different directions
Support ships automatically fire at different randomized rates
Support ships use their own laser and collision sounds
Support ships can destroy asteroids and contribute to the player’s score
Support ships can also be destroyed if they collide with an asteroid
Support ships remain active for about 5 seconds before disappearing

## Documentation

Additional project notes are available in:

- `Assets/Documentation/Design_Changes.txt`
- `Assets/Documentation/AI_Usage.txt`

`Design_Changes.txt` describes changes made from the original one-page game design.

`AI_Usage.txt` describes how AI was used during development and troubleshooting.

## Development

Built with Unity 6.3 LTS and C#.
