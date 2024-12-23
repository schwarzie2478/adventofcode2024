// See https://aka.ms/new-console-template for more information
using System.Text.RegularExpressions;

var filename = "inputdata.txt";
var input = File.ReadAllLines(filename);

var machinecount = input.Count() / 4;
var machines = new List<ClawMachine>();
for (int machine = 0; machine <= machinecount; machine++)
{
    machines.Add(new ClawMachine(input.Skip(4 * machine).Take(4)));
}
var winningMachines = machines.Where(m => m.Tokens < 10000);

var result = winningMachines.Sum(m => m.Tokens);
Console.WriteLine($"Result part 1: {result}");
class ClawMachine
{
    public (long, long) Prize { get; set; }
    public (long, long) A { get; set; }
    public (long, long) B { get; set; }
    private long tokens = 400001;
    public long Tokens
    {
        get
        {
            if (tokens > 40000)
            {
                CalculateWithBorders();
            }
            return tokens;
        }
    }
    public ClawMachine(IEnumerable<string> machineinput)
    {
        var matchButtonA = Regex.Match(machineinput.ElementAt(0), "X\\+(\\d+), Y\\+(\\d+)");
        var matchButtonB = Regex.Match(machineinput.ElementAt(1), "X\\+(\\d+), Y\\+(\\d+)");
        var matchPrize = Regex.Match(machineinput.ElementAt(2), "X=(\\d+), Y=(\\d+)");
        A = (long.Parse(matchButtonA.Groups[1].ToString()), long.Parse(matchButtonA.Groups[2].ToString()));
        B = (long.Parse(matchButtonB.Groups[1].ToString()), long.Parse(matchButtonB.Groups[2].ToString()));
        Prize = (10000000000000 + long.Parse(matchPrize.Groups[1].ToString()), 10000000000000 + long.Parse(matchPrize.Groups[2].ToString()));



    }

    private void CalculateWithBorders()
    {
        long aFrom = 10000000000000 / Math.Max(A.Item1,A.Item2);
        long aTo = 10000000000000 / Math.Min(A.Item1,A.Item2);;
        long bFrom = 10000000000000 / Math.Max(B.Item1,B.Item2);
        long bTo = 10000000000000 / Math.Min(B.Item1,B.Item2);



        for (long a = aFrom; a < aTo; a++)
        {
            //System.Console.WriteLine($"Checking a taps: diff= {Prize.Item1 - (a * A.Item1)} && {Prize.Item2} - {(a * A.Item2)} {Prize.Item2 - (a * A.Item2)}");
            if(Prize.Item1 - (a*A.Item1) < 0) break;
            if(Prize.Item2 - (a*A.Item2) < 0) break;


            for (long b = bFrom; b < bTo; b++)
            {
                //System.Console.WriteLine($"Checking b taps: diff= {Prize.Item1 - (b * B.Item1)} && {Prize.Item2 - (b * B.Item2)}");
                if(Prize.Item1 - (b*B.Item1) < 0) break;
                if(Prize.Item2 - (b*B.Item2) < 0) break;



                long resultX = a*A.Item1 + b*B.Item1;
                long resultY = a*A.Item2 + b*B.Item2;
                 if (resultX == Prize.Item1 && resultY == Prize.Item2)
                {
                    tokens = Math.Min(3 * a + b, tokens);
                    System.Console.WriteLine($"Solution found:  {a} A taps on {A.Item1},{A.Item2} and {b} b taps  on {B.Item1},{B.Item2} make for prize {Prize.Item1},{Prize.Item2} in {tokens} tokens");
                    break;
                }               
            }
        }

    }
    private void Calculate()
    {
        //For all combo's of AB smaller then prize location, tap away
        long minTaps = 10000000000000 / Math.Min(Math.Max(A.Item1, A.Item2), Math.Max(B.Item1, B.Item2));
        long aTap = 10000000000000 / Math.Max(A.Item1, A.Item2);
        long bTap = 10000000000000 / Math.Max(B.Item1, B.Item2);
        System.Console.WriteLine($"Calculating for prize {Prize.Item1},{Prize.Item2}, starting from {aTap} A taps on button A of {A.Item1},{A.Item2} {bTap} B taps on button B of {B.Item1},{B.Item2}");


        bool debugOnce = true;
        aTap = minTaps;
        System.Console.WriteLine($"{Prize.Item1 - (aTap * A.Item1)} && {Prize.Item2 - (aTap * B.Item2)}");
        while (aTap * A.Item1 < Prize.Item1)
        {
            System.Console.WriteLine("inside a taps");
            bTap = minTaps;
            System.Console.WriteLine($"{Prize.Item1 - (bTap * B.Item1)}  && {Prize.Item2 - (bTap * B.Item2)}");

            while (bTap * B.Item1 < Prize.Item1)
            {

                if (debugOnce)
                {
                    System.Console.WriteLine($"Trying {aTap} A Taps and {bTap} B Taps");
                    //debugOnce = false;
                }
                var resultX = aTap * A.Item1 + bTap * B.Item1;
                var resultY = aTap * A.Item2 + bTap * B.Item2;
                if (resultX == Prize.Item1 && resultY == Prize.Item2)
                {
                    tokens = Math.Min(3 * aTap + bTap, tokens);
                    System.Console.WriteLine($"Solution found:  {aTap} A taps on {A.Item1},{A.Item2} and {bTap} b taps  on {B.Item1},{B.Item2} make for prize {Prize.Item1},{Prize.Item2} in {tokens} tokens");
                    break;
                }
                if (resultX > Prize.Item1 || resultY > Prize.Item2) break;

                bTap++;
            }
            aTap++;
        }
    }

}