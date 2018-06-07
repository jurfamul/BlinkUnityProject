Jurgen Famula
Game Programming 1
Game Milestone 4
Title: "Blink"

For the final milestone, I have implemented a system that allows the player to retry a level up to three times and keep track of their
score between attempts by creating a scene_manager singleton that contains the values for the player's life count and score total. This Singleton
also resets when ever the game goes back to the main menu, so the player does need to restart the game to replenish thier lives.
I have added a credits squence at the end of the game and which can be reached by beating the main level or selecting the credits from the main menu.
I implemented Tweening based movement on one of the enemies in the game and modified the behavoir of the astroids to avoid the situation where they would
block the other enemies from entering the camera's field of view. I have implemented a save system that records the player's last score and displays it along
side the current score when the player either dies three times or completes the level. My attempts to implement a particle system on the player's ship that 
would create a trial of particles when ever the player used their blinks where unsuccessful as I was unable to figure out how to bring the particles out from
behind the background. As using the preview, I was only able to see the particles when I zoomed out from enough in the scene to see the those which were moving
past the edge of the background. I was also unable to implement a traditional behavoir tree as the enemies in the game only have a single state which is unque
to that enemy. So instead I focused on presentation and usablity. I added three UI counters to the bottom of the screen which show the player how many lives,
blinks, and points they currently. Speaking of the Blink, I added several subsystems that greatly improve the mechanic's usablity. First, I added a limit to
the number of Blinks the player can use in row and created a recharge system using a timer coroutine to refill the player's blinks up the max of 4. I also
added a coroutine that causes the sprite to flash red if the player is out of blinks and blue if they try to warp outside of blink's range, the existance of which
was not properly communited in prevoius builds of the game. To increase the overall juiceness of the game, I also added additional sound effects like explosion and
blink sound effect. I also made some back end changes the scripts used by the enemies which ensure that all methods that will need to be called by another script
share naming rules to help with organization and readablity. Finally, I added several genaric "manager" scripts for the handling of UI changes and method calls
across objects in game to which should make future developement changes easier to implement.