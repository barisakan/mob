using packer.Config;

namespace packer.Model;

/// <summary>
/// Stores package information to be packed.
/// </summary>
public class Package : IPackage
{

    private double weight;
    private List<IItem> items;
    private IConf conf = Conf.Instance;

    /// <summary>
    /// Creates new package object with maximum weight
    /// </summary>
    /// <param name="weight">Maximum weight of the package</param>
    /// <exception cref="APIException">Throws exception if weight exceeds value which is defined within config </exception>
    public Package(double weight)
    {
        if (weight > conf.MaxPackageWeight)
        {
            throw new APIException(Messages.MaxPackageWeigth);

        }
        this.weight = weight;
        items = new List<IItem>();
    }

    public Package(double weight, List<IItem> items) : this(weight)
    {
        foreach (var item in items)
        {
            AddItem(item);
        }
    }

    /// <summary>
    /// Adds ne item to package
    /// </summary>
    /// <param name="i"></param>
    /// <exception cref="APIException">Throws exception if maximum number of items exceeded</exception>
    public void AddItem(IItem i)
    {
        //Items heavier than package capacity are discarded
        if (i.Weight <= weight)
        {
            if (items.Count() == conf.MaxItemCount)
            {
                throw new APIException(Messages.MaxItem);
            }
            items.Add(i);
        }
        items = items.OrderByDescending(x => x.Profit).ToList();
    }

    public int Count()
    {
        return items.Count();
    }

    public IReadOnlyList<IItem> Items { get { return items; } }

    public double PackageWeight { get { return weight; } }


    public override string ToString()
    {
        var str = "";
        var loop = Items.OrderBy(i => i.Id);
        foreach (var item in loop)
        {
            if (item.IsSelected)
            {
                if (str.Length > 0)
                {
                    str = str + "," + item.Id.ToString();
                }
                else
                {
                    str = item.Id.ToString();
                }
            }
        }
        if (string.IsNullOrEmpty(str))
            return "-";
        return str;
    }

    /// <summary>
    /// Marks item within Item list as selected.
    /// </summary>
    /// <param name="id">Item id</param>
    public void Select(int id)
    {

        items.Where(i => i.Id == id).First().Select();
    }
}


