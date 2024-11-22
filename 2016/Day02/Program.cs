using System.Text;
PartOne();
PartTwo();
void PartOne()
{

    var x = 1;
    var y = 1;

    var lines = File.ReadAllLines("input.txt");
    var pad = new char[3][]{
    new char[]{'1', '2', '3'},
    new char[]{'4', '5', '6'},
    new char[]{'7', '8', '9'}};
    var ans = new StringBuilder();
    foreach (var line in lines)
    {
        foreach (var move in line)
        {
            switch (move)
            {
                case 'U': if (x > 0) x--; break;
                case 'D': if (x < 2) x++; break;
                case 'R': if (y < 2) y++; break;
                case 'L': if (y > 0) y--; break;
            }
        }
        ans.Append(pad[x][y]);
    }
    Console.WriteLine(ans.ToString());
}

void PartTwo()
{

    var x = 3;
    var y = 1;

    var lines = File.ReadAllLines("input.txt");
    var pad = new string[7]{
        "       ",
        "   1   ",
        "  234  ",
        " 56789 ",
        "  ABC  ",
        "   D   ",
        "       "
    };
 
    var ans = new StringBuilder();
    foreach (var line in lines)
    {
        foreach (var move in line)
        {
            var newx=x;
            var newy=y;
            switch (move)
            {
                case 'U': newx--; break;
                case 'D': newx++; break;
                case 'R': newy++; break;
                case 'L': newy--; break;
            }
            if (pad[newx][newy]!=' ')
            {
                x = newx;
                y = newy;
            }
        }
        ans.Append(pad[x][y]);
    }
    Console.WriteLine(ans.ToString());
}
