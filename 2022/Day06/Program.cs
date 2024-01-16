Step(4);
Step(14);
void Step(int count)
{
    var marker = new char[count];
    var index = 0;
    foreach (var t in File.ReadAllText("input.txt"))
    {
        index++;
        for(int i=count-1; i>=1; i--)
            marker[i] = marker[i-1];
        marker[0] = t;
        if (index>=count&&marker.Distinct().Count()==count)
        {
            Console.WriteLine(index);
            break;
        }
    }
}