using System.Collections;

public static class Recursion
{
    /// <summary>
    /// #############
    /// # Problem 1 #
    /// #############
    /// Using recursion, find the sum of 1^2 + 2^2 + 3^2 + ... + n^2
    /// and return it.  Remember to both express the solution 
    /// in terms of recursive call on a smaller problem and 
    /// to identify a base case (terminating case).  If the value of
    /// n <= 0, just return 0.   A loop should not be used.
    /// </summary>
    public static int SumSquaresRecursive(int n)
{
    // Base case: if n <= 0, return 0
    if (n <= 0)
    {
        return 0;
    }
    // Recursive case: n^2 + SumSquaresRecursive(n - 1)
    return (n * n) + SumSquaresRecursive(n - 1);
}

    /// <summary>
    /// #############
    /// # Problem 2 #
    /// #############
    /// Using recursion, insert permutations of length
    /// 'size' from a list of 'letters' into the results list.  This function
    /// should assume that each letter is unique (i.e. the 
    /// function does not need to find unique permutations).
    ///
    /// In mathematics, we can calculate the number of permutations
    /// using the formula: len(letters)! / (len(letters) - size)!
    ///
    /// For example, if letters was [A,B,C] and size was 2 then
    /// the following would the contents of the results array after the function ran: AB, AC, BA, BC, CA, CB (might be in 
    /// a different order).
    ///
    /// You can assume that the size specified is always valid (between 1 
    /// and the length of the letters list).
    /// </summary>
    public static void PermutationsChoose(List<string> results, string letters, int size, string word = "")
    {
        
        GeneratePermutations(letters.ToCharArray(), size, "", results);
        
    }

    private static void GeneratePermutations(char[] letters, int size, string current, List<string> results)
    {
        // Base case: If the current permutation has reached the desired size, add it to results
        if (current.Length == size)
        {
            results.Add(current);
            return;
        }

        // Recursive case: Iterate over the letters and generate permutations
        foreach (char letter in letters)
        {
            // Skip if the letter is already in the current permutation to avoid duplicates
            if (!current.Contains(letter))
            {
                GeneratePermutations(letters, size, current + letter, results);
            }
        }
    }


    /// <summary>
    /// #############
    /// # Problem 3 #
    /// #############
    /// Imagine that there was a staircase with 's' stairs.  
    /// We want to count how many ways there are to climb 
    /// the stairs.  If the person could only climb one 
    /// stair at a time, then the total would be just one.  
    /// However, if the person could choose to climb either 
    /// one, two, or three stairs at a time (in any order), 
    /// then the total possibilities become much more 
    /// complicated.  If there were just three stairs,
    /// the possible ways to climb would be four as follows:
    ///
    ///     1 step, 1 step, 1 step
    ///     1 step, 2 step
    ///     2 step, 1 step
    ///     3 step
    ///
    /// With just one step to go, the ways to get
    /// to the top of 's' stairs is to either:
    ///
    /// - take a single step from the second to last step, 
    /// - take a double step from the third to last step, 
    /// - take a triple step from the fourth to last step
    ///
    /// We don't need to think about scenarios like taking two 
    /// single steps from the third to last step because this
    /// is already part of the first scenario (taking a single
    /// step from the second to last step).
    ///
    /// These final leaps give us a sum:
    ///
    /// CountWaysToClimb(s) = CountWaysToClimb(s-1) + 
    ///                       CountWaysToClimb(s-2) +
    ///                       CountWaysToClimb(s-3)
    ///
    /// To run this function for larger values of 's', you will need
    /// to update this function to use memoization.  The parameter
    /// 'remember' has already been added as an input parameter to 
    /// the function for you to complete this task.
    /// </summary>
    public static decimal CountWaysToClimb(int s, Dictionary<int, decimal>? remember = null)
    {
        // Initialize the dictionary if it's null
        if (remember == null)
        {
            remember = new Dictionary<int, decimal>();
        }

        // Base cases
        if (s == 0) return 0;
        if (s == 1) return 1;
        if (s == 2) return 2;
        if (s == 3) return 4;

        // Check if value is already computed
        if (remember.ContainsKey(s))
        {
            return remember[s];
        }

        // Compute the value recursively
        decimal ways = CountWaysToClimb(s - 1, remember) +
                    CountWaysToClimb(s - 2, remember) +
                    CountWaysToClimb(s - 3, remember);
        
        // Store the result in the dictionary
        remember[s] = ways;

        return ways;
    }


    /// <summary>
    /// #############
    /// # Problem 4 #
    /// #############
    /// A binary string is a string consisting of just 1's and 0's.  For example, 1010111 is 
    /// a binary string.  If we introduce a wildcard symbol * into the string, we can say that 
    /// this is now a pattern for multiple binary strings.  For example, 101*1 could be used 
    /// to represent 10101 and 10111.  A pattern can have more than one * wildcard.  For example, 
    /// 1**1 would result in 4 different binary strings: 1001, 1011, 1101, and 1111.
    ///	
    /// Using recursion, insert all possible binary strings for a given pattern into the results list.  You might find 
    /// some of the string functions like IndexOf and [..X] / [X..] to be useful in solving this problem.
    /// </summary>
    public static void WildcardBinary(string pattern, List<string> results)
    {
        int index = pattern.IndexOf('*');
        
        // Base case: No wildcard found, add the pattern to the results
        if (index == -1)
        {
            results.Add(pattern);
            return;
        }

        // Recursive case: Replace the wildcard with '0' and '1', and recurse
        WildcardBinary(pattern.Substring(0, index) + '0' + pattern.Substring(index + 1), results);
        WildcardBinary(pattern.Substring(0, index) + '1' + pattern.Substring(index + 1), results);
    }

    /// <summary>
    /// Use recursion to insert all paths that start at (0,0) and end at the
    /// 'end' square into the results list.
    /// </summary>
    void SolveMaze(Maze maze, int x, int y, List<(int, int)> currPath, List<string> results)
    {
        // Add the current position to the path
        currPath.Add((x, y));

        // Check if we have reached the end
        if (maze.IsEnd(x, y))
        {
            // Convert the path to a string and add to results
            results.Add(string.Join("->", currPath.Select(pos => $"({pos.Item1}, {pos.Item2})")));
            currPath.RemoveAt(currPath.Count - 1); // Backtrack
            return;
        }

        // Explore all possible directions: down, up, right, left
        int[] dx = { 1, -1, 0, 0 };
        int[] dy = { 0, 0, 1, -1 };

        for (int i = 0; i < 4; i++)
        {
            int newX = x + dx[i];
            int newY = y + dy[i];

            if (maze.IsValidMove(newX, newY, currPath))
            {
                // Recur for the next position
                SolveMaze(maze, newX, newY, currPath, results);
            }
        }

        // Backtrack by removing the last position from the current path
        currPath.RemoveAt(currPath.Count - 1);
    }

}