// See https://aka.ms/new-console-template for more information
using System.Text.RegularExpressions;
using System.Xml.XPath;

// var match = Regex.Match("wrb","^wr");
// System.Console.WriteLine($"{match.Success}");
// return;
var filename="inputdata2.txt";
var input = File.ReadAllLines(filename);

var patterns = input.First().Split(", ").OrderBy( s => s.Length);
var displays = input.Skip(2);
var unmatchables = new List<string>();
var submatches = new Dictionary<string,long>();
var result = 0L;
foreach (var display in displays)
{
    System.Console.WriteLine($"Matching for {display}");
    result += CanMatchWithPatterns(display);
}



Console.WriteLine($"Result part 1: {result}");

long CanMatchWithPatterns(string display, bool starting = true)
{
    if( String.IsNullOrEmpty(display)) {

        System.Console.WriteLine("Fully Matched!");
        return 1;
    }
    if(unmatchables.Contains(display))
    { 
        //System.Console.WriteLine($"{display} is unmatchable");
        return 0;
    }
    if(submatches.ContainsKey(display)) return submatches[display];

    foreach (var pattern in patterns)
    {
        if(pattern.Length > display.Length) break;
        //System.Console.WriteLine($"Matching {display} with {pattern}");
        string patternfromstart = $"^{pattern}";
        if(Regex.Match(display,patternfromstart).Success)
        {
            //System.Console.WriteLine("Matched");
            var tail = display.Substring(pattern.Length);
            var matched = CanMatchWithPatterns(tail,false);
            if(matched>0) {
                System.Console.WriteLine($"{display} is matchable");
                if(!submatches.ContainsKey(display)) 
                { submatches.Add(display,matched);}
                else {submatches[display] += matched;}
                
            }

            if(matched==0 && !submatches.ContainsKey(tail))
            {
                //System.Console.WriteLine($" {tail} is unmatchable");
                unmatchables.Add(tail);
            }
        }
    }
    if(submatches.ContainsKey(display)) return submatches[display];
 
    return 0;
}