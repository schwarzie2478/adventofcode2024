// See https://aka.ms/new-console-template for more information
using System.Data;
using System.Text.RegularExpressions;

var result = 0;
var input = File.ReadAllLines("inputdata.txt");
var rules2 = input
    .Where(s => s.Contains('|'))
    .Select(p => p.Split('|')
                .Select(i => int.Parse(i)))
    .GroupBy( c => c.Last())
    ;
foreach (var group  in rules2.First())
{
    System.Console.WriteLine($"{group.First()}");
}
    var updates2 = input
    .Where(s => s.Contains(','))
    .Select(p => p.Split(',').Select( i => int.Parse(i)));

System.Console.WriteLine($"rule Count: {rules2.Count()}");
System.Console.WriteLine($"update Count: {updates2.Count()}");

var result3= updates2.Aggregate(0, (sum, update) => sum+((
    GoodUpdate(update,rules2)
    )?update.ElementAt(update.Count()/2):0));

Console.WriteLine($"Part 1 Result : {result3}");
return;
bool GoodUpdate(IEnumerable<int>update,IEnumerable<IGrouping<int,IEnumerable<int>>> rules)
{
    System.Console.WriteLine("checking update");
    var previouspages = new List<int>();
    foreach (var page in update)
    {
        //System.Console.WriteLine($"Checking if {page} ");

        {           
            foreach (var checkpage in previouspages)
            {
                System.Console.WriteLine($"Checking if {page} must be before {checkpage}");
                if(rules.Any(g => g.Key==checkpage && g.Any(pp => pp.Contains( page)))) {System.Console.WriteLine("no"); return false;}
            }
            previouspages.Add(page);                
        }
    } 
    return true;
}
//Read rules
//Check updates and stored middle pages
bool rulesdone = false;
var rules = new Dictionary<int,List<int>>();
var badupdates = new List<List<int>>();

foreach (var line in input)
{
    if(line =="") { rulesdone = true;continue;}
    if(!rulesdone)
    {
        //store rule;
        var rule = line.Split('|').Select(p => int.Parse(p)).ToList();
        if(!rules.Keys.Contains(rule[1]))
        {
            rules.Add(rule[1],[]);
        }
        rules[rule[1]].Add(rule[0]);  
    }else{
        //check update
        System.Console.WriteLine($"Check update {line}");
        var badupdate = false;
        var update = line.Split(",").Select(p => int.Parse(p)).ToList();
        var previouspages = new List<int>();

        foreach (var page in update)
        {           
            foreach (var checkpage in previouspages)
            {
                if(!rules.Keys.Contains(checkpage)){continue;}
                var badpages = rules[checkpage]; 
                if(badpages.Contains(page)){System.Console.WriteLine($"page {page} should not come after {checkpage}");badupdates.Add(update); badupdate= true; break;}
            }
            if(badupdate) { break;}
            previouspages.Add(page);
        }
        if(badupdate) { continue;}

        //add middle page
        System.Console.WriteLine($"Adding page {update[update.Count/2]}");
        result += update[update.Count/2];
    }
}
//Correct and sum badupdates
var result2 = 0;
foreach (var update in badupdates)
{
    System.Console.WriteLine($"Correcting {String.Concat(update)}");
    //Try to correct
    var correct = new List<int>();
    foreach (var page in update)
    {
        if(correct.Count==0){correct.Add(page); continue;}
        var pageadded= false;

        for (int i = 0; i < correct.Count; i++)
        {
            if(!rules.Keys.Contains(correct[i])){continue;}
            if(rules[correct[i]].Contains(page)){correct.Insert(i,page);pageadded = true;break;}
        }
        if(pageadded==false){correct.Add(page);}
    }
    System.Console.WriteLine($"Adding page {correct[correct.Count/2]}");
    result2 += correct[correct.Count/2];
    
}

Console.WriteLine($"Part 1 Result : {result}");
Console.WriteLine($"Part 2 Result : {result2}");
