// See https://aka.ms/new-console-template for more information
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;

var filename="inputdata2.txt";
var input = File.ReadAllLines(filename);


var oldmap = input.Where(l => l.Length > 0 && l.First() =='#').ToList();
var map = oldmap.Select( p => String.Join( "",p.Select( c => {
    switch(c)
    {
        case 'O':
            return $"[]";
        case '@':
            return "@.";
    }
    return $"{c}{c}";
})));
var dimension = map.Count();
var posX = 0;
var posY = 0;
foreach (var line in map)
{
    if(line.Contains('@'))
    {
        posX = line.IndexOf('@');break;
    }
    posY++;
}
var movements = input.Skip(map.Count()+1).SelectMany( s => s.ToCharArray());

foreach (var step in movements)
{
    System.Console.WriteLine($"Try to move {posX},{posY} in direction {step}");
    if(!TryMoveItem(posX,posY,step)) {System.Console.WriteLine($"Continuing on {posX}{posY}");continue;} 
    TryMoveItem(posX,posY, step,true);
    System.Console.WriteLine($"Left my place at {posX},{posY}");

    SetTile(posX,posY,'.');
    foreach (var line in map)
    {
        System.Console.WriteLine(line);
    }

    switch(step)
    {
        case '<':
            posX -=1;
            break;
        case '>':
            posX +=1;
            break;
        case 'v':
            posY +=1;
            break;
        case '^':
            posY -=1;
            break;
    }
}


var result = 0;
for (int x = 0; x < dimension; x++)
{
    for (int y = 0; y < dimension; y++)
    {
        if(map.ElementAt(y).ElementAt(x)=='[')
        {
            result += (100*y)+x;
        }
    }
}

Console.WriteLine($"Result part 1: {result}");
bool TryMoveItem(int tryX, int tryY, char direction, bool allowMoving = false)
{
    System.Console.WriteLine($"Trying to move {tryX},{tryY} in direction {direction}");
    
    if(!Inbound(tryX,tryY)){ System.Console.WriteLine("Not Inbound"); return false;}
    System.Console.WriteLine($"{tryX}{tryY} is valid point");
    var mapitem = map.ElementAt(tryY).ElementAt(tryX);
    System.Console.WriteLine($"found {mapitem} at {tryX}{tryY}");
    if( mapitem =='.') return true;
    if( mapitem =='#') return false;
    if(  mapitem =='@')
    {
        var nextX =tryX;
        var nextY =tryY;
        switch(direction)
        {
            case '<':
                nextX-=1;
                break;
           case '>':
                nextX+=1;
                break;
           case 'v':
                nextY+=1;
                break;
           case '^':
                nextY-=1;
                break;
        }
        System.Console.WriteLine($"Looking at {nextX},{nextY} from {tryX}{tryY}");
        if(!TryMoveItem(nextX,nextY,direction,allowMoving)) return false;
        if(allowMoving)
        {
           System.Console.WriteLine($"Moving '{mapitem}' to {nextX}{nextY}");
           SetTile(nextX,nextY,mapitem);
           SetTile(nextX+1,nextY,'.');
        }
        System.Console.WriteLine($"Moving '{mapitem}' to {nextX}{nextY}");
        return true;
    }
    if(mapitem =='[' || mapitem ==']' )
    {
        var leftX =tryX + ((mapitem==']')?-1:0);
        var leftY =tryY;
        var rightX =tryX + ((mapitem=='[')?1:0);
        var rightY =tryY;
        var nextleftX = leftX;
        var nextleftY = leftY;
        var nextrightX = rightX;
        var nextrightY = rightY;
        var othermapitem = (mapitem =='[')?']':'[';
        var otherX  = (mapitem=='[')?rightX:leftX;
        var touchesLeftside = mapitem=='[';
        
        switch(direction)
        {
            case '<':
                nextleftX-=1;
                nextrightX-=1;
                break;
           case '>':
                nextleftX+=1;
                nextrightX+=1;
                break;
           case 'v':
                nextleftY+=1;
                nextrightY+=1;
                break;
           case '^':
                nextleftY-=1;
                nextrightY-=1;
                break;
        }
        System.Console.WriteLine($"Looking at {leftX},{leftY} from {tryX}{tryY}");
        System.Console.WriteLine($"Looking at {rightX},{rightY} from {tryX}{tryY}");
        if(!TryMoveItem(leftX,leftY,direction,allowMoving)||!TryMoveItem(rightX,rightY,direction,allowMoving)) return false;
        
        if(allowMoving)
        {
           if(touchesLeftside)
           {
                System.Console.WriteLine($"Moving '{mapitem}' to {leftX}{leftY}");
                SetTile(leftX,leftY,mapitem);
                System.Console.WriteLine($"Moving '{othermapitem}' to {rightX}{rightY}");
                SetTile(rightX,rightY,othermapitem);
            
           }else
           {
                System.Console.WriteLine($"Moving '{mapitem}' to {rightX}{rightY}");
                SetTile(rightX,rightY,mapitem);
                System.Console.WriteLine($"Moving '{othermapitem}' to {leftX}{leftY}");
                SetTile(leftX,leftY,othermapitem);
           }

        }
        //System.Console.WriteLine($"Moving '{mapitem}' to {nextX}{nextY}");
        return true;{

    }
    return false;
}
bool Inbound(int inX, int inY)
{
    if( inX < 0 || inY < 0 || inX >= dimension || inY >= dimension) return false;
    System.Console.WriteLine($"{inX},{inY} is in bound");
    return true;
}
void SetTile(int setX, int setY, char content)
 {
    System.Console.WriteLine($"Setting {content} at {setX},{setY}");
    var line =    map[setY];
    line = line.Remove(setX,1);
    line = line.Insert(setX,$"{content}");
    map.RemoveAt(setY);
    map.Insert(setY,line);
 }
