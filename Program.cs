

using System;
using System.Net.NetworkInformation;
using System.Text;
using Microsoft.VisualBasic;
using System.Threading;

string customInputLocation;
string replacedDataFolder = null;
Console.WriteLine("Input From EU4 provinces folder = 1 custom output = 2\n");
string IFcustomInputLocation = Console.ReadLine();
if (IFcustomInputLocation == null)
{
    Console.WriteLine("ty idioto, miałeś jedno zadanie\n");
    Thread.Sleep(2137);
    throw new Exception("idiota");
}
if (IFcustomInputLocation == "1")
{
    customInputLocation = @"\Program Files (x86)\Steam\steamapps\common\Europa Universalis IV\history\provinces\";
}
else if (IFcustomInputLocation == "2")
{
    customInputLocation = Console.ReadLine();
}

string provincesFolder = @"\Program Files (x86)\Steam\steamapps\common\Europa Universalis IV\history\provinces";

Console.WriteLine("Output to EU4 provinces folder = 1 custom output = 2\n");
string customOutputLocation = Console.ReadLine();
if (customOutputLocation == null)
{
    Console.WriteLine("no debil\n");
    Thread.Sleep(2137);
    throw new Exception("you had one job");
}
if (customOutputLocation == "1")
{
    replacedDataFolder = @"\Program Files (x86)\Steam\steamapps\common\Europa Universalis IV\history\provinces\";
}
else if (customOutputLocation == "2")
{
    replacedDataFolder = Console.ReadLine();
}

Console.WriteLine("strict replace = 1 normal replace = 2\n");
string replaceMode = Console.ReadLine();
Console.WriteLine('\n');

Console.WriteLine("find:\n");
string find = Console.ReadLine();

Console.WriteLine("find2:\n");
string find2 = Console.ReadLine();

Console.WriteLine("\nreplace:\n");
string replace = Console.ReadLine();

Console.WriteLine("\nreplace2:\n");
string replace2 = Console.ReadLine();



IEnumerable<string> files = Directory.EnumerateFiles(provincesFolder);

foreach (string file in files)
{
    StreamReader reader = null;
    reader = new StreamReader(file, Encoding.GetEncoding(28591), true);
    string fileName = Path.GetFileName(file);
    IEnumerable<string> lines = File.ReadLines(file);
    FileContents fileArray = new();
    reader.Close();
    if (replaceMode == "1")
    {

        File.WriteAllText(replacedDataFolder + fileName, StrictReplace(find, find2, replace, replace2, lines, fileArray), Encoding.GetEncoding(28591));
    }

    else if (replaceMode == "2")
    {
        File.WriteAllText(replacedDataFolder + fileName, NormalReplace(find, find2, replace, replace2, lines, fileArray), Encoding.GetEncoding(28591));
    }
}
return;

string StrictReplace(string find, string find2, string replace, string replace2,IEnumerable<string> lines, FileContents fileArray)
{
    

        foreach (string line in lines)
        {
            if (line != find && line != find2)
            {
                fileArray.data.Add(line);
                Console.WriteLine(line);
            }
            else if (line == find)
            {
                if (replace == null) continue;
                fileArray.data.Add(replace);
                Console.WriteLine(replace);
            }
            else if (line == find2)
            {
                if (replace2 == null) continue;
                fileArray.data.Add(replace2);
                Console.WriteLine(replace2);
            }
        }

        return fileArray.MergeData(fileArray.data);
}


string NormalReplace(string find, string find2, string replace, string replace2, IEnumerable<string> lines, FileContents fileArray)
{
        foreach (string line in lines)
        {
            if (!line.Contains(find) && !line.Contains(find2))
            {
                fileArray.data.Add(line);
                Console.WriteLine(line);
            }
            else if (line.Contains(find))
            {
                if (replace == null) continue;
                fileArray.data.Add(replace);
                    Console.WriteLine(replace);
            }
            else if (line.Contains(find2))
            {
                    if (replace2 == null) continue;
                    fileArray.data.Add(replace2);
                    Console.WriteLine(replace2);
            }
        }

        return fileArray.MergeData(fileArray.data);
}

class FileContents
{
    public List<string> data = new();

    public string MergeData(List<string> data)
    {
        return String.Join("\n", data.ToArray());
    }
}

