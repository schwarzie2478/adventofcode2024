// See https://aka.ms/new-console-template for more information

var input = new List<int>();
var input2 =new List<int>();

var file = File.ReadAllLines("inputdata.txt");
var file2 = File.ReadAllLines("inputdata2.txt");
foreach (var str in file)  
{
    var id = 0;
    var result = int.TryParse(str, out id);
    Console.WriteLine($"Adding {id}");
    
    input.Add(id);
}
foreach (var str in file2)  
{
    var id = 0;
    var result = int.TryParse(str, out id);
    Console.WriteLine($"Adding {id}");
    input2.Add(id);
}

input.Sort();
input2.Sort();
var dist = 0;
foreach (var c in Enumerable.Range(0,input.Count))
{
    dist += Math.Abs(input[c]-input2[c]);
    Console.WriteLine($"With {input[c]} and {input2[c]} the distance is now {dist}");
}
var grouping = input2.GroupBy( p =>p).ToList();
var similarity = 0;
foreach (var item in input)
{
    if(grouping.Any(p => p.Key == item))
    {
        similarity += grouping.First(p => p.Key== item).Count() * item;
        Console.WriteLine($"Adding  {item} with count { grouping.First(p => p.Key== item).Count()} for similartiy of {similarity}");
    }
}
Console.WriteLine($"Distance = {dist}");
Console.WriteLine($"Similarity = {similarity}");

