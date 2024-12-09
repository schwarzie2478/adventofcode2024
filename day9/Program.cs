// See https://aka.ms/new-console-template for more information
var filename="inputdata.txt";
var input = File.ReadAllLines(filename);

var compressed = input.First().Select( c => c);
var uncompressed = "";

var result = 0;
Console.WriteLine($"Result part 1: {result}");
