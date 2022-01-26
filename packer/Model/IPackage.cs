namespace packer.Model;

public interface IPackage
{
    IReadOnlyList<IItem> Items { get; }
    double PackageWeight { get; }
    void AddItem(IItem i);
    int Count();
    void Select(int id);
    string ToString();
}
