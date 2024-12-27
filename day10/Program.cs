// See https://aka.ms/new-console-template for more information
var filename = "inputdata2.txt";
var input = File.ReadAllLines(filename);
var dimension = input.Length+2;
var grid = new int[dimension, dimension];
for (int i = 0; i < dimension; i++)
{
    for (int j = 0; j < dimension; j++)
    {
        grid[j,i] = -1;
    }
}

var x = 1;
var y = 1;
var trailheads = new List<(int, int)>();

foreach (var line in input)
{
    y=1;
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
    trailends.AddRange(Climb(start));
}


var result = trailends.Count();
Console.WriteLine($"Result part 1: {result}");

IEnumerable<(int,int)> Climb((int,int) current)
{
    if(grid[current.Item1,current.Item2]==9)
    {
        return new[] {current};
    }

    int currentstep = grid[current.Item1,current.Item2];
    int next = currentstep+1;
    var fromhere = new List<(int,int)>();
    //fromhere.Add(current);
    if( grid[current.Item1 + 1, current.Item2] == next)
    {
        fromhere.AddRange(Climb( (current.Item1 + 1, current.Item2)));
    }
     if( grid[current.Item1 - 1, current.Item2] == next)
    {
        fromhere.AddRange(Climb( (current.Item1 - 1, current.Item2)));
    }
     if( grid[current.Item1, current.Item2 + 1] == next)
    {
        fromhere.AddRange(Climb( (current.Item1, current.Item2 + 1)));
    }
     if( grid[current.Item1 , current.Item2- 1] == next)
    {
        fromhere.AddRange(Climb( (current.Item1, current.Item2 - 1)));
    }
    
    return fromhere;
}
