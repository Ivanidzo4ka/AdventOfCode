PartOne(true);
PartOne(false);
PartTwo(true);
PartTwo(false);
void PartOne(bool test = false)
{
    var data = ReadData(test);
    var n = data.Length;
    var m = data[0].Length;

    var ans = 0L;
    if (test)
        Console.Write("Test:");

    for (int i = 0; i < n; i++)
        for (int j = 0; j < m; j++)
        {
            if (data[i][j] == 'X')
            {
                for (int x = -1; x <= 1; x++)
                    for (int y = -1; y <= 1; y++)
                        ans += Count(data, i, j, x, y, "MAS");
            }
        }
    Console.WriteLine(ans);
}

int Count(string[] data, int i, int j, int x, int y, string rest)
{
    if (rest == "") return 1;
    var ans = 0;

    {
        var newi = x + i;
        var newj = y + j;
        if (newi < 0 || newi >= data.Length || newj < 0 || newj >= data[0].Length)
            return 0;
        if (data[newi][newj] == rest[0])
            ans += Count(data, newi, newj, x, y, rest.Substring(1));
    }
    return ans;
}
void PartTwo(bool test = false)
{
    var data = ReadData(test);
    var ans = 0L;
    if (test)
        Console.Write("Test:");
    var n = data.Length;

    for (int i = 0; i < n - 2; i++)
        for (int j = 0; j < n - 2; j++)
        {
            /*
            M S
             A
            M S
            */
            if (data[i][j] == 'M' && data[i + 2][j] == 'M' && data[i + 1][j + 1] == 'A' && data[i][j + 2] == 'S' && data[i + 2][j + 2] == 'S')
                ans++;
            /*
            M M
             A
            S S
            */
            if (data[i][j] == 'M' && data[i + 2][j] == 'S' && data[i + 1][j + 1] == 'A' && data[i][j + 2] == 'M' && data[i + 2][j + 2] == 'S')
                ans++;
            /*
            S S
             A
            M M
            */
            if (data[i][j] == 'S' && data[i + 2][j] == 'M' && data[i + 1][j + 1] == 'A' && data[i][j + 2] == 'S' && data[i + 2][j + 2] == 'M')
                ans++;
            /*
            S M
             A
            S M
            */
            if (data[i][j] == 'S' && data[i + 2][j] == 'S' && data[i + 1][j + 1] == 'A' && data[i][j + 2] == 'M' && data[i + 2][j + 2] == 'M')
                ans++;
        }
    Console.WriteLine(ans);

}

string[] ReadData(bool test = false)
{
    var lines = File.ReadAllLines(test ? "test.txt" : "input.txt");
    return lines;
}