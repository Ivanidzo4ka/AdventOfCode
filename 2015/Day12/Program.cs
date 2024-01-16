using System.Text.Json.Nodes;
PartOne();
PartTwo();
void PartOne()
{
    var ans = 0l;
    var line = Parse("input.txt")[0];
    var count = 0;
    for (int i = 0; i < line.Length; i++)
    {
        if (char.IsDigit(line[i]))
            count++;
        else
        {
            if (count != 0)
            {
                var sign = 1;
                if (line[i - count - 1] == '-')
                    sign = -1;
                ans += (sign * int.Parse(line.Substring(i - count, count)));
            }
            count = 0;
        }
    }
    Console.WriteLine(ans);
}


void PartTwo()
{
    var ans = 0l;
    var data = Parse("input.txt");
    var line = data[0];
    JsonNode root = JsonNode.Parse(line);
    ans = Walk(root);
    Console.WriteLine(ans);
}

long Walk(JsonNode root)
{
    var ans = 0l;
    if (root is JsonObject obj)
    {
        foreach (var elem in obj)
        {
            if (elem.Value is JsonValue value)
            {
                if (value.TryGetValue(out string val) && val == "red")
                    return 0;
            }
            
            ans += Walk(elem.Value);
        }
    }else if (root is JsonArray arr)
    {
        foreach (var elem in arr)
        {
            ans += Walk(elem);
        }
    }else if (root is JsonValue value)
    {
        if (value.TryGetValue(out int val))
            return val;
    }
    return ans;
}
IList<string> Parse(string fileName)
{
    var lines = File.ReadAllLines(fileName);
    return lines;
}
