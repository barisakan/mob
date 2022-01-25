using Packer;
using Packer.Model;
using Xunit;

namespace test;

public class ModelTests
{
    [Fact]
    public void ItemCreation()
    {
        var id = 1;
        var cost = 50.21;
        var weight = 43.23;

        var i = new Item(id, weight, cost) ;

        Assert.Equal(id, i.Id);
        Assert.Equal(cost, i.Cost);
        Assert.Equal(weight, i.Weight);
        Assert.Equal(cost / weight, i.Profit);
        Assert.False(i.IsSelected);
    }

    [Fact]
    public void ItemThrowsExceptionIfWeightGreaterThan100()
    {
        var id = 1;
        var cost = 50.21;
        var weight = 143.23;
       
        var ex = Assert.Throws<APIException>( () =>
            {
                new Item(id, weight, cost);
            }
        );

        Assert.Equal(Exceptions.MaxWeight, ex.Message);
    }


    [Fact]
    public void ItemThrowsExceptionIfCostGreaterThan100()
    {
        var id = 1;
        var cost = 150.21;
        var weight = 43.23;

        var ex = Assert.Throws<APIException>(() =>
        {
            new Item(id, weight, cost);
        }
        );

        Assert.Equal(Exceptions.MaxCost, ex.Message);
    }

    [Fact]
    public void PackageThrowsExceptionIfPackageWeigthIsAbove100()
    {

        var mw = 101;

        var ex = Assert.Throws<APIException>(() =>
        {
            var pkg = new Package(mw);
        }
        );

        Assert.Equal(Exceptions.MaxBoxWeigth, ex.Message);
    }

    [Fact]
    public void PackageThrowsExceptionIfMorethan15ItemsAdded()
    {
        var id = 1;
        var cost = 50.21;
        var weight = 43.23;
        var mw = 100;

        var ex = Assert.Throws<APIException>(() =>
        {
            var pkg = new Package(mw);

            for (int i = 0; i <= 15; i++)
            {
                pkg.AddItem (new Item(id, weight, cost));
            }
            
        }
        );

        Assert.Equal(Exceptions.MaxItem, ex.Message);
    }

}