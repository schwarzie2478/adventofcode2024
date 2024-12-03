// See https://aka.ms/new-console-template for more information
using System.Net;
using System.Text.RegularExpressions;

var do_pattern= @"(.*)(don't\(\)|do\(\))(.*)";

var pattern =@"mul\(\d{1,3},\d{1,3}\)";
var pattern2 = @"mul\((\d{1,3}),(\d{1,3})\)";
var input = File.ReadAllLines("inputdata2.txt");

var result = 0;
var enabled = true;//Stays valid across lines!!!
var lastdodont= "do()";
var savedlastdodont= false;

foreach (var line in input)
{
    System.Console.WriteLine($"Testing {line}");

    var split_lines = Regex.Match(lastdodont + line,do_pattern);
    foreach (var group in split_lines.Groups)
    {
        //System.Console.WriteLine($"Captured: {group.ToString().LimitLength(25)}");
    }

    while(split_lines.Groups.Count == 4)
    {
        if(split_lines.Groups[2].ToString() == "don't()"){enabled = false;System.Console.WriteLine($"enabled:{enabled}");} 
        if(split_lines.Groups[2].ToString() == "do()") {enabled = true;System.Console.WriteLine($"enabled:{enabled}");}
        if(savedlastdodont==false)
       {
         lastdodont = (enabled)?"do()":"don't()";
         savedlastdodont=true;
       } 

        if(enabled){
        
            //System.Console.WriteLine($"Matching on {split_lines.Groups[3].ToString().LimitLength(25)}");
            var matches = Regex.Matches( split_lines.Groups[3].ToString(), pattern);
            foreach (var match in matches)
            {
                var multiplication = Regex.Match(match.ToString(),pattern2);
                var calculation = 1;
                var bSkipGroup0 = true;
                foreach (var group in multiplication.Groups)
                {
                    if (bSkipGroup0){bSkipGroup0 = false;continue;}
                    //System.Console.Write($"{group.ToString().ToString()}, ");
                    calculation *= int.Parse(group.ToString());

                }

                result += calculation;
                //System.Console.WriteLine($" Result is now {result}");
            }

        }
        if(split_lines.Groups[2].ToString()==""){System.Console.WriteLine("Breaking on empty do/don't"); break;}
        //System.Console.WriteLine($"Continueing on string {split_lines.Groups[1].ToString().LimitLength(25)}");
        split_lines = Regex.Match( split_lines.Groups[1].ToString(), do_pattern);

    }
    savedlastdodont = false;

    System.Console.WriteLine($"Result is now {result}");

    
}

Console.WriteLine($"The sum is {result}");
public static class StringExtensions
{
    public static string LimitLength(this string source, int maxLength)
    {
        if (source.Length <= maxLength)
        {
        return source;
        }

        return source.Substring(0, maxLength);
    }
}
