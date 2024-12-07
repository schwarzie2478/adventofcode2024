using System.Numerics;

var fileName = "inputdata2.txt";
var input = File.ReadAllLines(fileName);
BigInteger result = 0;

var data = input.Select(p => p.Split(':'));
var testvalues = data.Select(p => p.First()).Select(s => BigInteger.Parse(s));
var operators = data.Select( p => p.Skip(1).Single().Trim().Split(' '));
var operatornumbers = operators.Select( p => p.Select( s => BigInteger.Parse(s)).Reverse());

var measurements = testvalues.Zip(operatornumbers);

foreach (var measurement in measurements)
{
    System.Console.WriteLine($"Measurement: testvalue={measurement.First}, values = {string.Join(",",measurement.Second)}");
    var testvalue = measurement.First;
    var ops = measurement.Second;
    var combinations = Fit(ops.First(),ops.Skip(1));
    //System.Console.WriteLine($"Combiniations: {string.Join(",",combinations)}");
    if(combinations.Any(i => i== testvalue))
    {
        System.Console.WriteLine($"Match found for {testvalue}");
        result+=testvalue;
    }
}


Console.WriteLine($"Result 1: {result}");
IEnumerable<BigInteger> Fit(BigInteger firstOperand,IEnumerable<BigInteger> rest)
{

    if(rest.Count()==1)
    {
        //End of list
        //System.Console.WriteLine($"Combining {firstOperand} and {rest.First()}");
        return [firstOperand+rest.First(),firstOperand*rest.First(),BigInteger.Parse($"{rest.First()}{firstOperand}")];
    }else{
        //System.Console.WriteLine($"From {firstOperand} and {string.Join(",",rest)}");
        var combinations = Fit(rest.First(),rest.Skip(1));
        //System.Console.WriteLine($"Combining {firstOperand} and {string.Join(",",combinations)}");
        List<BigInteger> newcombinations = [];
        foreach (var combi in combinations)
        {
            newcombinations.Add(combi+firstOperand);
            newcombinations.Add(combi*firstOperand);
            newcombinations.Add(BigInteger.Parse($"{combi}{firstOperand}"));
        }
        return newcombinations;

    }
}
