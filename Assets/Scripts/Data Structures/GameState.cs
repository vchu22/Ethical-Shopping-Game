using System.Collections.Generic;
using System.Linq;

public static class GameState
{
    public static int currentRound = 1;
    private static Aisle[] itemCatelog;
    public static int[] selectedAisleItemsIdx;

    public static void setupAisleProducts(Aisle[] input)
    {
        itemCatelog = new Aisle[input.Length];
    }
    public static AisleItem[] getAisleProducts(int idx)
    {
        if (idx < 0 || itemCatelog.Length <= idx) { 
            return null; 
        }
        return itemCatelog[idx].items;
    }
    public static AisleItem[] getAisleProducts(string aisleName)
    {
        foreach (Aisle a in itemCatelog) { 
            if (a.name == aisleName)
            {
                return a.items;
            }
        }
        return null;
    }
    public static string[] getAisleNameList()
    {
        return itemCatelog.Select(itemCatelog => itemCatelog.name).ToArray();
    }
}
