// See https://aka.ms/new-console-template for more information
using System.Runtime.CompilerServices;

var filename="inputdata.txt";
var input = File.ReadAllLines(filename);

// grid = regions of same plants
var fields = new List<List<(char,(int,int))>>();
int r = 0;
int c = 0;
foreach (var line in input)
{
    foreach (var plant in line)
    {
        if(!fields.Any(f => f.Any(p => p.Item1 == plant)))
        {
            //Add new region
            fields.Add( new List<(char, (int, int))>{(plant,(c,r))});
        }else
        {
            var field = fields.Where( f => f.First().Item1 == plant).First();
            field.Add((plant,(c,r)));
        }
        c++;
    }
    r++;
}
var cost = 0;
foreach (var region in fields)
{
    var area = region.Count();
    var perimeter = region.Count()*4;
    foreach (var plant in region)
    {
        if( region.Any( f => f.Item2.Item1 == plant.Item2.Item1 && f.Item2.Item2 == plant.Item2.Item2 + 1))
        {
            perimeter-=2;
        }
        if( region.Any( f => f.Item2.Item1 == plant.Item2.Item1 +1 && f.Item2.Item2 == plant.Item2.Item2 ))
        {
            perimeter-=2;
        }
    }
    cost += area*perimeter;
    System.Console.WriteLine($"A region of {region.First().Item1} plants with price {area} * {perimeter} = {area*perimeter}");
}

var result = cost;
Console.WriteLine($"Result part 1: {result}");
