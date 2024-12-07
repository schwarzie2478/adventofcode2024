// See https://aka.ms/new-console-template for more information
using System.Xml;

var fileName = "inputdata2.txt";
var input = File.ReadAllLines(fileName);
var input2 = File.ReadAllLines(fileName);
var result = 0;
var result2 = 0;
var verbose = false;
var direction = Direction.Unknown;
var xpos = -1;
var ypos = -1;
var dimension = input.First().Count();

var startingx = -1;
var startingy = -1;
var startingdirection = Direction.Unknown;

//Determine starting position
//Mark X for current position
//Make step ( turn or move forward in current direction)
//Determine if still inside grid, else stop
//Count X in grid
foreach (var line in input)
{
    ypos++;
    xpos = line.IndexOfAny(['<', '>', 'V', '^']);
    if (xpos == -1) continue;
    if (xpos >= 0) break;

}

System.Console.WriteLine($"starting Position: X:{xpos}, Y:{ypos}");
switch (input[ypos].ElementAt(xpos))
{
    case '<':
        direction = Direction.West;
        break;
    case '>':
        direction = Direction.East;
        break;
    case 'V':
        direction = Direction.South;
        break;
    case '^':
        direction = Direction.North;
        break;
    default:
        break;
}
System.Console.WriteLine($"Starting direction: {Enum.GetName(direction)}");

startingx = xpos; startingy = ypos; startingdirection = direction;

var loops = new List<(int, int)>();
do
{

    input[ypos] = input[ypos].Remove(xpos, 1);
    input[ypos] = input[ypos].Insert(xpos, "X");
    input2[ypos] = input2[ypos].Remove(xpos, 1);
    input2[ypos] = input2[ypos].Insert(xpos, String.Format("{0}", GetDirection(direction)));

    if (RoadClear(input, xpos, ypos, direction))
    {

        // System.Console.WriteLine($"Main Road Clear for ({xpos},{ypos}) in direction {Enum.GetName(direction)} ");
        var nextstep = Step(xpos, ypos, direction);
        if (loops.Any(p => p.Item1 == xpos && p.Item2 == ypos))
        {
            System.Console.WriteLine($"loop already found for {xpos},{ypos}");
        }
        else
        {
            if (IsLoopblock(xpos, ypos, direction))
            {
                loops.Add((xpos,ypos));
                result2++;
            }


        }
        xpos = nextstep.Item1; ypos = nextstep.Item2;

    }
    else
    {
        direction = Turn(direction);
        // System.Console.WriteLine($"Turn to {Enum.GetName(direction)}");
    }
} while (Inbound(xpos, ypos, dimension));
System.Console.WriteLine("\n guard route grid");
foreach (var line in input)
{
    System.Console.WriteLine(line);
}
System.Console.WriteLine("\nDirectional grid");
foreach (var line in input2)
{
    System.Console.WriteLine(line);
}
result = input.Aggregate(0, (total, line) =>
{
    total += line.Where(c => c == 'X').Count();
    return total;
});
Console.WriteLine($"Result Part 1: {result}");
Console.WriteLine($"Result Part 2: {result2}");


bool IsLoopblock(int xpos, int ypos, Direction direction)
{
    System.Console.WriteLine($"Block on ({xpos},{ypos})");
    var input3 = File.ReadAllLines(fileName);

    input3[ypos] = input3[ypos].Remove(xpos, 1);
    input3[ypos] = input3[ypos].Insert(xpos, "#");

    xpos = startingx;
    ypos = startingy;
    direction = startingdirection;

    var turns = new List<(int, int, Direction)>();
    do
    {

        input3[ypos] = input3[ypos].Remove(xpos, 1);
        input3[ypos] = input3[ypos].Insert(xpos, "X");


        if (RoadClear(input3, xpos, ypos, direction))
        {
            var nextstep = Step(xpos, ypos, direction);
            xpos = nextstep.Item1; ypos = nextstep.Item2;

        }
        else
        {
            direction = Turn(direction);
            // System.Console.WriteLine($"Turn to {Enum.GetName(direction)}");
            if (turns.Any(t => t.Item1 == xpos && t.Item2 == ypos && t.Item3 == direction))
            {
                System.Console.WriteLine($"Loop found at ({xpos},{ypos}, dir:{direction})");
                //Found loop
                foreach (var line in input3)
                {
                    System.Console.WriteLine(line);
                }
                return true;
            }
            turns.Add((xpos, ypos, direction));
        }
    } while (Inbound(xpos, ypos, dimension));
    return false;
}

char GetDirection(Direction direction)
{
    switch (direction)
    {
        case Direction.East:
            return '>';
        case Direction.South:
            return 'V';
        case Direction.West:
            return '<';
        case Direction.North:
            return '^';

    }
    return Enum.GetName(direction).First();
}
bool RoadClear(string[] input, int xpos, int ypos, Direction direction)
{
    if (verbose) System.Console.WriteLine($"Is Road clear for {xpos},{ypos} in direction {Enum.GetName(direction)}");
    if (xpos == 0 && direction == Direction.West) return true;
    if (ypos == 0 && direction == Direction.North) return true;
    if (xpos == dimension - 1 && direction == Direction.East) return true;
    if (ypos == dimension - 1 && direction == Direction.South) return true;

    if (direction == Direction.West && input[ypos].ElementAt(xpos - 1) == '#') return false;
    if (direction == Direction.East && input[ypos].ElementAt(xpos + 1) == '#') return false;
    if (direction == Direction.North && input[ypos - 1].ElementAt(xpos) == '#') return false;
    if (direction == Direction.South && input[ypos + 1].ElementAt(xpos) == '#') return false;

    return true;
}
bool Inbound(int xpos, int ypos, int dimension)
{
    if (xpos < 0 || ypos < 0 || xpos >= dimension || ypos >= dimension) return false;
    return true;
}

Direction Turn(Direction direction)
{
    switch (direction)
    {
        case Direction.North:
            return Direction.East;
        case Direction.East:
            return Direction.South;
        case Direction.South:
            return Direction.West;
        case Direction.West:
            return Direction.North;
        default:
            return Direction.Unknown;
    }
}
(int, int) Step(int xpos, int ypos, Direction direction)
{
    if (verbose) System.Console.WriteLine($"Stepping from ({xpos},{ypos})");
    switch (direction)
    {
        case Direction.North:
            return (xpos, ypos - 1);
        case Direction.East:
            return (xpos + 1, ypos);
        case Direction.South:
            return (xpos, ypos + 1);
        case Direction.West:
            return (xpos - 1, ypos);
        default:
            return (xpos, ypos);
    }
}

enum Direction
{
    North,
    East,
    South,
    West,
    Unknown
}
