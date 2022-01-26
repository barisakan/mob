using Packer.Model;
using Packer.Util;
using System.Text;

namespace Packer;

/// <summary>
/// Packer parses provided file and generates string output of the calculated results
/// </summary>
public class Packer
{

    /// <summary>
    /// </summary>
    /// <param name="filePath">Path of the input file.</param>
    /// <returns>Returns string output</returns>
    /// <exception cref="APIException">
    /// Thrown when file not found, file malformatted or data within file does not meet requirements    
    /// </exception>
    public static string Pack(string filePath)
    {
        var sb = new StringBuilder();
        try
        {
            var prs = new Parser(filePath);
            var records = prs.Read();

            for (int i = 0; i < records.Count; i++)
            {
                var record = records[i];    

                var ks = new Knapsack(record.PackageWeight, record.Items);
                ks.Calculate();

                foreach (var idx in ks.SelectedItems())
                {
                    record.Select(idx);
                }

                if (i+1 == records.Count)
                {
                    //lastline does not print newline
                    sb.Append(record.ToString());
                }
                else
                {
                    sb.AppendLine(record.ToString());
                }

            }
        }
        catch (APIException e)
        {
            throw;
        }
        catch (Exception e)
        {
            throw new APIException(e);
        }
        return sb.ToString();
    }
}
