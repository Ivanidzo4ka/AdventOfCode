Process(PartOneWalk);
Process(PartTwoWalk);
(List<string>, string) GetMapAndInstructions()
{
    var map = new List<string>();
    var readMap = true;
    string instruction = null;
    foreach (var line in File.ReadAllLines("input.txt"))
    {
        if (readMap)
        {
            if (string.IsNullOrEmpty(line))
            {
                readMap = false;
            }
            else
                map.Add(line);
        }
        else
        {
            instruction = line;
        }
    }
    for (int i = 1; i < map.Count; i++)
        if (map[i].Length != map[0].Length)
        {
            map[i] = map[i] + new String(' ', map[0].Length - map[i].Length);
        }
    return (map, instruction);
}

void Process(Func<Direction, List<string>, int, int, (int, int, Direction)> GetNext)
{
    var (map, instruction) = GetMapAndInstructions();
    var pos = 0;
    var currentx = 0;
    var currenty = 0;
    while (map[currentx][currenty] != '.')
        currenty++;

    Direction direction = Direction.Right;

    while (true)
    {
        var moves = 0;
        while (pos < instruction.Length && instruction[pos] >= '0' && instruction[pos] <= '9')
        {
            moves *= 10;
            moves += (instruction[pos] - '0');
            pos++;
        }

         
        for (int i = 0; i < moves; i++)
        {
            //Print(map, currentx, currenty,direction);
            (int nextx, int nexty, Direction newDirection) = GetNext(direction, map, currentx, currenty);
            if (map[nextx][nexty] == ' ')
            {
                Console.WriteLine("something wrong!");
            }
            if (map[nextx][nexty] == '#')
            {
                break;
            }
            else
            {
                currentx = nextx;
                currenty = nexty;
                direction = newDirection;
            }

        }
        if (pos < instruction.Length)
        {
            var directionChange = instruction[pos++];
            if (directionChange == 'L')
            {
                direction = direction switch
                {
                    Direction.Right => Direction.Up,
                    Direction.Down => Direction.Right,
                    Direction.Left => Direction.Down,
                    Direction.Up => Direction.Left,
                };
            }
            else
            {
                direction = direction switch
                {
                    Direction.Right => Direction.Down,
                    Direction.Down => Direction.Left,
                    Direction.Left => Direction.Up,
                    Direction.Up => Direction.Right,
                };
            }
        }
        else
        {
            break;
        }

    }
    Console.WriteLine(1000 * (currentx + 1) + 4 * (currenty + 1) + (int)direction);
}

(int nextx, int nexty, Direction newDirection) PartOneWalk(Direction d, List<string> map, int currentx, int currenty)
{
    //                                   R      D        L         U
    var dir = new (int x, int y)[4] { (0, 1), (1, 0), (0, -1), (-1, 0) };
    var nextx = currentx;
    var nexty = currenty;
    nextx += dir[(int)d].x;
    nexty += dir[(int)d].y;
    if (nextx < 0 || nextx >= map.Count)
    {
        nextx = (nextx + map.Count) % map.Count;
    }
    if (nexty < 0 || nexty >= map[nextx].Length)
    {
        nexty = (nexty + map[nextx].Length) % map[nextx].Length;
    }
    if (map[nextx][nexty] == ' ')
    {
        while (map[nextx][nexty] == ' ')
        {
            nextx += dir[(int)d].x;
            nexty += dir[(int)d].y;
            if (nextx < 0 || nextx >= map.Count)
            {
                nextx = (nextx + map.Count) % map.Count;
            }
            if (nexty < 0 || nexty >= map[nextx].Length)
            {
                nexty = (nexty + map[nextx].Length) % map[nextx].Length;
            }
        }
    }
    return (nextx, nexty, d);
}


(int nextx, int nexty, Direction newd) PartTwoWalk(Direction d, List<string> map, int currentx, int currenty)
{
    //                                   R      D        L         U
    var dir = new (int x, int y)[4] { (0, 1), (1, 0), (0, -1), (-1, 0) };
    var nextx = currentx;
    var nexty = currenty;
    // 1 UP goes to 6 right 
    if (currentx == 0 && currenty >= 50 && currenty < 100 && d == Direction.Up)
    {
        return (100 + currenty,0, Direction.Right);
    }
    // 1 Left goes to 4  right
    if (currenty == 50 && currentx >= 0 && currentx < 50 && d == Direction.Left)
    {
        return (149 - currentx, 0, Direction.Right);
    }
    // 2 up goes to 6  up
    if (currentx == 0 && currenty >= 100&&currenty<150 && d == Direction.Up)
    {
        return (199, currenty - 100, Direction.Up);
    }
    // 2 right goes to  5  left
    if (currenty == 149 && currentx >= 0 && currentx < 50 && d == Direction.Right)
    {
        return (149 - currentx, 99, Direction.Left);
    }
    // 2 down goes to 3  left
    if (currentx == 49 && currenty >= 100 && currenty < 150 && d == Direction.Down)
    {
        return (currenty - 50, 99, Direction.Left);
    }
    // 3 LEFT goes to 4 down
    if (currenty == 50 && currentx >= 50 && currentx < 100 && d == Direction.Left)
    {
        return (100, currentx - 50, Direction.Down);
    }// 3 RIGHT goes to 2  UP
    if (currenty == 99 && currentx >= 50 && currentx < 100 && d == Direction.Right)
    {
        return (49, 50 + currentx, Direction.Up);
    }
    //4 UP goes to 3 right
    if (currentx == 100 && currenty >= 0 && currenty < 50 && d == Direction.Up)
    {
        return (currenty + 50, 50, Direction.Right);
    }
    // 4 LEFT goest to 1 right 
    if (currenty == 0 && currentx >= 100 && currentx < 150 && d == Direction.Left)
    {
        return (149 - currentx, 50, Direction.Right);
    }
    // 5 RIGHT goes to 2 left
    if (currenty == 99 && currentx >= 100 && currentx < 150 && d == Direction.Right)
    {
        return (149 - (currentx), 149, Direction.Left);
    }
    // 5 DOWN goes to  6 left
    if (currentx == 149 && currenty >= 50 && currenty < 100 && d == Direction.Down)
    {
        return (currenty + 100, 49, Direction.Left);
    }
    // 6 LEFT goes to 1 down
    if (currenty == 0 && currentx >= 150 && currentx < 200 && d == Direction.Left)
    {
        return (0, currentx - 100, Direction.Down);
    }
    // 6 RIGHT goes to 5 UP
    if (currenty == 49 && currentx >= 150 && currentx < 200 && d == Direction.Right)
    {
        return (149, currentx - 100, Direction.Up);
    }
    // 6 DOWN goes to 2 down
    if (currentx == 199 && currenty >= 0 && currenty < 50 && d == Direction.Down)
    {
        return (0, 100 + currenty, Direction.Down);
    }
    nextx += dir[(int)d].x;
    nexty += dir[(int)d].y;


    return (nextx, nexty, d);
}

void Print(List<string> map, int currentx, int currenty, Direction d )
{
    var m = new string[4] { ">", "v", "<", "^" };
    for (int i = 0; i < map.Count; i++)
    {
        if (Math.Abs(currentx - i) > 100) continue;
        if (i != currentx)
        {
            Console.WriteLine(map[i]);
        }
        else
        {
            Console.WriteLine(map[i].Substring(0, currenty) + m[(int)d] + map[i].Substring(currenty + 1) + "   <-----");
        }
    }
    Console.WriteLine();
}

enum Direction : int
{
    Right = 0,
    Down = 1,
    Left = 2,
    Up = 3,

}
