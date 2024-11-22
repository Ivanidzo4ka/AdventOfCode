// See https://aka.ms/new-console-template for more information
//PartOne();
PartTwo();
void PartOne()
{
    var lines = File.ReadAllLines("input.txt");
    var x=0;
    var y=0;
    var dir = new (int x, int y)[4]{(-1,0), (0,1), (1,0), (0,-1)};
    var d = 0;
    foreach(var cur in lines[0].Split(", "))
    {
        if (cur[0]=='R')
            d++;
        else d--;
        d+=4;
        d%=4;
        var l = int.Parse(cur.Substring(1,cur.Length-1));
        x+=dir[d].x*l;
        y+=dir[d].y*l;
    }
    Console.WriteLine($"{Math.Abs(x)+Math.Abs(y)}");
}

void PartTwo()
{
     var lines = File.ReadAllLines("input.txt");
    var x=0;
    var y=0;
    var dir = new (int x, int y)[4]{(-1,0), (0,1), (1,0), (0,-1)};
    var d = 0;
    var visited = new HashSet<(int x, int y)>();
    visited.Add((x, y));
    foreach(var cur in lines[0].Split(", "))
    {
        if (cur[0]=='R')
            d++;
        else d--;
        d+=4;
        d%=4;
        var l = int.Parse(cur.Substring(1,cur.Length-1));
        bool done=false;
        for(int i=0; i< l; i++)
        {
            x+=dir[d].x;
            y+=dir[d].y;
            if (!visited.Add((x,y)))
            {
                done=true;
                break;
            }
        }
        if (done)
            break;
    }
    Console.WriteLine($"{Math.Abs(x)+Math.Abs(y)}");
}