namespace Packer;
/// <summary>
/// Holds business rules for domain model.
/// It can be initiated before accessing singleton instance.
/// </summary>
public class Conf : IConf
{
    private double maxItemCost;
    private double maxItemWeight;
    private double maxPackageWeight;
    private int maxItemCount;
    private static IConf config;

    private Conf(double maxItemCost, double maxItemWeight, double maxPackageWeight, int maxItemCount)
    {
        this.maxItemCost = maxItemCost;
        this.maxItemWeight = maxItemWeight;
        this.maxPackageWeight = maxPackageWeight;
        this.maxItemCount = maxItemCount;
    }

    //Returns instance of config. If called before initialized it populates default data
    public static IConf Instance
    {
        get
        {
            if(config == null)
            {
                config = new Conf(maxItemCost: 100, maxItemWeight: 100, maxPackageWeight: 100, maxItemCount: 15);
            }
            return config;
        }
    }

    public static IConf Init(double maxItemCost, double maxItemWeight, double maxPackageWeight, int maxItemCount)
    {
        config = new Conf(maxItemCost: maxItemCost, maxItemWeight: maxItemWeight, maxPackageWeight: maxPackageWeight, maxItemCount: maxItemCount);
        return config;
    }

    public double MaxItemCost => maxItemCost;
    public double MaxItemWeight => maxItemWeight;

    public double MaxPackageWeight => maxPackageWeight;
    public int MaxItemCount => maxItemCount;
}