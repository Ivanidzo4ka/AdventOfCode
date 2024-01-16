PartOne();
PartTwo();

void PartOne()
{
    var ans=0L;
    foreach (string line in File.ReadAllLines("input.txt"))
    {
        
        var dim =line.Split("x").Select(x=>long.Parse(x)).ToArray();
        
        ans+=(2*dim[0]*dim[1]) +(2*dim[1]*dim[2])+(2*dim[0]*dim[2])+(
            Math.Min(dim[0]*dim[1], 
            Math.Min(dim[0]*dim[2], dim[1]*dim[2])));
    }

    Console.WriteLine(ans);
}
void PartTwo()
{
    var ans=0L;
   foreach (string line in File.ReadAllLines("input.txt"))
    {
        
        var dim =line.Split("x").Select(x=>long.Parse(x)).ToArray();
        Array.Sort(dim);
        ans+=(dim[0]+dim[0]+dim[1]+dim[1])+(dim[0]*dim[1]*dim[2]);
    }


    Console.WriteLine(ans);
}
