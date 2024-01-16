PartOne();
PartTwo();

void PartOne()
{
    var lines = File.ReadAllLines("input.txt");
    var ans = 0;
    foreach (var c in lines[0])
        if (c == '(')
            ans++;
        else ans--;
    Console.WriteLine(ans);
}

void PartTwo()
{
    var lines = File.ReadAllLines("input.txt");
    var floor = 0;
    var index = 0;
    foreach (var c in lines[0])
    {
        index++;
        if (c == '(')
            floor++;
        else floor--;
        if (floor == -1)
        {
            Console.WriteLine(index);
            return;
        }
    }

}