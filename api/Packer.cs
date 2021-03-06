using packer;
using packer.Config;
using packer.Strategy;
using packer.Util;

namespace api;

/// <summary>
/// Packer parses provided file and generates string output of the calculated results
/// </summary>
public class Packer
{
    //initializin config with default values
    private static IConf cfg = Conf.Init(maxItemCost: 100, maxItemWeight: 100, maxPackageWeight: 100, maxItemCount: 15);


    /// <summary>
    /// </summary>
    /// <param name="filePath">Path of the input file.</param>
    /// <returns>Returns string output</returns>
    /// <exception cref="APIException">
    /// Thrown when file not found, file malformatted or data within file does not meet requirements    
    /// </exception>
    public static string Pack(string filePath)
    {
        try
        {
            var parser = new Parser();
            return parser.Read(filePath)
                         .PackAll((p) =>
                           {
                               var st = new Knapsack(p);
                               st.Calculate();
                           })
                         .PrintResults();

        }
        catch (APIException)
        {
            throw;
        }
        catch (Exception e)
        {
            throw new APIException(e);
        }
    }
}
