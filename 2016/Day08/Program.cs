using System.Text;
PartOne();
void PartOne()
{
    var lines = File.ReadAllLines("input.txt");
    var n=6;
    var m=50;
    var field = new bool[n][];
    for(int i=0; i<n; i++)
        field[i]= new bool[m];

    foreach(var inst in lines)
    {
        if (inst.StartsWith("rect "))
        {
            var dim = inst.Substring(5).Split("x").Select(x=>int.Parse(x)).ToArray();
            for (int i=0; i< dim[1]; i++)
                for (int j=0; j< dim[0]; j++)
                    field[i][j]=true;
        }
        else if (inst.StartsWith("rotate row y="))
        {
            var dim = inst.Substring(13).Split(" by ").Select(x=>int.Parse(x)).ToArray();
            var newOne = new bool[m];
            for(int i=0; i< m; i++)
                newOne[(i+dim[1])%m]= field[dim[0]][i];
            for (int i=0; i<m; i++)
                field[dim[0]][i] = newOne[i];
        }
        else {
            var dim = inst.Substring(16).Split(" by ").Select(x=>int.Parse(x)).ToArray();
            var newOne = new bool[m];
            for(int i=0; i< n; i++)
                newOne[(i+dim[1])%n]= field[i][dim[0]];
            for (int i=0; i<n; i++)
                field[i][dim[0]] = newOne[i];
        }
       // Print(field);
    }
    Print(field);
    var ans=0;
    for(int i=0; i< n; i++)
    for (int j=0; j<m; j++)
        if (field[i][j]) ans++;
    Console.WriteLine(ans);
}
void Print(bool[][] board)
{
    for(int i=0; i< board.Length;i++)
    {
        var sb= new StringBuilder();
        for(int j=0; j<board[0].Length; j++)
            sb.Append(board[i][j]?'#':'.');
        Console.WriteLine(sb.ToString());
        sb.Clear();
    }
    Console.WriteLine();
}