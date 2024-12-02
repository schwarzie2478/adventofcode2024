// See https://aka.ms/new-console-template for more information

using System.Diagnostics.SymbolStore;
using System.Runtime.InteropServices;

var file = File.ReadAllLines("inputdata2.txt");
var reports = new List<List<int>>();
foreach (var item in file)
{
    var values = item.Split(" ");
    var report = new List<int>();
    foreach (var value in values)
    {
        var level = Int32.Parse(value);
        report.Add(level);
    }
    reports.Add(report);    
}
var safeReports = 0;
Console.WriteLine($"reports: {reports.Count()}");
foreach (var report in reports)
{
    bool bReportIsSafe = false;

    List<int> test = new List<int>();
    test.AddRange(report);
    if(CheckReport(test))
    {
        bReportIsSafe = true;
    }else{
        //Try removing one item
        for (int i = 0; i < report.Count(); i++)
        {
            test.Clear();
            test.AddRange(report);
            test.RemoveAt(i);
            if(CheckReport(test))
            {
                bReportIsSafe = true;
                break;
            }
        }
    }
    if(bReportIsSafe){System.Console.WriteLine("Safe");safeReports++;} else{ System.Console.WriteLine("UnSafe");}
}

bool CheckReport(List<int> report)
{
    var differences = new List<int>();
    var previouslevel= 0;
    foreach (var level in report)
    {
        if(previouslevel==0){
            previouslevel = level;
            continue;
        }
        differences.Add(previouslevel-level);
        previouslevel = level;
    }
    if(differences.Any(p => Math.Abs(p) > 3 || p == 0))
    {
        System.Console.WriteLine("Too high values");
        return false;
    }
    var signs = differences.Select(p => Math.Sign(p));
    if(signs.Distinct().Count() != 1)
    {
        System.Console.WriteLine("Signs don't match");
         return false;
    }
    return true;
}
Console.WriteLine($"Safe reports: {safeReports}");
