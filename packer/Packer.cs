using Packer.Model;
using Packer.Util;
using System.Text;

namespace Packer;

/// <summary>
/// Packer parses provided file and generates string output of the calculated results
/// </summary>
public class Packer
{
    public static IConf cfg { get; private set; }
    public static IParser Parser { get; private set; }
    public static Knapsack Strategy { get; private set; }


    /// <summary>
    /// </summary>
    /// <param name="filePath">Path of the input file.</param>
    /// <returns>Returns string output</returns>
    /// <exception cref="APIException">
    /// Thrown when file not found, file malformatted or data within file does not meet requirements    
    /// </exception>
    public static string Pack(string filePath)
    {
        cfg = Conf.Instance;
        
        Parser = new Parser();

        string result = "";
        
        try
        {
            var packed = PackAll(filePath);
            result = PrintResults(packed);

        }
        catch (APIException e)
        {
            throw;
        }
        catch (Exception e)
        {
            throw new APIException(e);
        }
        return result;
    }

    private static List<IPackage> PackAll(string filePath)
    {
        var packages = Parser.Read(filePath);

        foreach (var package in packages) { 
            
            var st = new Knapsack(package);
            st.Calculate();
        }
        return packages;
    }

    private static string PrintResults(List<IPackage> packages)
    {
        var sb = new StringBuilder();

        for (int i = 0; i < packages.Count; i++)
        {
            var package = packages[i];

            if (i + 1 == packages.Count)
            {
                //lastline does not print newline
                sb.Append(package.ToString());
            }
            else
            {
                sb.AppendLine(package.ToString());
            }

        }
        return sb.ToString();
    }
}
