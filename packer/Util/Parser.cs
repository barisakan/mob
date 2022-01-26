using Packer.Model;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Packer.Util;


/// <summary>
///  Responsible for parsing file.
/// </summary>
public class Parser
{
    private string filePath;

    public Parser(string filePath)
    {
        this.filePath = filePath;
    }

    /// <summary>
    /// Opens file and reads everyline than parses each line to Package objects.
    /// </summary>    
    /// <returns>Returns List<Package></returns>
    /// <exception cref="APIException">
    /// Thrown when file not found, file malformatted or data within file does not meet requirements    
    /// </exception>
    public List<Package> Read()
    {
        var records = new List<Package>();
        StreamReader sr;

        try
        {
            sr = File.OpenText(filePath);
            using (sr)
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
        catch (APIException ex)
        {
            //throwing error without changing
            throw;
        }
        catch (Exception ex)
        {
            //encapsulating possible errors
            throw new APIException(Messages.FileOpenError, ex);
        }

    }

    /// <summary>
    ///  Parses each line according to file specifications.
    /// </summary>    
    /// <returns>Returns Package</returns>
    /// <exception cref="APIException">
    /// Thrown when any parsing error occurs.
    /// </exception>
    private static Package ParseLine(string line)
    {
        try
        {
            var parts = line.Split(':');
            var weight = double.Parse(parts[0].Trim(), CultureInfo.InvariantCulture);

            var box = new Package(weight);

            //using regex to parse items from line
            var regex = new Regex(@"(?<item>(?<id>[0-9]+)(?>[,])(?<weight>[0-9]+[.][0-9]+)(?>[,])(?>[€])(?<cost>[0-9]+))");
            
            if (regex.IsMatch(parts[1]))
            {
                foreach (Match match in regex.Matches(parts[1]))
                {
                    var idx = int.Parse(match.Groups["id"].Value);
                    var itemWeight = double.Parse(match.Groups["weight"].Value, CultureInfo.InvariantCulture);
                    var itemCost = double.Parse(match.Groups["cost"].Value, CultureInfo.InvariantCulture);

                    box.AddItem(new Item(idx, itemWeight, itemCost));
                }
            }
            return box;
        }
        catch(Exception ex)
        {
            throw new APIException(Messages.LineParsingError, ex);
        }
        
    }

}