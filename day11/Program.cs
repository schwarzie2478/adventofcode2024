// See https://aka.ms/new-console-template for more information
using System.Numerics;
using System.Reflection.Metadata.Ecma335;

var filename = "inputdata.txt";

var input = new List<BigInteger> { 125, 17 };
var input2 = new List<BigInteger> { 0, 89741, 316108, 7641, 756, 9, 7832357, 91 };
var blinks = 75;
var blinkpairs = new Dictionary<BigInteger,BigInteger>{  {125,1}, {17,1}};
var blinkpairs2 = new Dictionary<BigInteger,BigInteger>{ {0,1}, {89741,1}, {316108,1}, {7641,1}, {756,1}, {9,1}, {7832357,1}, {91,1}};
var blinked = new Dictionary<BigInteger,BigInteger>();
var output = blinkpairs2.ToArray();
for (int i = 0; i < blinks; i++)
{
    
    blinked.Clear();
    foreach (var stone in output)
    {
        var stones = TransformStone(stone);
        foreach (var newstone in stones)
        {
            if(blinked.ContainsKey(newstone.Key))
            {
                blinked[newstone.Key] += newstone.Value;
            }else
            {
                blinked.Add(newstone.Key,newstone.Value);
            }
        }
    }
    output = blinked.ToArray();
    System.Console.WriteLine($"Different stones: {output.Length}");

}


var result = output.Aggregate((BigInteger)0, (sum, stone) => { sum += stone.Value; return sum;});
Console.WriteLine($"Result part 1: {result}");
Dictionary<BigInteger,BigInteger> TransformStone(KeyValuePair<BigInteger,BigInteger> stone)
{
    if (stone.Key == 0) return new Dictionary<BigInteger,BigInteger> {{1,stone.Value} };
    if (HasEvenDigits(stone.Key))
    {
        var str = stone.Key.ToString();
        var half = str.Length / 2;
        var firstNumber = BigInteger.Parse(str.Substring(0, half));
        var secondNumber = BigInteger.Parse(str.Substring( half));
        if(firstNumber==secondNumber) return new Dictionary<BigInteger,BigInteger> {{firstNumber,stone.Value*2}};

        return new Dictionary<BigInteger,BigInteger> { {firstNumber,stone.Value}, {secondNumber,stone.Value} };
    }
    return new Dictionary<BigInteger,BigInteger> { {stone.Key * 2024,stone.Value} };

}


static bool HasEvenDigits(BigInteger stone)
{
    if (stone.ToString().Length % 2 == 0) return true;
    return false;
}