// See https://aka.ms/new-console-template for more information
using System.Data;

var filename = "inputdata.txt";
var input = File.ReadAllLines(filename);
var result = 0;
var result2 = "";

var couples = input.Select(l => l.Split('-').Order()).Select(p => (p.ElementAt(0), p.ElementAt(1)));
var interconnects = new List<(string, string, string)>();
foreach (var couple in couples)
{
    foreach (var other in couples.Where(c => c.Item1 == couple.Item1 && c.Item2 != couple.Item2))
    {
        if (couples.Contains((couple.Item2, other.Item2)))
        {
            var sequence = (couple.Item1, couple.Item2, other.Item2);
            if (!interconnects.Contains(sequence)) interconnects.Add(sequence);
        }
    }
}
var desiredinterconnects = new List<(string, string, string)>();
foreach (var inter in interconnects)
{
    if (inter.Item3.StartsWith('t') || inter.Item2.StartsWith('t') || inter.Item1.StartsWith('t'))
    {
        desiredinterconnects.Add(inter);
    }
}
result = desiredinterconnects.Count();

var connections = new List<List<string>>();
foreach (var couple in couples)
{



}


Console.WriteLine($"Result part 1: {result}");
Console.WriteLine($"Result part 2: {result2}");
