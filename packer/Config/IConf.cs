namespace packer.Config;

public interface IConf
{
    double MaxItemCost { get; }
    int MaxItemCount { get; }
    double MaxItemWeight { get; }
    double MaxPackageWeight { get; }
}
