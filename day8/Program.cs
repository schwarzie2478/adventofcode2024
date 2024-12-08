// See https://aka.ms/new-console-template for more information
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

var inputFilename = "inputdata2.txt";
var input = File.ReadAllLines(inputFilename);
var anti = new List<(int,int)>();

var frequencies = new List<(char,(int,int))>();
for (int i = 0; i < input.Count(); i++)
{
    for (int j = 0; j < input[i].Length; j++)
    {
        var f = input[i].ElementAt(j);
        if(f !='.') { frequencies.Add((f,(i,j)));}
    }
}
var groups = frequencies.GroupBy( p => p.Item1);
var antipods = new List<(int,int)>();

foreach (var group in groups)
{
    //combine each member
    var pairs = CreatePairs(group);
    //Add antipod
    antipods.AddRange(CreateAntipods(pairs)) ;
}
//Count all antipods inbounds
var result = antipods.Distinct().Where( a => a.Item1 >=0 && a.Item1 < input.First().Count() && a.Item2 >=0 && a.Item2 < input.Count()).Count();

Console.WriteLine($"Result Part 1: {result}");

List<((int,int),(int,int))> CreatePairs(IEnumerable<(char,(int,int))> members)
{
    if(members.Count()==0) return [];

    var pairs = new List<((int,int),(int,int))>();
    var first = members.First();
    var rest = members.Skip(1);
    pairs.AddRange(CreatePairs(rest));
    foreach (var second in rest)
    {
        pairs.Add((first.Item2,second.Item2));
    }
    return pairs;
}
List<(int,int)> CreateAntipods(List<((int,int),(int,int))> pairs)
{
    var antipods =  new List<(int,int)>();
    foreach (var pair in pairs)
    {
        /*
        if(x1 < x2)
        if(y1 < y2)

        if(x1 >x2)
        if(y1 <y2)

        if(x1 == x2)

        if(y1 == y2)
        */
        var dx = Math.Max(pair.Item1.Item1,pair.Item2.Item1)-Math.Min(pair.Item1.Item1,pair.Item2.Item1);
        dx *=(pair.Item1.Item1 < pair.Item2.Item1)?1:-1;
        var dy =  pair.Item2.Item2 - pair.Item1.Item2;

        for (int i = 0; i < input.Length/Math.Max(dx,dy); i++)
        {
            //Add points before
            antipods.Add((pair.Item1.Item1-dx*i,pair.Item1.Item2-dy*i));
            antipods.Add((pair.Item2.Item1+dx*i,pair.Item2.Item2+dy*i));

        }

        antipods.AddRange( [(pair.Item1.Item1-dx,pair.Item1.Item2-dy),(pair.Item2.Item1+dx,pair.Item2.Item2+dy)]);
    }
    return antipods;
    return [];
}


