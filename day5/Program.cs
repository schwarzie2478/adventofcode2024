// See https://aka.ms/new-console-template for more information
var result = 0;
var input = File.ReadAllLines("inputdata2.txt");

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
