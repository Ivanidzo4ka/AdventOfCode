// See https://aka.ms/new-console-template for more information

Step(true);
Step(false);
void Step(bool one)
{
    LinkedList<char>[] stacks = null;
    var readStacks = true;
    foreach (var line in File.ReadAllLines("input.txt"))
    {

        if (string.IsNullOrEmpty(line))
        {
            readStacks = false;
            continue;
        }
        if (readStacks)
        {
            if (stacks == null)
            {
                stacks = new LinkedList<char>[(line.Length + 1) / 4];
            }
            for (int i = 0; i < (line.Length + 1) / 4; i++)
            {
                if (line[1] == '1')
                {
                    continue;
                }
                if (line[i * 4 + 1] != ' ')
                {
                    if (stacks[i] == null)
                        stacks[i] = new LinkedList<char>();
                    stacks[i].AddFirst(line[i * 4 + 1]);
                }
            }
        }
        else
        {
            var start = 5;
            var end = line.IndexOf(' ', start);
            var amount = int.Parse(line.Substring(start, end - start));
            start = end + 6;
            end = line.IndexOf(' ', start);
            var from = int.Parse(line.Substring(start, end - start)) - 1;
            start = end + 4;
            end = line.Length;
            var to = int.Parse(line.Substring(start, end - start)) - 1;

            if (!one)
            {

                var stack = new Stack<char>();
                for (int i = 0; i < amount; i++)
                {
                    var first = stacks[from].Last();
                    stack.Push(first);
                    stacks[from].RemoveLast();
                }
                while (stack.Count != 0)
                    stacks[to].AddLast(stack.Pop());
            }
            else
            {
                for (int i = 0; i < amount; i++)
                {
                    var first = stacks[from].Last();
                    stacks[to].AddLast(first);
                    stacks[from].RemoveLast();
                }
            }
        }
    }

    foreach (var stack in stacks)
    {
        Console.Write(stack.Last());
    }
    Console.WriteLine();
}