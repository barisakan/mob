using Packer;
using System.IO;
using System.Text;
using Xunit;

namespace test;

public class PackerTests
{


    [Fact]
    public void ExampleMemoryDataTest()
    {
        var input = @"81 : (1,53.38,€45) (2,88.62,€98) (3,78.48,€3) (4,72.30,€76) (5,30.18,€9) (6,46.34,€48)
8 : (1,15.3,€34)
75 : (1,85.31,€29) (2,14.55,€74) (3,3.98,€16) (4,26.24,€55) (5,63.69,€52) (6,76.25,€75) (7,60.02,€74) (8,93.18,€35) (9,89.95,€78)
56 : (1,90.72,€13) (2,33.80,€40) (3,43.15,€10) (4,37.97,€16) (5,46.81,€36) (6,48.77,€79) (7,81.80,€45) (8,19.36,€79) (9,6.76,€64)";

        var output = @"4
-
2,7
8,9";

        File.WriteAllText("ExampleMemoryDataTest", input, Encoding.UTF8);

        Assert.Equal(output, Packer.Packer.Pack(@".\ExampleMemoryDataTest"));

        File.Delete("ExampleMemoryDataTest");
    }

    [Fact]
    public void ExampleWrongFormatDataTest()
    {
        var input = @"AB : (1,53.38,€45) (2,88.62,€98) (3,78.48,€3) (4,72.30,€76) (5,30.18,€9) (6,46.34,€48)";
               
        File.WriteAllText("ExampleWrongFormatDataTest", input, Encoding.UTF8);

        var ex = Assert.Throws<APIException>(() => {

            Packer.Packer.Pack(@".\ExampleWrongFormatDataTest");

        });

        Assert.Equal(Messages.LineParsingError, ex.Message);

        File.Delete("ExampleWrongFormatDataTest");
    }


    [Fact]
    public void ParserThrowsErrorWhenFilenotFound()
    {

        var ex = Assert.Throws<APIException>( () => {

            var result = Packer.Packer.Pack(@".\data\some_file");

        });

        Assert.Equal(typeof(APIException), ex.GetType());
    }
}
