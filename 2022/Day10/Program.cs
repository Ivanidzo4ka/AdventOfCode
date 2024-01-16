StepOne();
StepTwo();
void StepOne()
{

    var lines = File.ReadAllLines("input.txt");
    int x = 1;
    var act = 0;
    var val = 0;
    var pos = 0;
    var ans = 0;
    var cycles = new HashSet<int>() { 20, 60, 100, 140, 180, 220 };
    for (int i = 1; i <= 220; i++)
    {

        if (act == 0)
        {
            x += val;
            if (cycles.Contains(i))
            {
                ans += (x * i);
            }
            var c = lines[pos++];
            if (c == "noop")
            {
                val = 0;
            }
            else
            {
                val = int.Parse(c.Split(" ")[1]);
                act = 1;
            }
        }
        else
        {
            if (cycles.Contains(i))
            {
                ans += (x * i);
            }
            act--;
        }

    }
    Console.WriteLine(ans);
}

void StepTwo(){
    var lines = File.ReadAllLines("input.txt");
    int x = 1;
    var act = 0;
    var val = 0;
    var pos = 0;
    var chars = new char[240];
    for (int i = 0; i < 240; i++)
    {
        if (act == 0)
        {
            x += val;
            var c = lines[pos++];
            if (c == "noop")
            {
                val = 0;
            }
            else
            {
                val = int.Parse(c.Split(" ")[1]);
                act = 1;
            }
        }
        else
        {
            act--;
        }
        if (Math.Abs((i%40)-x)<=1)
            chars[i]='#';
        else 
            chars[i]='.';

    }
    foreach(var ch in chars.Chunk(40))
        Console.WriteLine(string.Join("", ch));
    
}