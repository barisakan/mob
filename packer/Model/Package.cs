using System.Diagnostics;

namespace Packer.Model;

public class Package
{

    private double weight;
    private List<Item> items;

    public Package(double weight)
    {
        if (weight > 100)
        {
            throw new APIException("Box weight can not be more than 100");

        }
        this.weight = weight;
        items = new List<Item>();
    }

    public Package(double weight, List<Item> items)
    {
        this.weight = weight;
        this.items = items;
    }

    public void AddItem(Item i)
    {
        //Items heavier than package capacity are discarded
        if (i.Weight <= weight)
        {
            if (items.Count() == 15)
            {
                throw new APIException("Maximum 15 items can be evaluated");
            }
            items.Add(i);
        }
        items = items.OrderByDescending(x => x.Profit).ToList();
    }

    private int[] Weights()
    {

        var weights = new int[items.Count()];
        for (int i = 0; i < items.Count; i++)
        {
            weights[i] = Convert.ToInt32(items[i].Weight * 100);
        }

        return weights;
    }

    private int[] Costs()
    {
        var costs = new int[items.Count()];
        for (int i = 0; i < items.Count; i++)
        {
            costs[i] = Convert.ToInt32(items[i].Cost * 100);
        }

        return costs;
    }

    private int Weight()
    {
        return Convert.ToInt32(weight * 100);
    }

    public void Calculate()
    {
        knapSack(Weight(), Weights(), Costs(), items.Count());
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


    private void knapSack(int capacity, int[] weights, int[] costs, int itemCnt)
    {
        int item, weight;
        int[,] results = new int[itemCnt + 1, capacity + 1];

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

        MarkItems(capacity, weights, costs, itemCnt, results);
    }

    private void MarkItems(int capacity, int[] weights, int[] costs, int itemCnt, int[,] results)
    {
        int item;
        int res = results[itemCnt, capacity];
        var weight = capacity;

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
                items[item - 1].Select();
            }

            Trace.WriteLine("-" + (item - 1) + " - " + items[item - 1].Id + "=> rs[" + (item - 1) + ", " + weight + "] :" + results[item - 1, weight] + "--> w:" + weight + " - c:" + res);
        }
    }

    public override string ToString()
    {
        var pList = items.OrderBy(x => x.Id);
        var str = "";
        foreach (var item in pList)
        {
            if (item.IsSelected)
            {
                if (str.Length > 0)
                {
                    str = str + "," + item.Id.ToString();
                }
                else
                {
                    str = item.Id.ToString();
                }
            }
        }
        if (string.IsNullOrEmpty(str))
            return "-";
        return str;
    }
}

//81 : (1,53.38,€45) (2,88.62,€98) (3,78.48,€3) (4,72.30,€76) (5,30.18,€9) (6,46.34,€48)
//8 : (1,15.3,€34)
//75 : (1,85.31,€29) (2,14.55,€74) (3,3.98,€16) (4,26.24,€55) (5,63.69,€52) (6,76.25,€75) (7,60.02,€74) (8,93.18,€35) (9,89.95,€78)
//56 : (1,90.72,€13) (2,33.80,€40) (3,43.15,€10) (4,37.97,€16) (5,46.81,€36) (6,48.77,€79) (7,81.80,€45) (8,19.36,€79) (9,6.76,€64)