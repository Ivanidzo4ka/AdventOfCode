var Types = new Dictionary<string, int>();
Solve(false);
Solve(true);


void Solve(bool partTwo)
{
    var start = Parse("input.txt", partTwo);
    var end = new State();
    end.Floors[3].Generators.AddRange(Enumerable.Range(0, Types.Count));
    end.Floors[3].Microchips.AddRange(Enumerable.Range(0, Types.Count));
    var cut = 1L << Types.Count * 2;
    var floorMul = new long[4]
    {
        1L,
        cut,
        cut*cut,
        cut*cut*cut,
    };
    var floorCuts = new long[4]
    {
        cut,
        cut*cut,
        cut*cut*cut,
        cut*cut*cut*cut
    };
    var totalTypes = Types.Count * 2;
    var startState = start;
    var endState = end.GetHash(Types.Count);
    var visited = new HashSet<(long, short)>();
    Queue<(long, short)> states = new Queue<(long, short)>();
    Queue<(long, short)> next = new Queue<(long, short)>();

    states.Enqueue((startState, 0));
    visited.Add((startState, 0));
    var dir = new int[] { -1, 1 };
    var step=0;
    while (states.Count > 0)
    {
        step++;
        while (states.Count > 0)
        {
            var (state, floor) = states.Dequeue();
            if (state==endState) 
            {
                Console.WriteLine(step-1);
                return;
            }
            var currentFloor = state % floorCuts[floor]/ floorMul[floor];
            for (int i = 0; i < 2 * totalTypes; i++)
            {
                if ((currentFloor & (1L << i)) != 0)
                {
                    for (int j = i + 1; j <= totalTypes; j++)
                    {
                        if ((currentFloor & (1L << j)) != 0 || j == totalTypes)
                        {
                            long diff;
                            if (j == totalTypes)
                            {
                                diff = 1 << i;
                            }
                            else
                                diff = (1 << i) + (1 << j);
                            var updatedCurrentFloor = currentFloor - diff;
                            if (Valid(updatedCurrentFloor,Types.Count))
                            {
                                foreach (var d in dir)
                                {
                                    if (floor + d >= 0 && floor + d < 4)
                                    {
                                        var goingToFloor = floor + d;
                                        var nextFloor = state % floorCuts[goingToFloor]/floorMul[goingToFloor];;
                                        nextFloor += diff;
                                        if (Valid(nextFloor, Types.Count))
                                        {
                                            var nextState = state;
                                            nextState -= diff * floorMul[floor];
                                            nextState += diff * floorMul[goingToFloor];
                                            if (visited.Add((nextState, (short)goingToFloor)))
                                            {
                                                next.Enqueue((nextState, (short)goingToFloor));
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }


        }
        if (next.Count > 0){ (states,next) = (next, states); }
        
    }
}
bool Valid(long floor, int totalTypes)
{
    var hasGenerator = (floor / (1 << totalTypes)) > 0;
    if (!hasGenerator) return true;
    for (int i = 0; i < totalTypes; i++)
    {
        // has Chip but don't have generator
        if (((floor & (1L << i)) != 0) && ((floor & (1L << (i + totalTypes))) == 0))
        {
            return false;
        }
    }
    return true;
}


long Parse(string input, bool partTwo = false) 
{
    var lines = File.ReadAllLines(input);
    if (partTwo)
    {
        lines[0] += " and a elerium generator and a elerium-compatible microchip";
        lines[0] += " and a dilithium generator and a dilithium-compatible microchip";
    }
    var floor = 0;
    var state = new State();
    foreach (var line in lines)
    {
        var split = line.Split(' ');
        for (int i = 0; i < split.Length; i++)
        {
            if (split[i].StartsWith("microchip"))
            {
                var type = split[i - 1].Split("-")[0];
                if (!Types.ContainsKey(type))
                {
                    Types[type] = Types.Count;
                }
                state.Floors[floor].Microchips.Add(Types[type]);
            }
            else if (split[i].StartsWith("generator"))
            {
                var type = split[i - 1];
                if (!Types.ContainsKey(type))
                {
                    Types[type] = Types.Count;
                }
                state.Floors[floor].Generators.Add(Types[type]);
            }
        }

        floor++;
    }
    return state.GetHash(Types.Count);
}

class State
{
    public IReadOnlyList<Floor> Floors { get; }

    public class Floor
    {
        public List<int> Microchips { get; } = new List<int>();
        public List<int> Generators { get; } = new List<int>();
    }

    public State()
    {
        Floors = new List<Floor>
        {
            new Floor(),
            new Floor(),
            new Floor(),
            new Floor()
        };
    }

    public long GetHash(int totalTypes)
    {
        var value = 0L;
        var size = 1L << 2 * totalTypes;
        var shift = 1L;
        for (int i = 0; i < Floors.Count; i++)
        {
            var floorValue = 0L;
            foreach (var chip in Floors[i].Microchips)
            {
                floorValue += 1 << chip;
            }
            foreach (var generator in Floors[i].Generators)
            {
                floorValue += 1 << generator << totalTypes;
            }
            value += floorValue * shift;
            shift *= size;
        }
        return value;
    }
}