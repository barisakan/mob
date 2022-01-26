using System.Diagnostics;

namespace Packer.Model;

public class Knapsack
{

    private IPackage package;

    private int capacity;
    private List<Item> items;
    private int itemCount;
    private int[] weights;
    private int[] costs;

    private int precision = 100;
    private int[,] results;



    public Knapsack(IPackage package)
    {
        this.package = package;
        this.items = package.Items.OrderByDescending(i => i.Profit).ToList();

        this.capacity = Convert.ToInt32(package.PackageWeight * precision);        
        this.itemCount = items.Count();


        this.weights = new int[itemCount];
        for (int i = 0; i < itemCount; i++)
        {
            weights[i] = Convert.ToInt32(items[i].Weight * precision);
        }

        this.costs = new int[itemCount];
        for (int i = 0; i < itemCount; i++)
        {
            costs[i] = Convert.ToInt32(items[i].Cost * precision);
        }
    }


    public void Calculate()
    {
        FillMatrix();

        FindSelectedItems();
    }


    private void FillMatrix()
    {
        int item, weight;
        results = new int[itemCount + 1, capacity + 1];

        // Build table results[][] in bottom up manner
        for (item = 0; item <= itemCount; item++)
        {
            for (weight = 0; weight <= capacity; weight++)
            {
                if (item == 0 || weight == 0)
                {
                    results[item, weight] = 0;
                }
                else if (weights[item - 1] <= weight)
                {
                    var a = costs[item - 1] + results[item - 1, weight - weights[item - 1]];
                    var b = results[item - 1, weight];
                    results[item, weight] = Math.Max(a, b);
                }
                else
                {
                    results[item, weight] = results[item - 1, weight];
                }
            }
        }
        
    }

    private void FindSelectedItems()
    {
        int item;
        int res = results[itemCount, capacity];
        var weight = capacity;
        var selectedItems = new List<int>();   

        //scanning matrix from bottom up from last column
        for (item = itemCount; item > 0 && res > 0; item--)
        {
            Trace.WriteLine("+" + (item - 1) + " - " + items[item - 1].Id + "=> rs[" + (item - 1) + ", " + weight + "] :" + results[item - 1, weight] + "--> w:" + weight + " - c:" + res);


            // value equals on previous row so we need to go up
            if (res == results[item - 1, weight])
            {
                continue;
            }
            else
            {
                //deducting cost of included item
                res = res - costs[item - 1];
                //changing column
                weight = weight - weights[item - 1];
                //marking selected item within item list
                selectedItems.Add(items[item - 1].Id);
                //marking selected items on package                
                package.Select(items[item - 1].Id);
            }
            Trace.WriteLine("-" + (item - 1) + " - " + items[item - 1].Id + "=> rs[" + (item - 1) + ", " + weight + "] :" + results[item - 1, weight] + "--> w:" + weight + " - c:" + res);
        }
    }

}