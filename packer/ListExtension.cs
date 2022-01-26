using packer.Model;
using packer.Strategy;
using System.Text;

namespace packer;

public static class ListExtension
{

    /// <summary>
    /// Processes all packages read from file.
    /// </summary>
    /// <param name="filePath"> path to file to be read</param>
    /// <returns>List<IPackage></returns>
    public static List<IPackage> PackAll(this List<IPackage> packages)
    {
        foreach (var package in packages)
        {
            var st = new Knapsack(package);
            st.Calculate();
        }
        return packages;
    }

    /// <summary>
    /// Provides extension point for future updates. If necessary different algorithm can be applied 
    /// </summary>
    /// <param name="packages"></param>
    /// <param name="f">PArameterless function to be executed</param>
    /// <returns>List<IPackage></returns>
    public static List<IPackage> PackAll(this List<IPackage> packages, Action<IPackage> f)
    {
        foreach (var package in packages)
        {
            f.Invoke(package);
        }
        return packages;
    }

    /// <summary>
    /// Prints out results for every package.
    /// </summary>
    /// <param name="packages"></param>
    /// <returns>string</returns>
    public static string PrintResults(this List<IPackage> packages)
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