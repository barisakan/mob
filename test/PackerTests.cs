using System.IO;
using Xunit;

namespace test;

public class PackerTests
{
    [Fact]
    public void ExampleDataTest()
    {

        var expected = File.ReadAllText(@".\data\example_output");

        Assert.Equal(expected, Packer.Packer.Pack(@".\data\example_input"));
    }
}
