using Bogus;
using packer.Strategy;
using System.Collections.Generic;
using Xunit;


namespace test;

public class TreeTests
{
    [Fact]
    public void Test()
    {
        var f = new Faker<TreeItem>();

        var t = new Tree(f.Generate());


        t.AddItem(f.Generate());

    }
}
