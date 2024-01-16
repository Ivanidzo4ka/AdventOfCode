
using System.Reflection.Metadata.Ecma335;

PartOne();

void PartOne()
{
    var ans = 0L;
    var data = Parse("input.txt");
    ans = GetValue(data.row, data.col);
    Console.WriteLine(ans);
}

long GetValue(int expectedRow, int expectedCol)
{
    var row = 1;
    var col = 1;
    var val = 20151125L;
    while (!(row == expectedRow && expectedCol == col))
    {
        val *= 252533;
        val %= 33554393;
        if (row == 1)
        {
            row = col + row;
            col = 1;
        }
        else
        {
            col++;
            row--;
        }
    }

    return val;
}

(int row, int col) Parse(string fileName)
{
    var lines = File.ReadAllLines(fileName);
    var sp = lines[0].Split(",");
    return (int.Parse(sp[0]), int.Parse(sp[1]));

}
