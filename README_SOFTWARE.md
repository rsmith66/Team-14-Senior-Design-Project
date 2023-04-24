# Team 14 Software Report
## Software Modules
### Simon - Pattern Matching Game
![Simon Gameplay Pic](https://user-images.githubusercontent.com/75346297/228964391-c9705f1e-4803-44c0-b0a3-24ab386930bd.jpg)

#### Gameplay
This game is based off the childhood game Simon that has four differently colored buttons that display an incrementally long pattern that the user must duplicate. This game does not have a set number of buttons but makes the buttons based on how many holds are on the climbing wall. For this game, the user stands in front of the holds and the holds will change color and output an audio to show a pattern. Once the pattern is fully displayed, the user will press the buttons on the sensors in the correct order. If the user presses all the sensors in the correct order that was displayed, the game will add 100 points to the score and then will commence the generating a new pattern with one additional hold to press added to the pattern. If the user does not get any part of the pattern correct, the game moves to the Game Over screen and displays the score and a button to press if they want to try again.

#### Code
![Simon Display Pattern](https://user-images.githubusercontent.com/75346297/234076435-2ac5e146-ae52-413f-b2af-17126ddba0b4.jpg)
Most of the code for the functionality of the game can be found in the CreateHold.cs file. The game is able to work with the AR wall by first using the calibration.txt file that it receives from the calibration phase, which is a text file that has the x and y coordinates of each climbing hold, and creates square interactable game objects for each climbing hold at the coordinated location. Once the game begins, the code works by choosing one of the random holds and adding it to the pattern that it is generating. It then changes the color of the game objects and plays a clip of audio for one second. Then it waits for user inputs. How we get the user input is by reading from the data.txt file that is generated from the raspberry pi which includes a matrix of 1s and 0s where the 0 represents if a hold is not pressed and a 1 is generated if it is pressed. The code continually checks this file for any updates and if a hold changes from a 0 to a 1, it will activate the hold and, if it is a hold in the correct sequence, change its color and play the audio until it is no longer held. The code then cycles to displaying a pattern again and continues along this strategy until the game is over.

### Missile Launch Game
![TestReportGame](https://user-images.githubusercontent.com/75346297/228968182-6e5e0973-ad4c-4912-9f4d-43194faa0330.jpg)

#### Gameplay
The gameplay for missile launch involves a user hanging on the wall and having to move themselves across the holds to evade missiles that are falling from the top of the screen. The user must dodge the missiles for 45 seconds or until they run out of their three lives. If the user gets hit, the missile turns into a cartoon explosion and he or she loses a life. Missiles continue to fall at different speeds throughout the game, until the user either loses three lives or 45 seconds has elapsed and then the Game Over screen will appear along with the player's score and a button allowing them to play again. 
#### Code
![Holds code](https://user-images.githubusercontent.com/75346297/228970583-af1d875d-9eb7-4ad1-bf57-c4d8f7af24f2.jpg)

The code for Missile Launch uses the same method as Simon to calibrate with the wall and the sensor inputs. It creates a virtual wall within the game by taking in the calibration.txt file and creating the game objects based off the coordinates there. It then repeatedly checks for user inputs based on the data.txt file that contains the matrix of 0s and 1s depending on if the hold is pressed or not. The other additions besides the basic functionality of the game are that since we wanted the missile to explode once the missile hits the body of the person and not just the holds, we wrote code to draw lines between each hold that will be interactable and cause the missile to explode upon impact. A screenshot of the code for creating the lines is above. We had to create and destroy these lines along the same measure of time that we checked the text file for which holds were activated. This checking for updated holds and creating/destroying lines continues until 45 seconds has elapsed or the user has lost three lives.

### Multiplexing Code

### Reading from Sensor Data File Code

## Connecting the Modules

## Installing and Starting Software
