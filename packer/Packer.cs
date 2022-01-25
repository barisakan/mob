using Packer.Util;
using System.Text;

namespace Packer;

public class Packer
{
    
    public static string Pack(string filePath)
    {
        var sb = new StringBuilder();
        try
        {
            var prs = new Parser(filePath);
            var records = prs.Read();

            foreach (var record in records)
            {
                record.Calculate();
                sb.Append(record.ToString());
                sb.Append("\n");
            }
        }
        catch (Exception e)
        {
            throw new APIException("File processing exception", e);
        }
        return sb.ToString();
    }
}
