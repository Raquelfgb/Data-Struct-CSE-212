public static class Arrays
{

    public static double[] MultiplesOf(double number, int length)
    {
        // Step 1: Create an array to hold the multiples.
        double[] multiples = new double[length];
    
        // Step 2: Loop through and calculate each multiple.
        for (int i = 0; i < length; i++)
        {
            // Step 3: Each multiple is the starting number times (i + 1)
            multiples[i] = number * (i + 1);
        }

        // Step 4: Return the array of multiples.
        return multiples;
    }

  
    public static void RotateListRight(List<int> data, int amount)
    {
        // Step 1: Use modulo to ensure amount is within the bounds of the list size.
        int rotate = amount % data.Count;
    
        // Step 2: If the amount is 0, the list remains the same.
        if (rotate == 0)
            return;

        // Step 3: Split the list into two parts.
        List<int> right = data.GetRange(data.Count - rotate, rotate);
        List<int> left = data.GetRange(0, data.Count - rotate);
    
        // Step 4: Clear the original list and combine the two parts to update the original list.
        data.Clear();
        data.AddRange(right);
        data.AddRange(left);
    }
}