Solution();
void Solution()
{
    var root = new Folder(null);
    Folder current = null;
    foreach (var line in File.ReadAllLines("input.txt"))
    {
        if (!line.StartsWith("$"))
        {
            if (line.StartsWith("dir"))
            {
                var fol = line.Substring(4);
                if (!current.Folders.ContainsKey(fol))
                {
                    current.Folders[fol] = new Folder(current);
                }
            }
            else
            {
                var split = line.Split(" ");
                var name = split[1];
                var size = long.Parse(split[0]);
                current.Files[name] = size;
            }
        }
        else if (line.StartsWith("$ cd"))
        {
            var arg = line.Substring(5);
            switch (arg)
            {
                case "/":
                    current = root;
                    break;
                case "..":
                    current = current.Parent;
                    break;
                default:
                    current = current.Folders[arg];
                    break;
            }
        }

    }
    Fill(root);

    Console.WriteLine(WalkOne(root));
    long minSize = 30000000 - (70000000 - root.TotalSize);
    Console.WriteLine(WalkTwo(root, minSize));
}

void Fill(Folder folder)
{

    long total = 0;
    foreach (var files in folder.Files)
    {
        total += files.Value;
    }
    foreach (var fol in folder.Folders.Values)
    {
        Fill(fol);
        total += fol.TotalSize;

    }
    folder.TotalSize = total;
}

long WalkOne(Folder folder)
{
    long ans = 0;
    foreach (var fol in folder.Folders.Values)
    {
        ans += WalkOne(fol);
    }
    if (folder.TotalSize <= 100000)
        ans += folder.TotalSize;
    return ans;
}

long WalkTwo(Folder folder, long size)
{
    long ans = long.MaxValue;
    if (folder.TotalSize > size)
    {
        ans = folder.TotalSize;
        foreach (var fol in folder.Folders.Values)
        {
            ans = Math.Min(ans, WalkTwo(fol, size));
        }
    }
    return ans;
}
public class Folder
{
    public Folder Parent;
    public Dictionary<string, Folder> Folders;
    public Dictionary<string, long> Files;

    public long TotalSize;
    public Folder(Folder parent)
    {
        Parent = parent;
        Folders = new Dictionary<string, Folder>();
        Files = new Dictionary<string, long>();
    }
}
