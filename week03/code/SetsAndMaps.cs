using System.Text.Json;

public static class SetsAndMaps
{
    /// <summary>
    /// The words parameter contains a list of two character 
    /// words (lower case, no duplicates). Using sets, find an O(n) 
    /// solution for returning all symmetric pairs of words.  
    ///
    /// For example, if words was: [am, at, ma, if, fi], we would return :
    ///
    /// ["am & ma", "if & fi"]
    ///
    /// The order of the array does not matter, nor does the order of the specific words in each string in the array.
    /// at would not be returned because ta is not in the list of words.
    ///
    /// As a special case, if the letters are the same (example: 'aa') then
    /// it would not match anything else (remember the assumption above
    /// that there were no duplicates) and therefore should not be returned.
    /// </summary>
    /// <param name="words">An array of 2-character words (lowercase, no duplicates)</param>
    public static string[] FindPairs(string[] words)
    {
        // Create a result list to store symmetric pairs
        var result = new List<string>();
        // Create a HashSet to store words as we iterate
        var wordSet = new HashSet<string>();

        // Iterate over each word in the input array
        foreach (var word in words)
        {
            // Reverse the word
            var reversed = new string(word.Reverse().ToArray());

            // If the reverse exists in the set, it's a symmetric pair
            if (wordSet.Contains(reversed))
            {
                // Add the symmetric pair to the result
                result.Add($"{reversed} & {word}");
            }
            else
            {
                // Otherwise, add the word to the set
                wordSet.Add(word);
            }
        }

        // Convert the result list to an array and return
        return result.ToArray();
    }

    /// <summary>
    /// Read a census file and summarize the degrees (education)
    /// earned by those contained in the file.  The summary
    /// should be stored in a dictionary where the key is the
    /// degree earned and the value is the number of people that 
    /// have earned that degree.  The degree information is in
    /// the 4th column of the file.  There is no header row in the
    /// file.
    /// </summary>
    /// <param name="filename">The name of the file to read</param>
    /// <returns>fixed array of divisors</returns>
    public static Dictionary<string, int> SummarizeDegrees(string filename)
    {
        // Create a dictionary to store degree counts
        var degrees = new Dictionary<string, int>();

        // Read each line from the file
        foreach (var line in File.ReadLines(filename))
        {
            // Split the line by commas to extract fields
            var fields = line.Split(",");

            // Degree is in the 4th column (index 3)
            var degree = fields[3].Trim();

            // If the degree is already in the dictionary, increment its count
            if (degrees.ContainsKey(degree))
            {
                degrees[degree]++;
            }
            else
            {
                // Otherwise, add the degree to the dictionary with an initial count of 1
                degrees[degree] = 1;
            }
        }

        // Return the dictionary containing the degree summary
        return degrees;
    }

    /// <summary>
    /// Determine if 'word1' and 'word2' are anagrams.  An anagram
    /// is when the same letters in a word are re-organized into a 
    /// new word.  A dictionary is used to solve the problem.
    /// 
    /// Examples:
    /// is_anagram("CAT","ACT") would return true
    /// is_anagram("DOG","GOOD") would return false because GOOD has 2 O's
    /// 
    /// Important Note: When determining if two words are anagrams, you
    /// should ignore any spaces.  You should also ignore cases.  For 
    /// example, 'Ab' and 'Ba' should be considered anagrams
    /// 
    /// Reminder: You can access a letter by index in a string by 
    /// using the [] notation.
    /// </summary>
    public static bool IsAnagram(string word1, string word2)
    {
        // Convert both words to lowercase and remove any non-letter characters
        word1 = new string(word1.ToLower().Where(char.IsLetter).ToArray());
        word2 = new string(word2.ToLower().Where(char.IsLetter).ToArray());

        // If the lengths are not the same, they cannot be anagrams
        if (word1.Length != word2.Length)
            return false;

        // Create a dictionary to count the occurrences of characters in word1
        var charCount = new Dictionary<char, int>();

        // Populate the dictionary with character counts from word1
        foreach (var c in word1)
        {
            if (charCount.ContainsKey(c))
                charCount[c]++;
            else
                charCount[c] = 1;
        }

        // Check each character in word2 against the dictionary
        foreach (var c in word2)
        {
            if (!charCount.ContainsKey(c) || charCount[c] == 0)
                return false;

            charCount[c]--;
        }

        // If all checks pass, the words are anagrams
        return true;
    }

    /// <summary>
    /// This function will read JSON (Javascript Object Notation) data from the 
    /// United States Geological Service (USGS) consisting of earthquake data.
    /// The data will include all earthquakes in the current day.
    /// 
    /// JSON data is organized into a dictionary. After reading the data using
    /// the built-in HTTP client library, this function will return a list of all
    /// earthquake locations ('place' attribute) and magnitudes ('mag' attribute).
    /// Additional information about the format of the JSON data can be found 
    /// at this website:  
    /// 
    /// https://earthquake.usgs.gov/earthquakes/feed/v1.0/geojson.php
    /// 
    /// </summary>
    public static string[] EarthquakeDailySummary()
    {
        // The URL to fetch earthquake data from the USGS website
        const string uri = "https://earthquake.usgs.gov/earthquakes/feed/v1.0/summary/all_day.geojson";

        // Create an HTTP client to send the request
        using var client = new HttpClient();

        // Prepare the GET request
        using var getRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

        // Get the response stream from the request
        using var jsonStream = client.Send(getRequestMessage).Content.ReadAsStream();

        // Read the response stream
        using var reader = new StreamReader(jsonStream);
        var json = reader.ReadToEnd();

        // Set JSON deserialization options to be case-insensitive
        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

        // Deserialize the JSON into an object of type FeatureCollection
        var featureCollection = JsonSerializer.Deserialize<FeatureCollection>(json, options);

        // Create a list to store the earthquake summaries
        var summaries = new List<string>();

        // Iterate over each feature (earthquake) in the collection
        foreach (var feature in featureCollection.Features)
        {
            // Get the place and magnitude of the earthquake
            var place = feature.Properties.Place;
            var magnitude = feature.Properties.Mag;

            // Add a formatted string to the summaries list
            summaries.Add($"{place} - Mag {magnitude}");
        }

        // Return the summaries as an array
        return summaries.ToArray();
    }
}