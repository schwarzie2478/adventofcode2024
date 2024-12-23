// See https://aka.ms/new-console-template for more information
using System.Diagnostics.CodeAnalysis;
using System.Numerics;
using Microsoft.VisualBasic;

var filename="inputdata.txt";
var input = File.ReadAllLines(filename);
var calculations = new Dictionary<long,long>();
var modulos = new Dictionary<long,long>();
var result = 0L;
var result2 = 0L;

var sequences  = new Dictionary<(long,long,long,long),long>();
var buyersequences = new List<Dictionary<(long,long,long,long),long>>();

foreach (var secret in input)
{
    var queue = new Queue<(long,long)>();
    sequences.Clear();
    var previousprice = -1;
    var nextprice =0;

    var current = long.Parse(secret);
    System.Console.WriteLine($"Parsing {current}");
    //iterate 2000 times
    for (int i = 0; i < 2000; i++)
    {
        var next = CalculateNextSecret(current);
        //System.Console.WriteLine(current);
        nextprice = next.ToString().ToCharArray().Last()-'0';
        if(previousprice >=  0)
        {
           queue.Enqueue((nextprice,nextprice-previousprice));
           if(queue.Count()>=4)
           {
                while(queue.Count()>4) queue.Dequeue();
                var sequence = ReturnSequence( queue.Select( q => q.Item2));
                var price = queue.Last().Item1;
                if(sequences.ContainsKey(sequence))
                {
                    //Monkey doesn't wait for best sequence, only first match
                    // if (sequences[sequence] < price)
                    // {
                    //     sequences.Remove(sequence);
                    //     sequences.Add(sequence,price);
                    // }
                }else
                {
                    sequences.Add(sequence,price);
                }

           }
        }
        previousprice = nextprice;
        current = next;

    }

    buyersequences.Add(sequences.ToDictionary());

    result +=current;
    
}
var flatlist = buyersequences.SelectMany( d => d.ToList());
var groups = flatlist.GroupBy ( l => l.Key);


foreach (var group in groups)
{
    var groupsales = group.Aggregate(0L, ( sum, next) => {
        sum += next.Value;
        return sum;
    });

    if(groupsales>result2)
    {
        result2 = groupsales;
    }
}


Console.WriteLine($"Result part 1: {result}");
Console.WriteLine($"Result part 2: {result2}");

long CalculateNextSecret(long secret)
{
    if(calculations.ContainsKey(secret)) return calculations[secret];
    var calc = 0L;
    //Step 1   *64 mix prune
    calc = Prune(Mix(secret,secret*64));
    //Step 2   /32 mix prune
    var calcbis = (long) Math.Floor((calc/32d));
    calc = Prune(Mix(calc,calcbis));
    //Step 3   *2024 mix prune
    calc = Prune(Mix(calc,calc*2048));

    calculations.Add(secret,calc);

    return calc;
}
long Mix(long current, long next)
{
    return current ^ next;
    
}
long Prune(long secret)
{
    // modulo 16777216
    if(modulos.ContainsKey(secret)) return modulos[secret];
    var calc = secret % 16777216L; 
    modulos.Add(secret,calc);
    return calc;
}
(long,long,long,long) ReturnSequence(IEnumerable<long> list)
{
    var item1 = list.ElementAt(0);
    var item2 = list.ElementAt(1);
    var item3 = list.ElementAt(2);
    var item4 = list.ElementAt(3);

    return (item1,item2,item3,item4);
}