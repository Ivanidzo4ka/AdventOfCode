using System.ComponentModel;

PartOne();
PartTwo();

void PartOne()
{
    var ans = 0L;
    var data = Parse("input.txt");
    var house = 100000;
    while (true)
    {
        var presents = Presents(house);
        if (presents>= data)
        {
            Console.WriteLine(house);
            break;
        }
        house++;
    }

}
long Presents(long houseNumber)
{
    var res = 10L;
    if (houseNumber > 2)
        res += houseNumber * 10;
    var limit = (long)Math.Sqrt(houseNumber);
    for (int i = 2; i <= limit; i++)
        if (houseNumber % i == 0)
            {
                res += 10 * i;
                if (houseNumber/i>limit)
                res+=(houseNumber/i)*10;
            }
    return res;
}

long PresentsTwo(long houseNumber)
{
    var res = 10L;
    
    var limit = (long)Math.Sqrt(houseNumber);
    for (int i = 1; i <= limit; i++)
        if (houseNumber % i == 0)
            {
                if (houseNumber/i<=50)
                    res += 11 * i;
                if (houseNumber/i>limit&&i<=50)
                    res+=houseNumber/i*11;
            }
    return res;
}
void PartTwo()
{
    var data = Parse("input.txt");
    var house = 665280;
    while (true)
    {
        var presents = PresentsTwo(house);
        if (presents>= data)
        {
            Console.WriteLine(house);
            break;
        }
        house++;
    }
}

long Parse(string fileName)
{
    var lines = File.ReadAllLines(fileName);

    return long.Parse(lines[0]);
}

