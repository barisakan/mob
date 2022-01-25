using System.IO;
using Xunit;

namespace test;

public class PackerTests
{
    [Fact]
    public void ExampleDataTest()
    {

        var expected = File.ReadAllText(@".\data\example_output");

        //var packer = Packer.ConfigureWith( new PackerConfiguration
        //{
        //    MaxItemCost = 100,
        //    MaxItemWeight = 100,
        //    MaxItemCount=15,
        //    PrioritizeCostOverWeightPerformance = true
        //});

        Assert.Equal(expected, Packer.Packer.Pack(@".\data\example_input"));
    }
}
