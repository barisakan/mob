using packer.Config;

namespace packer.Model;

/// <summary>
/// Item
/// </summary>
public class Item : IItem
{
    private int id;
    private double weight;
    private double cost;
    private double profit;
    private bool selected;
    private IConf conf = Conf.Instance;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="id">Id of the item</param>
    /// <param name="weight">Weight of the item</param>
    /// <param name="cost">Cost of the item</param>
    /// <exception cref="APIException"></exception>
    public Item(int id, double weight, double cost)
    {
        if (weight > conf.MaxItemWeight)
        {
            throw new APIException(Messages.MaxItemWeight);
        }

        if (cost > conf.MaxItemCost)
        {
            throw new APIException(Messages.MaxItemCost);
        }

        this.id = id;
        this.weight = weight;
        this.cost = cost;
        profit = cost / weight;
        selected = false;
    }

    public double Weight { get => weight; }
    public double Cost { get => cost; }
    public double Profit { get => profit; }
    public int Id { get => id; }
    public bool IsSelected { get => selected; }

    public void Select()
    {
        selected = true;
    }
}
