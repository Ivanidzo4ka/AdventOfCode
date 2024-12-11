using System.Diagnostics;
using System.Text;

PartOne(true);
PartOne(false);
PartTwo(true);
PartTwo(false);
void PartOne(bool test = false)
{
    var data = ReadData(test);
    var ans = 0L;
    if (test)
        Console.Write("Test:");
    var disk = new List<int>();
    for (int i = 0; i < data.Length; i++)
    {

        for (char j = '0'; j < data[i]; j++)
        {
            if ((i & 1) == 0)
            {
                disk.Add(i / 2);
            }
            else
            {
                disk.Add(-1);
            }
        }
    }
    var r = disk.Count - 1;
    var l = 0;
    do
    {
        while (disk[l] != -1 && l < disk.Count) l++;
        while (disk[r] == -1 && r > 0) r--;
        if (l < r)
        {
            (disk[l], disk[r]) = (disk[r], disk[l]);
        }
        else
            break;
    } while (true);
    for (int i = 0; i < disk.Count; i++)
    {
        if (disk[i] != -1)
            ans += disk[i] * i;
    }
    Console.WriteLine(ans);
}

void PartTwo(bool test = false)
{
    var data = ReadData(test);
    var ans = 0L;
    if (test)
        Console.Write("Test:");

    var disk = new List<int>();
    for (int i = 0; i < data.Length; i++)
    {

        for (char j = '0'; j < data[i]; j++)
        {
            if ((i & 1) == 0)
            {
                disk.Add(i / 2);
            }
            else
            {
                disk.Add(-1);
            }
        }
    }
    var r = disk.Count - 1;
    var l = 0;
    var lsize = 0;
    do
    {
        var rsize = 0;
        while (disk[r] == -1 && r >= 0) r--;
        if (disk[r]==0) break;
        while (disk[r - rsize] ==disk[r]) rsize++;
        
        while (true)
        {
            lsize=0;
            while (disk[l] != -1 && l < r) l++;
            while (disk[l + lsize] == -1) lsize++;
            if (l >= r) break;
            if (lsize >= rsize) break;
            else
            {
                l += lsize;
            }
        }
        if (l < r)
        {
            var t = rsize-1;
            while (t >=0)
            {
                (disk[l + t], disk[r - t]) = (disk[r - t], disk[l + t]);
                t--;
            }
        }
        else {
            r-=rsize;
        }
        l=0;

    } while (true);
    for (int i = 0; i < disk.Count; i++)
    {
        if (disk[i] != -1)
            ans += disk[i] * i;
    }

    Console.WriteLine(ans);
}

string ReadData(bool test = false)
{
    var lines = File.ReadAllLines(test ? "test.txt" : "input.txt");
    return lines[0];
}