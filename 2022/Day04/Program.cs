StepOne();
StepTwo();

void StepOne(){
var total =0;
foreach(var line in File.ReadAllLines("input.txt"))
{
    var split = line.Split(',');
    var first=  GetInterval(split[0]);
    var second = GetInterval(split[1]);
     if (first.x<= second.x&& second.y<=first.y)
        {
            total++;
            continue;
        }
    if (second.x<=first.x&& first.y<=second.y)
    {
        total++;
    }
}
Console.WriteLine(total);
}

void StepTwo(){
var total =0;
foreach(var line in File.ReadAllLines("input.txt"))
{
    var split = line.Split(',');
    var first=  GetInterval(split[0]);
    var second = GetInterval(split[1]);
       if ((second.x<=first.x&& first.x<=second.y)||(second.x<=first.y&&first.y<=second.y))
        {
            total++;
            continue;
        }
    if ((first.x<=second.x&& second.x<=first.y)||(first.x<=second.y&&second.y<=first.y))
    {
        total++;
    }
   
}
Console.WriteLine(total);
}


(int x, int y) GetInterval(string str)
{
    var split = str.Split('-');
    var x = int.Parse(split[0]);
    var y = int.Parse(split[1]);
    return (x, y);
}