// See https://aka.ms/new-console-template for more information
var filename = "inputdata2.txt";
var input = File.ReadAllLines(filename);
var result = 0;
var result2 = 0;
var keys = new List<(int, int, int, int, int)>();
var locks = new List<(int, int, int, int, int)>();


var count = 0;
do
{
    var next = input.Skip(count).Take(7);
    var seq = new List<int>{-1,-1,-1,-1,-1};
    var islock= false;
    if (next.First().All(c => c == '#'))
    {
        islock= true;
    }
    foreach(var line in next)
    {
        var lineseq = line.ToCharArray().Select(c => (c=='#')?1:0).ToList();
        seq = seq.Zip(lineseq, (a,b) => a+b).ToList();
    }
    if(islock)
    {
         locks.Add(SequenceToTuple(seq));

    }else
    {
         keys.Add(SequenceToTuple(seq));
    }

    count += 8;

} while (count < input.Count());


foreach (var loc in locks)
{
    foreach (var key in keys)
    {
        if(Fits(loc,key))result++;
    }
}

Console.WriteLine($"Result part 1: {result}");
Console.WriteLine($"Result part 2: {result2}");

(int,int,int,int,int) SequenceToTuple(List<int> list)
{
    return ( list[0],list[1],list[2],list[3],list[4]);
}
bool Fits( (int,int,int,int,int) key, (int,int,int,int,int) loc)
{
    if( key.Item1 + loc.Item1 > 5) return false;
    if( key.Item2 + loc.Item2 > 5) return false;
    if( key.Item3 + loc.Item3 > 5) return false;
    if( key.Item4 + loc.Item4 > 5) return false;
    if( key.Item5 + loc.Item5 > 5) return false;
    return true;
}