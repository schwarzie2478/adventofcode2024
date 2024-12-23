// See https://aka.ms/new-console-template for more information
var filename = "inputdata.txt";
var input = File.ReadAllLines(filename);
var dimension = input.Length;
var grid = new int[dimension, dimension];

var x = 0;
var y = 0;
var trailheads = new List<(int, int)>();

foreach (var line in input)
{
    y=0;
    foreach (var step in line)
    {
        if (step == '0')
            trailheads.Add((x, y));
        grid[x,y] = step - '0';

        System.Console.WriteLine($"Grid {x}{y} height {step}");

        y++;
    }
    x++;
}

System.Console.WriteLine($"starts: {trailheads.Count()}");

var trailends = new List<(int, int)>();
foreach (var start in trailheads)
{
    System.Console.WriteLine($"Looking for trailends starting from {start.Item1}{start.Item2}");
    trailends.AddRange(LookForTrailEnds(0, start));
}


var result = trailends.Distinct().Count();
Console.WriteLine($"Result part 1: {result}");

IEnumerable<(int, int)> LookForTrailEnds(int height, (int, int) start)
{
    System.Console.WriteLine($"Looking for trailends from height {height} from {start.Item1}{start.Item2}");
    if (grid[start.Item1, start.Item2] == 9)
    {
        System.Console.WriteLine($"Trail end found: {start.Item1}{start.Item2}");
        return new List<(int, int)> { start };
    }

    var trailends = new List<(int, int)>();

    if (start.Item1 > 1 && grid[start.Item1, start.Item2] == grid[start.Item1 - 1, start.Item2] - 1)
    {
        System.Console.WriteLine("Looking higher");
        var nextheight = grid[start.Item1 - 1, start.Item2];
        trailends.AddRange(LookForTrailEnds(nextheight, (start.Item1 - 1, start.Item2)));
    }

    if (start.Item2 > 1 && grid[start.Item1, start.Item2] == grid[start.Item1, start.Item2 - 1] - 1)
    {
        System.Console.WriteLine("Looking lower");

        var nextheight = grid[start.Item1, start.Item2 - 1];
        trailends.AddRange(LookForTrailEnds(nextheight, (start.Item1, start.Item2 + 1)));
    }

    if (start.Item1 < dimension - 1 && grid[start.Item1, start.Item2] == grid[start.Item1 = 1, start.Item2] - 1)
    {
        System.Console.WriteLine("Looking left");

        var nextheight = grid[start.Item1 + 1, start.Item2];
        trailends.AddRange(LookForTrailEnds(nextheight, (start.Item1 + 1, start.Item2)));
    }
    
    if (start.Item2 < dimension - 1 && grid[start.Item1, start.Item2] == grid[start.Item1, start.Item2 + 1] - 1)
    {
        System.Console.WriteLine("Looking right");
        var nextheight = grid[start.Item1, start.Item2 + 1];
        trailends.AddRange(LookForTrailEnds(nextheight, (start.Item1, start.Item2 + 1)));
    }

    return trailends;
}