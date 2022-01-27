using packer.Model;
using System.Diagnostics;

namespace packer.Strategy;


public class Tree
{
    public TreeItem? Self { get; set; }
    public Tree? Left { get; set; }
    public Tree? Right { get; set; }

    public Tree(TreeItem i)
    {
        Self = i;
        Left = null;
        Right = null;
    }

    public void AddItem(TreeItem ti)
    {
        var leaf = GetLeaf(this);
        leaf.Left = new Tree(ti);
    }

    private Tree? GetLeaf(Tree t)
    {
        if (t.Left != null)
        {
            GetLeaf(t);
        }
        return t;
    }
}
public class TreeItem
{
    public int Id { get; set; }
    public double Weight { get; set; }
    public double Cost { get; set; }
}

public class Brute : IKnapsack
{

    private IPackage package;

    private int capacity;
    private List<IItem> items;
    private int itemCount;
    private int[] weights;
    private int[] costs;

    private int precision = 100;
    private int[,] results;


    /// <summary>
    /// Handles packaging operation of the package.
    /// Uses dynamic programming technique to resolve optimum items.
    /// Items are ordered according to cost/weight to make most profitable choice.
    /// </summary>
    /// <param name="package">Package item</param>
    public Brute(IPackage package)
    {
        this.package = package;
        items = package.Items.OrderByDescending(i => i.Profit).ToList();

        capacity = Convert.ToInt32(package.PackageWeight * precision);
        itemCount = items.Count();


        weights = new int[itemCount];
        for (int i = 0; i < itemCount; i++)
        {
            weights[i] = Convert.ToInt32(items[i].Weight * precision);
        }

        costs = new int[itemCount];
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


    /// <summary>
    /// Creates selection matrix for package items
    /// </summary>
    private void FillMatrix()
    {
        int item, weight;
        results = new int[itemCount + 1, capacity + 1];

        knapSack(capacity, weights, costs, itemCount);

    }

    static int knapSack(int W, int[] wt,
                            int[] val, int n)
    {

        // Base Case
        if (n == 0 || W == 0)
            return 0;

        // If weight of the nth item is
        // more than Knapsack capacity W,
        // then this item cannot be
        // included in the optimal solution
        if (wt[n - 1] > W)
            return knapSack(W, wt,
                            val, n - 1);

        // Return the maximum of two cases:
        // (1) nth item included
        // (2) not included
        else
            return Math.Max(val[n - 1]
                       + knapSack(W - wt[n - 1], wt,
                                  val, n - 1),
                       knapSack(W, wt, val, n - 1));
    }


    /// <summary>
    /// Scans generated matrix and determines selected items by traversing matrix from bottom up.
    /// </summary>
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