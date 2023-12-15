
using System.Collections;
using System.Collections.Specialized;
using System.Text;

List<string> lines = new();
using (StreamReader reader = new(args[0]))
{
    while (!reader.EndOfStream)
    {
        lines.Add(reader.ReadLine());
    }
}
var line = lines[0];

part1(); part2();
void part1()
{
    int total = 0;
    foreach (string a in line.Split(','))
        total += hash(a);
    Console.WriteLine(total);
}
void part2()
{
    OrderedDictionary[] boxes = new OrderedDictionary[256];
    foreach (string a in line.Split(','))
    {
        var eqSplit = a.Split('=');
        bool equalsOP = eqSplit.Length > 1;
        string aa = eqSplit[0].Split('-')[0];
        int val = 0;
        if (equalsOP) val = Convert.ToInt32(eqSplit[1]);
        int currentHash = hash(aa);
        if (equalsOP)
        {
            if (boxes[currentHash] is null)
                boxes[currentHash] = new();
            boxes[currentHash][aa] = val;
        }
        else
        {
            if (boxes[currentHash] is not null)
                boxes[currentHash].Remove(aa);
        }
    }
    long answer = 0;
    for (int i = 0; i < 256; i++)
    {
        var box = boxes[i]?.GetEnumerator();
        for (int j = 1; box is not null && box.MoveNext(); j++)
            answer += (i + 1) * j * Convert.ToInt32(box.Value);
    }
    Console.WriteLine(answer);
}

int hash(string a)
{
    int hash = 0;
    foreach (char c in a)
        hash = ((hash + c) * 17) & 255;
    return hash;
}