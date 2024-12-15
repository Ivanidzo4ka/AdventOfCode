
using System.Text;
var dir = new (int x, int y)[] { (0, 1), (1, 0), (-1, 0), (0, -1) };

PartOne(true);
PartOne(false);
PartTwo(true);
PartTwo(false);

void PartOne(bool test = false)
{
    var data = ReadData(test);
    var ans = 0L;
    if (test)
        Console.Write("Test:");



    var n = data.board.Length;
    var m = data.board[0].Length;
    var board = data.board;
    var startx = 0;
    var starty = 0;
    for (int i = 0; i < n; i++)
        for (int j = 0; j < m; j++)
        {
            if (board[i][j] == '@')
            {
                startx = i;
                starty = j;
            }
        }

    foreach (var l in data.inst)
    {
        var d = 0;
        switch (l)
        {
            case '<': d = 3; break;
            case '^': d = 2; break;
            case '>': d = 0; break;
            case 'v': d = 1; break;
        }

        var newx = startx + dir[d].x;
        var newy = starty + dir[d].y;
        if (board[newx][newy] == '#') continue;
        if (board[newx][newy] == '.')
        {
            board[startx][starty] = '.';
            board[newx][newy] = '@';

        }
        else
        {
            var mx = 0;
            var my = 0;
            var pos = 1;
            while (true)
            {
                mx = startx + pos * dir[d].x;
                my = starty + pos * dir[d].y;
                if (board[mx][my] == 'O') pos++;
                else break;
            }
            if (board[mx][my] == '#') continue;
            else
            {
                (board[newx][newy], board[mx][my]) = (board[mx][my], board[newx][newy]);
                board[startx][starty] = '.';
                board[newx][newy] = '@';
            }
        }
        startx = newx;
        starty = newy;
        // Print(board);
    }
    Print(board);
    for (int i = 0; i < n; i++)
        for (int j = 0; j < m; j++)
            if (board[i][j] == 'O')
            {
                ans += 100 * i + j;
            }
    Console.WriteLine(ans);
}

void PartTwo(bool test = false)
{
    var data = ReadData(test);
    var ans = 0L;
    if (test)
        Console.Write("Test:");
    var board = Transform(data.board);
    Print(board);
    var n = board.Length;
    var m = board[0].Length;
    var startx = 0;
    var starty = 0;
    for (int i = 0; i < n; i++)
        for (int j = 0; j < m; j++)
        {
            if (board[i][j] == '@')
            {
                startx = i;
                starty = j;
            }
        }

    for (int z = 0; z < data.inst.Length; z++)
    {
        var l = data.inst[z];
        var d = 0;
        switch (l)
        {
            case '<': d = 3; break;
            case '^': d = 2; break;
            case '>': d = 0; break;
            case 'v': d = 1; break;
        }

        var newx = startx + dir[d].x;
        var newy = starty + dir[d].y;
        if (board[newx][newy] == '#') continue;
        if (board[newx][newy] == '.')
        {
            board[startx][starty] = '.';
            board[newx][newy] = '@';

        }
        else
        {
            var toMove = new List<(int x, int y)>();
            var totalMove = new List<(int x, int y)>();


            if (board[newx][newy] == '[')
            {
                toMove.Add((newx, newy));
            }
            else
                toMove.Add((newx, newy - 1));
            bool cant = false;
            while (toMove.Count != 0)
            {
                var nextMove = new HashSet<(int x, int y)>();
                foreach (var box in toMove)
                {
                    for (int w = 0; w < 2; w++)
                    {
                        if (w == 1 && (d == 0 || d == 3)) continue;
                        var tx = box.x + dir[d].x;
                        var ty = box.y + dir[d].y + w;
                        if (d == 0) ty++;
                        if (board[tx][ty] == '#') { cant = true; break; }
                        if (w == 0) totalMove.Add(box);
                        if (board[tx][ty] == '[' || board[tx][ty] == ']')
                        {
                            if (board[tx][ty] == '[')
                                nextMove.Add((tx, ty));
                            else
                                nextMove.Add((tx, ty - 1));
                        }

                    }
                }
                if (cant) break;
                toMove = nextMove.ToList();
            }
            if (cant) continue;
            else
            {
                totalMove.Reverse();
                foreach (var box in totalMove)
                {
                    board[box.x][box.y] = '.';
                    board[box.x][box.y + 1] = '.';
                    var tx = box.x + dir[d].x;
                    var ty = box.y + dir[d].y;
                    board[tx][ty] = '[';
                    board[tx][ty + 1] = ']';
                }
                board[startx][starty] = '.';
                board[newx][newy] = '@';
            }
        }
       
        startx = newx;
        starty = newy;
    }
    Print(board);
    for (int i = 0; i < n; i++)
        for (int j = 0; j < m; j++)
            if (board[i][j] == '[')
            {
                ans += 100 * i + j;
            }
        Console.WriteLine(ans);
}

void Print(char[][] board)
{
    Console.WriteLine("");
    for (int i = 0; i < board.Length; i++)
    {
        var sb = new StringBuilder();
        foreach (var c in board[i])
            sb.Append(c);
        Console.WriteLine(sb.ToString());
    }
}

char[][] Transform(char[][] board)
{
    var result = new char[board.Length][];
    for (int i = 0; i < board.Length; i++)
    {
        result[i] = new char[board[0].Length * 2];
        for (int j = 0; j < board[0].Length; j++)
        {
            if (board[i][j] == '#') { result[i][j * 2] = '#'; result[i][1 + j * 2] = '#'; }
            else if (board[i][j] == 'O') { result[i][j * 2] = '['; result[i][1 + j * 2] = ']'; }
            else if (board[i][j] == '.') { result[i][j * 2] = '.'; result[i][1 + j * 2] = '.'; }
            else if (board[i][j] == '@') { result[i][j * 2] = '@'; result[i][1 + j * 2] = '.'; }
        }
    }
    return result;
}
(char[][] board, string inst) ReadData(bool test = false)
{
    var lines = File.ReadAllLines(test ? "test.txt" : "input.txt");
    var pos = 0;
    var board = new List<char[]>();
    while (lines[pos] != "")
    {
        board.Add(lines[pos].ToCharArray());
        pos++;
    }
    pos++;
    var sb = new StringBuilder();
    while (pos < lines.Length)
    {
        sb.Append(lines[pos]);
        pos++;
    }
    return (board.ToArray(), sb.ToString());
    //return lines.Select(x => x.ToCharArray()).ToArray();
}