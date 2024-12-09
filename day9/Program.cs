
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;

var filename = "inputdata2.txt";
var input = File.ReadAllLines(filename);

var compressed = input.First().Select(c => (int)Char.GetNumericValue(c));
var uncompressed = new List<int>();
var empty = false;
var emptyspaces = new List<(int,int)>();
var files = new List<(int,int,int)>();
compressed.Aggregate(0, (fileId, blocks) =>
{
    if(empty)
    {
        emptyspaces.Add((uncompressed.Count,blocks));
    }else
    {
        files.Add((uncompressed.Count,blocks,fileId));
    }
    foreach (var i in Enumerable.Range(0, blocks))
    {
        uncompressed.Add((empty) ? -1 : fileId);
    }
    if (empty) fileId++;
    empty = !empty;
    return fileId;
});
var disk = uncompressed;

//Part1Defragmentation(disk);

files.Reverse();

foreach (var file in files )
{
    //Try to move
    var emptyfound = emptyspaces.FirstOrDefault(e => e.Item2>=file.Item2);
    if(emptyfound == default) continue;

    if(emptyfound.Item1 >= file.Item1) continue;

    // Move all blocks
    System.Console.WriteLine($"moving {file.Item3} with size {file.Item2} from {file.Item1} to {emptyfound.Item1}");
    for (int i = 0; i < file.Item2; i++)
    {
        disk.RemoveAt(file.Item1 +i);
        disk.Insert(file.Item1+i, -1);
        disk.RemoveAt(emptyfound.Item1 + i);
        disk.Insert(emptyfound.Item1 +i, file.Item3);    
    }
    //Remove empty space
    var emptyspace = emptyspaces.IndexOf(emptyfound);
    emptyspaces.Remove(emptyfound);
    if(emptyfound.Item2 > file.Item2)
    {
        var newemptysize = emptyfound.Item2 - file.Item2;
        var newemptystart = emptyfound.Item1+emptyfound.Item2 -newemptysize;
        emptyspaces.Insert(emptyspace,(newemptystart,newemptysize));
        System.Console.WriteLine($"Resized empty from {emptyfound.Item2} to {newemptysize}, position from {emptyfound.Item1} to {newemptystart}");
    }

}
System.Console.WriteLine($"{String.Join(" ",disk)}");

//Checksum

BigInteger  checksum = 0; 
 disk.Aggregate(0, (fileId, filepart) =>
{

    if(filepart==-1) { fileId++;return fileId;} 
    //System.Console.Write($"Checksum: {checksum} + ({fileId}*{filepart})=");
    checksum += fileId *filepart;
    //System.Console.WriteLine($"{checksum}");
    fileId++;
    return fileId;
});

Console.WriteLine($"Result part 1: {checksum}");

static void NewMethod(List<int> disk)
{
    for (int i = disk.Count - 1; i > -1; i--)
    {
        if (disk[i] == -1) continue;
        int firstempty = disk.IndexOf(-1);
        if (firstempty >= i) break;

        var filepart = disk.ElementAt(i);
        System.Console.WriteLine($"moving {filepart} at  {i} to {firstempty}");

        disk.RemoveAt(i);
        disk.Insert(i, -1);
        disk.RemoveAt(firstempty);
        disk.Insert(firstempty, filepart);
    }
}