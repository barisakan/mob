
namespace Packer.Model
{
    public interface IPackage
    {
        IReadOnlyList<Item> Items { get; }
        double PackageWeight { get; }

        void AddItem(Item i);
        int Count();
        void Select(int id);
        string ToString();
    }
}