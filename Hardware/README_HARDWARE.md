# Team 14 Hardware Report

## Enclosure

![Enclosure Assembly](/Hardware/Pictures/hardware_diagram_transparent.png)
The manufactured parts portion is made up of 4 distinct parts: main coil enclosure, circuit button holder, button, and coil holder. The main coil enclosure is mounted on the front of the wall between the rock climbing hold and the wall itself. It has custom fit indentations for the two front side coils, the button holder, and button itself. The button and the circuit button holder are inserted between the main enclosure and wall in that order. They are built tightly enough to clearly lock into place, but they are mainly secured by the pressure between the wall and the enclosure. Lastly on the back side there is a coil holder piece which holds the two rear side coils into the correct position; this piece is also mounted on the standard bolt and requires no additional mounting hardware. Not used in the final product, but a key component of its assembly, is also a custom coil winding tool, which allows for the coils to be wound much more quickly and ensures they fit correctly. The printable files for all of these components can be found in the STLs folder.

## Circuitry

![Circuit Diagram](/Hardware/Pictures/circuit.png)
![Circuit Diagram](/Hardware/Pictures/circuit_flow.png)
Some circuit elements integrate into the enclosure in the following ways. Two coils and two press buttons are integrated into the front of the coil enclosure in the corresponding indents. These allow for the signal to be passed through when the button is depressed. Two coils are also mounted on the back of the wall in their designated slots and are then connected to the Raspberry Pi and an AC power source.
The remaining hardware consists of comparators, multiplexers, full bridge rectifiers, an AC power source, a projector, and a raspberry pi. These are connected in the configuration shown in the circuit diagram, such that the AC power source powers the input coil; the AC signal from the output coil (when the frontside button is depressed) is converted to DC with the full bridge rectifier; the multiplexer (controlled by the raspberry pi) selects a signal to read at a given time; the signal is normalized to a constant 3.3V or 0V DC by the comparator; the raspberry pi reads the output of the comparator as a digital signal. At this point these signals are by the software and the resulting game is displayed on the projector.
