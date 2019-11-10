using System;
using System.Linq;

class CollectResources
{
    static void ConvertResources(string[] resourcesRaw, out string[] resources, out int[] resourcesQuantity)
    {
        string[] resourcesResult = new string[resourcesRaw.Length];
        int[] resourcesQuantityResult = new int[resourcesRaw.Length];

        for (int i = 0; i < resourcesResult.Length; i++)
        {
            string[] resourceParams = resourcesRaw[i].Split('_');

            string resource = resourceParams[0];
            int quantity = 1;

            if (resourceParams.Length == 2)
            {
                quantity = int.Parse(resourceParams[1]);
            }

            resourcesResult[i] = resource;
            resourcesQuantityResult[i] = quantity;
        }

        resources = resourcesResult;
        resourcesQuantity = resourcesQuantityResult;
    }

    static int GetCollectedResourcesQuantity(int[] resourcesQuantities, bool[] isResourceValid, int start, int step)
    {
        bool[] isResourceIndexVisited = new bool[resourcesQuantities.Length];

        int index = start,
            collectedResourcesQuantity = 0;

        while (!isResourceIndexVisited[index])
        {
            if (isResourceValid[index])
            {
                collectedResourcesQuantity += resourcesQuantities[index];
            }

            isResourceIndexVisited[index] = true;
            index = (index + step) % resourcesQuantities.Length;
        }

        return collectedResourcesQuantity;
    }

    static void Main(string[] args)
    {
        string[] resourcesRaw = Console.ReadLine().Split(' ');
        int collectionPathsCount = int.Parse(Console.ReadLine());

        string[] resources = null;
        int[] resourcesQuantities = null;

        ConvertResources(resourcesRaw, out resources, out resourcesQuantities);
        
        bool[] isResourceValid = 
            resources.Select(res => 
                res == "gold" ||
                res == "wood" ||
                res == "food" ||
                res == "stone")
            .ToArray();

        int maxCollectedResources = 0;

        for (int i = 0; i < collectionPathsCount; i++)
        {
            int[] inputParams = Console.ReadLine()
                .Split(' ')
                .Select(int.Parse)
                .ToArray();

            int start = inputParams[0],
                step = inputParams[1];

            int collectedResourcesQuantity = 
                GetCollectedResourcesQuantity(resourcesQuantities, isResourceValid, start, step);

            if (collectedResourcesQuantity > maxCollectedResources)
            {
                maxCollectedResources = collectedResourcesQuantity;
            }
        }

        Console.WriteLine(maxCollectedResources);
    }
}