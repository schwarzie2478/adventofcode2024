// See https://aka.ms/new-console-template for more information
using System.Security.Cryptography.X509Certificates;
using System.Security.Permissions;
using System.Text;

var filename="inputdata2.txt";
var input = File.ReadAllLines(filename);
var result = 0;

char N = '^';
char S = 'V';
char W = '<';
char E = '>';

char[,] numpad = {{'7','8','9'},{'4','5','6'},{'1','2','3'},{'x','0','A'}, };
char[,] dirpad = {{'x','^','A'},{'<','V','>'}};

(int,int) startingposition = (3,2);
(int,int) startingdirection= (0,2);

// var test = ReturnSequenceFor(dirpad, "^A^^<<A>>AVVVA");
// System.Console.WriteLine(test);
// return;
foreach (var code in input)
{
    var seq1 = ReturnSequenceFor( numpad,code,true);
    System.Console.WriteLine($"First  sequence becomes {seq1}");
    var seq2 = ReturnSequenceFor( dirpad,seq1);
    System.Console.WriteLine($"Second sequence becomes {seq2}");
    var seq3 = ReturnSequenceFor( dirpad,seq2,false,true);
    System.Console.WriteLine($"Third  sequence becomes {seq3}");
    var complexitymultiplier = Int32.Parse( code.Substring(0,code.Length-1));

    System.Console.WriteLine($"Complexity is calulated with {seq3.Length} * {complexitymultiplier}");

    result+=(seq3.Length*complexitymultiplier);
}

Console.WriteLine($"Result part 1: {result}");
string ReturnSequenceFor(char[,] pad, string code , bool keypad =false,bool human = false)
{

    var currentposition = (keypad)?startingposition: startingdirection;
    
    var wholesequence = new StringBuilder();

    foreach (var token in code.ToArray())
    {
        //Determine new position
        var newposition = FindPosition(pad,token);
        var sequence = MoveToPosition(currentposition,newposition,keypad,human);
        wholesequence.Append(sequence);
        currentposition = newposition;
    }

    return wholesequence.ToString();
}
(int,int) FindPosition(char[,] pad, char target)
{
    //System.Console.WriteLine($"Searching for {target}");
    for (int i = 0; i < pad.GetLength(0); i++)
    {
        for (int j = 0; j < pad.GetLength(1); j++)
        {
            if(pad[i,j] == target) 
            {
                //System.Console.WriteLine($"Found at ({i},{j})");
                return (i,j);
            }
        }
    }
    return (-1,-1);
}
string MoveToPosition((int,int)current, (int,int) next, bool keypad = false,bool human = false)
{
    var seq = new StringBuilder();
    var seqh = new StringBuilder();
    var seqv = new StringBuilder();
    //Vertical
    int y = current.Item1 - next.Item1;
    if( y >=0)
    {
        for (int i = 0; i < y; i++)
        {
            seqv.Append(N);
        }
    }else
    {
        y = -y;
        for (int i = 0; i < y; i++)
        {
            seqv.Append(S);
        }
    }    
    //Horizontal
    int x = current.Item2 - next.Item2;
    if( x >=0)
    {
        for (int i = 0; i < x; i++)
        {
            seqh.Append(W);
        }
    }else
    {
        x = -x;
        for (int i = 0; i < x; i++)
        {
            seqh.Append(E);
        }
    }

    if(keypad)
    {
        if (current.Item1 == 3 && seqv.Length > 0 && seqv[0] == S)
        {
            seq.Append(seqh);
            seq.Append(seqv);

        }
        else if (current.Item2==0 && seqv.Length > 0 && seqv[0]==S)
        {
            seq.Append(seqv);
            seq.Append(seqh);
           
        }
        else
        {
            seq.Append(seqv);            
            seq.Append(seqh);  
        }
    }else if(human)
    {
            seq.Append(seqv);            
            seq.Append(seqh);         
    }
    else
    {
        if( current.Item1==1)
        {
            seq.Append(seqh);
            seq.Append(seqv);
        }else if (current.Item2== 0 && seqv[0] == N)
        {
            seq.Append(seqh);
            seq.Append(seqv);
           
        }else
        {
            seq.Append(seqv);           
            seq.Append(seqh);  
        }
    }


 
    seq.Append('A');
    var pad = (keypad)?numpad:dirpad;
    System.Console.WriteLine($"From current '{pad[current.Item1,current.Item2]}' to '{pad[next.Item1,next.Item2]}' we get {seq.ToString()}");
    return seq.ToString();    
}
