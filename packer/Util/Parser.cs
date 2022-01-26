using packer.Model;
using System.Globalization;
using System.Text.RegularExpressions;

namespace packer.Util;


/// <summary>
///  Responsible for parsing file.
/// </summary>
public class Parser : IParser
{
    public Parser()
    {
    }

    /// <summary>
    /// Opens file and reads everyline than parses each line to Package objects.
    /// </summary>    
    /// <returns>Returns List<Package></returns>
    /// <exception cref="APIException">
    /// Thrown when file not found, file malformatted or data within file does not meet requirements    
    /// </exception>
    public List<IPackage> Read(string filePath)
    {
        var records = new List<IPackage>();
        StreamReader? sr = null;

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
            sr.Close();
            return records;
        }
        catch (APIException)
        {
            //throwing error without changing
            throw;
        }
        catch (Exception ex)
        {
            //encapsulating possible errors
            throw new APIException(Messages.FileOpenError, ex);
        }
        finally
        {
            if(sr != null)
            {
                sr.Close();
            }
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
        catch (Exception ex)
        {
            throw new APIException(Messages.LineParsingError, ex);
        }

    }

}
