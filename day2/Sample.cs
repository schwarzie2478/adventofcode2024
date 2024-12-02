// // See https://aka.ms/new-console-template for more information

// var file = File.ReadAllLines("inputdata.txt");
// var reports = new List<List<int>>();
// foreach (var item in file)
// {
//     var values = item.Split(" ");
//     var report = new List<int>();
//     foreach (var value in values)
//     {
//         var level = Int32.Parse(value);
//         report.Add(level);
//     }
//     reports.Add(report);    
// }
// var safeReports = 0;
// Console.WriteLine($"reports: {reports.Count()}");
// foreach (var report in reports)
// {
//     foreach (var item in report)
//     {
//         System.Console.Write($"{item} ");

//     }
//     System.Console.WriteLine("");
//     int firstlevel = 0;
//     int secondlevel = 0;
//     int previouslevel = 0;

//     int lastDifference = 0;
//     bool bFirst = true;
//     bool bReportIsSafe = true;
//     bool bNeedDampening = false;
//     bool bSafetyProblemDampend = false;
//     foreach (var level in report)
//     {
//         if(bFirst) { secondlevel = level;bFirst = false;continue;}

//         previouslevel = firstlevel;
//         firstlevel = secondlevel;
//         secondlevel = level;
//         int difference = firstlevel - secondlevel;
//         if(bNeedDampening)
//         {
//             //Try the 3 combination if one is safe --> ok with dampening
//             if(IsReportSafe(previouslevel,secondlevel,lastDifference))
//             {
//                 //OK, set level correct for next check
//                 difference = previouslevel - secondlevel;
//                 bSafetyProblemDampend = true;
//                 bNeedDampening = false;
//                 lastDifference = difference;
//                 continue;
//             }

//         }
//         if(!IsReportSafe(firstlevel,secondlevel, lastDifference))
//         {
//             //Previous couple also needed dampening...
//             if(bNeedDampening == true)
//             {
//                 bReportIsSafe = false;
//                 break;
//             }
//             //Only one-time dampening allowed
//             if( bSafetyProblemDampend == true)
//             {
//                 bReportIsSafe = false;
//                 break;
//             }
//             //Do dampendCheck next time
//             bNeedDampening = true;
//         }else{
//             if(bNeedDampening == true)
//             {
//                 bSafetyProblemDampend = true;
//                 bNeedDampening = false;
//             }
//             lastDifference = difference;
//         }
 
        
//     }
//     if(bReportIsSafe){System.Console.WriteLine("Safe");safeReports++;} else{ System.Console.WriteLine("UnSafe");}
// }
// bool IsReportSafe(int firstlevel, int secondlevel,int lastDifference)
// {
//     int difference = firstlevel - secondlevel;
//     if(difference > 3 || difference < -3 || difference == 0)
//     {

//         System.Console.WriteLine($"False on too big of difference or zero difference, {firstlevel} vs { secondlevel}");
//         return false;
//     }
//     if(difference >0 && lastDifference < 0)
//     {           
//         System.Console.WriteLine($"up level change is different direction: {difference} vs {lastDifference}");
//         return false;
//     }
//     if(difference <0 && lastDifference > 0)
//     {          
//         System.Console.WriteLine($"down level change is different direction: {difference} vs {lastDifference}");
//         return false;
//     }
//     return true;
// }

// Console.WriteLine($"Safe reports: {safeReports}");
