namespace Packer.Model;

public class Item
{
    private int id;
    private double weight;
    private double cost;
    private double profit;
    private bool selected;

    public Item(int id, double weight, double cost)
    {
        if (weight > 100)
        {
            throw new APIException(Exceptions.MaxWeight);
        }

        if (cost > 100)
        {
            throw new APIException(Exceptions.MaxCost);
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
