using Packer.Model;
using System.Globalization;

namespace Packer.Util;

public class Parser
{
    private string filePath;

    public Parser(string filePath)
    {
        this.filePath = filePath;
    }
    public List<Package> Read()
    {
        var records = new List<Package>();

        using (StreamReader sr = File.OpenText(filePath))
        {
            while (!sr.EndOfStream)
            {
                var line = sr.ReadLine();

                if (line != null)
                {
                    var box = ParseLine(line);
                    records.Add(box);
                }
            }
        }
        return records;
    }

    private static Package ParseLine(string line)
    {
        var parts = line.Split(':');
        var weight = double.Parse(parts[0].Trim(), CultureInfo.InvariantCulture);

        var box = new Package(weight);

        var items = parts[1].Trim().Split(' ');

        foreach (var item in items)
        {
            var values = item.Replace("(", "").Replace(")", "").Split(',');

            var idx = int.Parse(values[0]);
            var itemWeight = double.Parse(values[1], CultureInfo.InvariantCulture);
            var itemCost = double.Parse(values[2].Replace("€", ""), CultureInfo.InvariantCulture);

            box.AddItem(new Item(idx, itemWeight, itemCost));
        }
        return box;
    }

}