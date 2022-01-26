using packer.Model;

namespace packer.Util;

public interface IParser
{
    List<IPackage> Read(string filePath);
}
