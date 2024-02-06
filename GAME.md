# Splitter

In this game you must group numbers to score points -- two 2s, three 3s, and so on -- but you're placing two numbers at a time, so things won't always work out.

### Extra clarification of the rules:

- Each round you must place 2 numbers based on the numbers from the dice
- You can choose where to place one number on the left side of the grid and the other will be mirrored to the other side
- If you get 4 connecting 4's you get 4 points, 5 connecting 5's you get 5 points, and so on
- If you get more or less than the number you will get no points
- A bonus space is present on each half of the pattern, and a scored group that contains this starred space has its points doubled

## Example

![alt text](image.png)

In this example you will get 3 points
2 2's are connected giving 2 point
1 1 is connected also giving 1 point

There are too many connected 4's on the board to give a point for that and theres to little connected 6's on the board for those to give a point

A example implementation can also be found in the Source/Example directory of this repo

## Details

The top left corner has the coordinate (0,0). The X coordinate increases to the right, the Y to the bottom.

### Input Data

1. When your program first runs via the Tester it will send a line with the sizes of the grid <width> <height> 
2. Then it will send the entire grid
3. The first dice
4. A 2 in the grid means its a bonus space
5. You cannot place numbers on 0's

After that you send the response back in the following format

p <number> <x>,<y>

Heres a example loop:

```
7 8
0 0 1 1 1 1 0 0
0 1 1 1 1 1 1 0
1 1 1 1 1 1 1 1
2 1 1 1 1 1 1 2
1 1 1 1 1 1 1 1
0 1 1 1 1 1 1 0
0 0 1 1 1 1 0 0
d 2 4
p 2 3,0
d 2 3
p 2 1,3
d 1 1
p 1 2,4
d 5 4
p 5 2,2
d 5 5
p 5 2,3
d 2 3
p 2 2,5
d 1 5
p 1 2,6
d 3 1
p 3 0,2
d 5 3
p 5 0,3
d 5 1
p 5 1,4
d 5 2
p 5 3,2
d 4 1
p 4 3,6
d 5 4
p 5 3,4
d 2 3
p 2 2,1
d 2 2
p 2 3,3
d 3 5
p 3 1,2
d 3 4
p 3 1,1
d 2 5
p 2 3,1
d 1 5
p 1 2,0
d 2 1
p 2 3,5
d 2 5
p 2 1,5
d 1 4
p 1 0,4
```

The only thing you have to send is the p's the rest is what you recieve excep for the last p


### Testing

Theres a exe in the releases panel of this repo you can use to test the program as follows `./Tester.exe "commands to execute" <games>` so for example `./Tester.exe "node bot.js" 20`

### Complex

Your solution can be as simple as taking a random number or as complex as taking into account the bonus fields (2) and more