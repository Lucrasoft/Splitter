# ClearTheGrid

You are given the grid sizes then the grid layout via the stdin of your application think `read()` or `Console.readLine()`
After that each round you are given 2 numbers via stdin to then send a response with the number to place and where to the stdout think `print()` or `Console.writeLine()` the other number will be placed on the oppesite side of the board.

The goal is to get the most of the most connecting numbers of the same count the number is for example 5 connected 5's

### Extra clarification of the rules:

<!-- Assume you want to move cell A to cell B , then 
- Cell A **must** have a value, you **cannot** start with an empty cell.
- Cell B **must** have a value, you **cannot** land on an empty cell.
- The distance between A and B **must** be the value of cell A, you **cannot** pick a shorter or longer distance.

After your move:
- Cell A is EMPTY
- Cell B value is either the sum (B+A) or the substration (B-A), this is your choice. 
   - In case of a negative value due to the substration, the absolute (positive) value will be Cell B's new value!  -->


## Example

![alt text](image.png)

In this example you will get 2 points
2 2's are connected giving 1 point
1 1 is connected also giving 1 point

There are too many 4's on the board to give a point for that and theres to little 6's on the board for those to give a point

A example implementation can also be found in the Source/Example directory of this repo
<!-- 
We will use the first level as an example.
In this level the grid is 8x5 and contains: 

![board](Images/board1.png)

A first action to clear the grid could be to move the left 7 on top of the right 7 and substract it. This will leave us with the board: 

![board](Images/board2.png)

To get rid of the 3,1 and 2. One could move the 1 on top of the 3 and substract it. Resulting in :

![board](Images/board3.png)

To clear the grid, we move one of the 2's on top of the other. The grid is clear! 

![board](Images/board4.png) -->


## Details

The top left corner has the coordinate (0,0). The X coordinate increases to the right, the Y to the bottom.

### Input Data

1. When your program first runs via the Tester it will send a line with the sizes of the grid <width> <height>
2. Then it will send the entire grid
3. The first dice
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
Done!!!
0 0 1 2 4 5 0 0
0 3 2 2 5 3 4 0
3 3 5 5 2 4 5 1
5 2 5 2 2 5 3 3
1 5 1 5 4 1 1 4
0 2 2 2 1 3 5 0
0 0 1 4 1 5 0 0
```

The only thing you have to send is the p's the rest is what you recieve excep for the last p



<!-- ### Solutions file

You can provide an solution to a level by constructing a file which contains 
- place each *move* on a line
- each line should contain 
   - the `x` and `y` coordinate of the source cell 
   - the direction as one of the uppercase letters [`U`, `D`, `L` or `R`] for the directions : up,down,left or right.
   - your choice for addition or subtraction by writing the `+` or `-` sign. 

A (free!) solution for the first level could be:

```
0 1 R -
1 2 L -
0 2 R -
```

> The first line reads as: take the cell at [0,1] (which is the left 7 in the grid) and move it's value to the right. Substract it from the other 7. This clears out both the 7's. Etc. -->

<!-- ### Check your solution

In the source of this repo you find a `Checker` tool to verify if your generated solution file is correct. 

```
checker.exe 12.txt 12.sol
``` -->

<!-- ### Complex

It can become complex quite quickly 😆

![board](Images/Moves.png) -->
### Complex

Your solution can be as simple as taking a random number or as complex as taking into account the bonus fields (2) and more