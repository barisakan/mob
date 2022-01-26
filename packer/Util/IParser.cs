using Packer.Model;

namespace Packer.Util
{
    public interface IParser
    {
        List<IPackage> Read(string filePath);
    }
}