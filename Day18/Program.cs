// See https://aka.ms/new-console-template for more information
var filename="inputdata.txt";
var input = File.ReadAllLines(filename);

var crashpoints = input.Take(12).Select( p => p.Split(",")).Select( sa => (Int32.Parse( sa.First()),Int32.Parse(sa.Skip(1).First())));


var result = 0;
Console.WriteLine($"Result part 1: {result}");
