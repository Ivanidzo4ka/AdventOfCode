PartOne();
PartTwo();

void PartOne()
{
    var ans = 0L;
    var data = Parse("input.txt");
    var counts = new List<int>(data.Count);
    ans = Walk(data, 0, 100, counts,false);
    Console.WriteLine(ans);
}
long Walk(List<Ingredient> ing, int pos, int left, List<int> counts, bool calories)
{
    if (pos == ing.Count)
    {
        if (left == 0) return Calc(counts, ing, calories);
        else return 0;
    }
    var ans = 0L;
    for (int i = 0; i <= left; i++)
    {
        counts.Add(i);
        ans = Math.Max(ans, Walk(ing, pos + 1, left - i, counts, calories));
        counts.RemoveAt(counts.Count - 1);
    }
    return ans;
}

long Calc(List<int> counts, List<Ingredient> ing, bool calories)
{
    var t = new long[5];
    for (int i = 0; i < counts.Count; i++)
    {
        t[0] += counts[i] * ing[i].capacity;
        t[1] += counts[i] * ing[i].durability;
        t[2] += counts[i] * ing[i].flavor;
        t[3] += counts[i] * ing[i].texture;
        t[4] += counts[i] * ing[i].calories;
    }
    if (calories && t[4] != 500)
        return 0;
    if (t[0] <= 0 || t[1] <= 0 || t[2] <= 0 || t[3] <= 0)
        return 0;
    return t[0] * t[1] * t[2] * t[3];
}
void PartTwo()
{
     var data = Parse("input.txt");
    var counts = new List<int>(data.Count);
    var ans = Walk(data, 0, 100, counts,true);
    Console.WriteLine(ans);
}

List<Ingredient> Parse(string fileName)
{
    var lines = File.ReadAllLines(fileName);
    var ans = new List<Ingredient>();
    //Butterscotch: capacity -1, durability -2, flavor 6, texture 3, calories 8
    //Cinnamon: capacity 2, durability 3, flavor -2, texture -1, calories 3
    foreach (var line in lines)
    {
        var split = line.Split(": ");
        var comSplit = split[1].Split(", ");
        ans.Add(new Ingredient(int.Parse(comSplit[0].Split(" ")[1]),
        int.Parse(comSplit[1].Split(" ")[1]),
        int.Parse(comSplit[2].Split(" ")[1]),
        int.Parse(comSplit[3].Split(" ")[1]),
        int.Parse(comSplit[4].Split(" ")[1])
        )
        );
    }
    return ans;

}

public record Ingredient(int capacity, int durability, int flavor, int texture, int calories);
