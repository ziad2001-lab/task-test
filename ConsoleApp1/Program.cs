using System;
using System.Collections.Generic;
using System.Text;

public class FizzBuzzResult
{
    /// <summary>
    /// Gets or sets the processed string after FizzBuzz replacements.
    /// </summary>
    public string OutputString { get; set; }

    /// <summary>
    /// Gets or sets the count of FizzBuzz replacements made.
    /// </summary>
    public int Count { get; set; }
}

public class FizzBuzzDetector
{
    private const int MinLength = 7;
    private const int MaxLength = 100;

    /// <summary>
    /// Processes the input string and replaces words based on FizzBuzz logic.
    /// </summary>
    /// <param name="input">The input string to process.</param>
    /// <returns>A FizzBuzzResult object containing the processed string and count of replacements.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the input string is null.</exception>
    /// <exception cref="ArgumentException">Thrown when the input string length is outside the valid range (7-100 characters).</exception>
    public FizzBuzzResult GetOverlappings(string input)
    {
        if (input == null)
        {
            throw new ArgumentNullException(nameof(input), "Input string cannot be null.");
        }

        // Replace newlines with spaces instead of removing them
        input = input.Replace("\n", " ");

        if (input.Length < MinLength || input.Length > MaxLength)
        {
            throw new ArgumentException($"Input string length must be between {MinLength} and {MaxLength} characters.");
        }

        List<(string word, int startIndex)> words = ExtractWords(input);
        StringBuilder result = new StringBuilder(input);
        int count = 0;
        int offset = 0; // keeps track of changes in length

        for (int wordIndex = 0; wordIndex < words.Count; wordIndex++)
        {
            string originalWord = words[wordIndex].word;
            int index = words[wordIndex].startIndex + offset;
            string replacement = GetFizzBuzzReplacement(wordIndex + 1); // 1-based position

            if (replacement != null)
            {
                result.Remove(index, originalWord.Length);
                result.Insert(index, replacement);
                offset += replacement.Length - originalWord.Length;
                count++;
            }
        }

        return new FizzBuzzResult
        {
            OutputString = result.ToString(),
            Count = count
        };
    }

    /// <summary>
    /// Extracts the words from the input string, keeping track of their starting indices.
    /// </summary>
    /// <param name="input">The input string to extract words from.</param>
    /// <returns>A list of tuples containing each word and its starting index in the input string.</returns>
    private List<(string word, int startIndex)> ExtractWords(string input)
    {
        List<(string, int)> words = new List<(string, int)>();
        StringBuilder currentWord = new StringBuilder();
        int wordStart = -1;

        for (int i = 0; i < input.Length; i++)
        {
            char c = input[i];
            if (char.IsLetterOrDigit(c) || (c == '\'' && currentWord.Length > 0))
            {
                if (currentWord.Length == 0)
                    wordStart = i;
                currentWord.Append(c);
            }
            else
            {
                if (currentWord.Length > 0)
                {
                    words.Add((currentWord.ToString(), wordStart));
                    currentWord.Clear();
                }
            }
        }

        if (currentWord.Length > 0)
        {
            words.Add((currentWord.ToString(), wordStart));
        }

        return words;
    }

    /// <summary>
    /// Gets the FizzBuzz replacement string based on the position.
    /// </summary>
    /// <param name="position">The position of the word in the sequence.</param>
    /// <returns>The replacement string ("Fizz", "Buzz", or "FizzBuzz") or null if no replacement is needed.</returns>
    private string GetFizzBuzzReplacement(int position)
    {
        if (position % 15 == 0) return "FizzBuzz";
        if (position % 3 == 0) return "Fizz";
        if (position % 5 == 0) return "Buzz";
        return null;
    }
}

// Example usage:
class Program
{
    static void Main(string[] args)
    {
        string input = @"Mary had a little lamb
Little lamb, little lamb
Mary had a little lamb
It's fleece was white as snow";

        FizzBuzzDetector detector = new FizzBuzzDetector();
        FizzBuzzResult result = detector.GetOverlappings(input);

        Console.WriteLine("Output string:");
        Console.WriteLine(result.OutputString);
        Console.WriteLine($"Count: {result.Count}");
    }
}
