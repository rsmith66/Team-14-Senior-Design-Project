import RPi.GPIO as GPIO
import curses
import time
from collections import defaultdict

GPIO.setmode(GPIO.BCM)

#16 cycles of 4 pins, 0th pin maps to GPIO port 6
NUM_GPIO =  4
NUM_MUX_INPUT = 16
NUM_SELECT = 4
OUTPUT_FILE = "data.txt"

GPIOmap = [6,13,19,26]
for GPIOport in GPIOmap:
    GPIO.setup(GPIOport, GPIO.IN)

#4 mux select pins
select = 0
MUXmap = [12,16,20,21] #s0,s1,s2,s3
for MUXport in MUXmap:
    GPIO.setup(MUXport, GPIO.OUT)

# ith row is ith MUX pin, jth col is jth GPIO pin
data = [[0]*NUM_GPIO for x in range(NUM_MUX_INPUT)]

stdscr = curses.initscr()
curses.noecho()
curses.cbreak()

while(True):
    #update data
    time.sleep(0.001)
    for idx in range(NUM_GPIO):
        data[select][idx] = GPIO.input(GPIOmap[idx])
    with open(OUTPUT_FILE,'w') as fout:
        for idx,row in enumerate(data):
            fout.write(f"{' '.join([str(x) for x in row])}\n")
            stdscr.addstr(idx,0,f"{' '.join([str(x) for x in row])}\n")
        stdscr.refresh()
    #update select
    select+=1; select %= 16;
    temp = select
    for idx in range(NUM_SELECT):
        GPIO.output(MUXmap[idx],temp%2)
        temp = temp//2
