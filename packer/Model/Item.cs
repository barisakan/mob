namespace Packer.Model;

public class Item
{
    private int id;
    private double weight;
    private double cost;
    private double profit;
    private bool selected;
    private IConf conf = Conf.Instance;

    public Item(int id, double weight, double cost)
    {
        if (weight > conf.MaxItemWeight)
        {
            throw new APIException(Messages.MaxWeight);
        }

        if (cost > conf.MaxItemCost)
        {
            throw new APIException(Messages.MaxCost);
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
