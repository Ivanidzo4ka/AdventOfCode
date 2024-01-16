StepOne();

void StepTwo(List<int> inc, long ans, long step)
{
    long desired = 1000000000000;
    var times = (desired - step) / inc.Count;
    var rest = (desired - step) % inc.Count;
    long smartans = ans + ((((long)inc.Sum()) * times) + inc.Take((int)rest).Sum());
    Console.WriteLine(smartans);
    
    Console.WriteLine(ans);
}
void StepOne()
{
    var pattern = File.ReadAllText("input.txt");
    var chamber = new List<int[]>();
    var element = 0;
    chamber.Add(new int[7]);
    chamber.Add(new int[7]);
    chamber.Add(new int[7]);
    chamber.Add(new int[7]);
    var steps = 100003;
    var patternPos = 0;
    var posx = chamber.Count - 1;
    var posy = 2;
    var outPattern = new List<int>();
    for (int i = 0; i < steps; i++)
    {

        posy = 2;
        if (element == 1) posy++;
        Act(chamber, posx, posy, element, Draw);
        //Display(chamber);
        while (true)
        {
            //hide
            Act(chamber, posx, posy, element, Erase);
            if (pattern[patternPos] == '>')
            {
                if (Act(chamber, posx, posy + 1, element, CanMove))
                    posy++;
            }
            else
            {
                if (Act(chamber, posx, posy - 1, element, CanMove))
                    posy--;
            }
            patternPos++;
            patternPos %= pattern.Length;
            if (Act(chamber, posx - 1, posy, element, CanMove))
            {
                posx--;
                //Move down
                Act(chamber, posx, posy, element, Draw);
            }
            else
            {
                Act(chamber, posx, posy, element, Draw);
                break;
            }

        }
        element++;
        element %= 5;

        var t = FindTallest(chamber);
        outPattern.Add(t);
        var neededCount = t + 4;
        posx = neededCount;
        if (element == 1 || element == 2) neededCount += 2;
        if (element == 3) neededCount += 3;
        if (element == 4) neededCount += 1;
        while (chamber.Count <= neededCount)
            chamber.Add(new int[7]);
        // Display(chamber);
    }
    var dif = new List<int>();
    for (int i = 1; i < outPattern.Count; i++)
        dif.Add(outPattern[i] - outPattern[i - 1]);

// use this section to find patter.        
  /*  var max =0;
    for (int i = 0; i < dif.Count; i++)
    {
        var pos = 0;
        while (dif[i + pos] == 1)
            pos++;
        if (3==pos){
            max=pos;
        }
    }*/
    var inc = new List<int>();
    // get pattern!
    for(int i=2407; i< 5857; i++)
        inc.Add(dif[i]);
    StepTwo(inc, outPattern[2407], 2407);
    for (int i = 4; i < outPattern.Count; i++)
    {
        if (outPattern[i - 3] == outPattern[i - 2] && outPattern[i - 2] == outPattern[i - 1] && outPattern[i - 1] == outPattern[i])
            Console.WriteLine(i);
    }
    Console.WriteLine(FindTallest(chamber) + 1);
}
int FindTallest(List<int[]> chamber)
{
    var t = chamber.Count - 1;
    while (chamber[t].Sum() == 0)
        t--;
    return t;
}
bool Draw(List<int[]> chamber, int posx, int posy)
{
    chamber[posx][posy] = 1;
    return true;
}
bool CanMove(List<int[]> chamber, int posx, int posy)
{
    return chamber[posx][posy] == 0;
}
bool Erase(List<int[]> chamber, int posx, int posy)
{
    chamber[posx][posy] = 0;
    return true;
}

bool Act(List<int[]> chamber, int posx, int posy, int element, Func<List<int[]>, int, int, bool> ac)
{
    if (posx < 0)
        return false;
    bool ans = true;
    switch (element)
    {
        case 0:
            if (posy > 3 || posy < 0)
                return false;
            for (int i = 0; i < 4; i++)
                ans &= ac(chamber, posx, posy + i);
            break;
        case 1:
            if (posy > 5 || posy < 1)
                return false;
            ans &= ac(chamber, posx, posy);
            ans &= ac(chamber, posx + 1, posy);

            ans &= ac(chamber, posx + 1, posy - 1);
            ans &= ac(chamber, posx + 1, posy + 1);
            ans &= ac(chamber, posx + 2, posy);
            break;
        case 2:
            if (posy > 4 || posy < 0)
                return false;
            ans &= ac(chamber, posx, posy);
            ans &= ac(chamber, posx, posy + 1);
            ans &= ac(chamber, posx, posy + 2);
            ans &= ac(chamber, posx + 1, posy + 2);
            ans &= ac(chamber, posx + 2, posy + 2);
            break;

        case 3:
            if (posy > 6 || posy < 0)
                return false;
            for (int i = 0; i < 4; i++)
            {
                ans &= ac(chamber, posx + i, posy);
            }
            break;
        case 4:
            if (posy > 5 || posy < 0)
                return false;
            ans &= ac(chamber, posx, posy);
            ans &= ac(chamber, posx, posy + 1);
            ans &= ac(chamber, posx + 1, posy);
            ans &= ac(chamber, posx + 1, posy + 1);
            break;

    }
    return ans;
}

void Display(List<int[]> chamber)
{
    for (int i = chamber.Count - 1; i >= 0; i--)
        Console.WriteLine(string.Join("", chamber[i].Select(x => x == 0 ? '.' : '#')));
    Console.WriteLine();
}