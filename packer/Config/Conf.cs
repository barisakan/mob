namespace packer.Config;
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

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="maxItemCost">Maximum item weight</param>
    /// <param name="maxItemWeight">Maximum item cost</param>
    /// <param name="maxPackageWeight">Maximum package weight</param>
    /// <param name="maxItemCount">Maximum items to be evaluated during packing</param>
    private Conf(double maxItemCost, double maxItemWeight, double maxPackageWeight, int maxItemCount)
    {
        this.maxItemCost = maxItemCost;
        this.maxItemWeight = maxItemWeight;
        this.maxPackageWeight = maxPackageWeight;
        this.maxItemCount = maxItemCount;
    }

    /// <summary>
    /// Returns instance of config. If called before initialized it populates default data
    /// </summary>
    public static IConf Instance
    {
        get
        {
            if (config == null)
            {
                config = new Conf(maxItemCost: 100, maxItemWeight: 100, maxPackageWeight: 100, maxItemCount: 15);
            }
            return config;
        }
    }

    /// <summary>
    /// Static initializor for configuration values.
    /// </summary>
    /// <param name="maxItemCost">Maximum item weight</param>
    /// <param name="maxItemWeight">Maximum item cost</param>
    /// <param name="maxPackageWeight">Maximum package weight</param>
    /// <param name="maxItemCount">Maximum items to be evaluated during packing</param>
    /// <returns></returns>
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