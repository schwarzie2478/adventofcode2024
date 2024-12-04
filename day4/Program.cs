// See https://aka.ms/new-console-template for more information
using System.Text.RegularExpressions;

var input = File.ReadAllLines("inputdata2.txt");
var dimension = input.Length;
var transpose = new List<string>();
var diagonaluptop = new List<string>();
var diagonalupbottom = new List<string>();
var diagonaldowntop = new List<string>();
var diagonaldownbottom = new List<string>();

for (int i = 0; i < dimension; i++)
{
    transpose.Add("");
    diagonaluptop.Add("");
    diagonalupbottom.Add("");
    diagonaldowntop.Add("");
    diagonaldownbottom.Add("");
}

var count = 0;

//horizontal
System.Console.WriteLine("Search Horizontal");
foreach (var line in input)
{
    count = CountMatches(count, line);

    //Transpose for vertical search
    var position = 0;
    foreach (var ch in line)
    {
        transpose[position] += ch;
        position++;
    }
}
//vertical
System.Console.WriteLine("Search Vertical");
foreach (var line in transpose)
{
    count = CountMatches(count, line);
}


//diagonals
var current_line=0;
for (int i = 4; i <= dimension; i++)
{   
    for (int j = 0; j < i; j++)
    {
        diagonaluptop[current_line] += input[i -1  - j][j];
        diagonalupbottom[current_line] +=input[dimension -1 -j][dimension - i +j];
        System.Console.WriteLine($"up   line {diagonaluptop[current_line]}\ndown line { diagonalupbottom[current_line]}");

        diagonaldowntop[current_line] += input[i -1  - j][dimension -1 - j];
        diagonaldownbottom[current_line] +=input[dimension -i +j][j];
        System.Console.WriteLine($"up   line {diagonaldowntop[current_line]}\ndown line { diagonaldownbottom[current_line]}");
    }
    current_line++;
}
diagonalupbottom[current_line-1] = "";
diagonaldownbottom[current_line-1] ="";
foreach (var line in diagonaluptop)
{
    count = CountMatches(count,line);
}
foreach (var line in diagonalupbottom)
{
    count = CountMatches(count,line);
}
foreach (var line in diagonaldowntop)
{
    count = CountMatches(count,line);
}
foreach (var line in diagonaldownbottom)
{
    count = CountMatches(count,line);
}


Console.WriteLine($"Part 1 Result : {count}");

//Part 2 result check on MAS in cross
// for all A, check cross for pair of MS
var count2 = 0;
for (int i = 1; i < dimension-1; i++)
{
    for (int j = 1; j < dimension -1; j++)
    {
        //Check Cross -> count

        if(input[i][j] =='A')
        {
            if(input[i-1][j-1] =='M')
            {
                if(input[i+1][j+1] !='S'){continue;}

            }else if (input[i-1][j-1] =='S')
            {
                if(input[i+1][j+1] !='M'){continue;} 
            }else { continue;}

            if(input[i-1][j+1]=='S' && input[i+1][j-1]=='M'){count2++;continue;}
            if(input[i+1][j-1]=='S' && input[i-1][j+1]=='M'){count2++;continue;} 
        }
    }
}
Console.WriteLine($"Part 2 Result : {count2}");

static int CountMatches(int count, string line)
{
    var matches = Regex.Matches(line, "XMAS");
    System.Console.WriteLine($"{matches.Count} XMAS found. in line {line}");
    count += matches.Count;
    matches = Regex.Matches(line, "SAMX");
    System.Console.WriteLine($"{matches.Count} SAMX found. in line {line}");
    count += matches.Count;
    return count;
}