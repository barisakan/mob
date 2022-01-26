using System.Diagnostics;

namespace Packer.Model;

public class Knapsack
{
    
    private int packageWeigth;
    private List<Item> items;
    private int itemCount;
    private int[] weights;
    private int[] costs;

    private int precision = 100;
    private int[,] results;

    public Knapsack(double packageWeigth, IReadOnlyList<Item> items)
    {
        this.packageWeigth = Convert.ToInt32(packageWeigth * precision);
        this.items = items.OrderByDescending(i => i.Profit).ToList();
        this.itemCount = items.Count();
        GenerateArrays();

    }

    private void GenerateArrays()
    {
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
        knapSack( packageWeigth, weights, costs, itemCount);        
    }

    public List<int> SelectedItems()
    {
        return MarkItems(packageWeigth, weights, costs, itemCount);
    }

    private void knapSack(int capacity, int[] weights, int[] costs, int itemCnt)
    {
        int item, weight;
        results = new int[itemCnt + 1, capacity + 1];

        // Build table results[][] in bottom up manner
        for (item = 0; item <= itemCnt; item++)
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

    private List<int> MarkItems(int capacity, int[] weights, int[] costs, int itemCnt)
    {
        int item;
        int res = results[itemCnt, capacity];
        var weight = capacity;
        var selectedItems = new List<int>();   

        //scanning matrix from bottom up from last column
        for (item = itemCnt; item > 0 && res > 0; item--)
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
            }
            Trace.WriteLine("-" + (item - 1) + " - " + items[item - 1].Id + "=> rs[" + (item - 1) + ", " + weight + "] :" + results[item - 1, weight] + "--> w:" + weight + " - c:" + res);
        }

        return selectedItems;
    }


    //private double knapSack(double W, int[]? wt,int[]? val, int n)
    //{

    //    // Base Case
    //    if (n == 0 || W == 0)
    //        return 0;

    //    // If weight of the nth item is
    //    // more than Knapsack capacity W,
    //    // then this item cannot be
    //    // included in the optimal solution
    //    if (items[n - 1].Weight > W)
    //    {
    //        return knapSack(W, wt, val, n - 1);
    //    }

    //    // Return the maximum of two cases:
    //    // (1) nth item included
    //    // (2) not included
    //    else
    //    {
    //        double included = items[n - 1].Cost + knapSack(W - items[n - 1].Weight, wt, val, n - 1);
    //        double excluded = knapSack(W, wt, val, n - 1);

    //        if (included > excluded)
    //        {
    //            items[n - 1].Select();
    //            return included;
    //        }
    //        return excluded;
    //    }            
    //}


}