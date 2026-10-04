using System.Diagnostics.Tracing;
using Parallel_Prog___file_read;

int totalChars = 0;
int totalWords = 0;
int totalLines = 0;
int totalErrorWords = 0;

Parallel.For(1, 11, i =>
{
    var text = File.ReadAllText($"Folder/file{i}.txt");

    var result = analyzeWord(text);

    Interlocked.Add(ref totalChars, result.Characters);
    Interlocked.Add(ref totalWords, result.Words);
    Interlocked.Add(ref totalLines, result.Lines);
    Interlocked.Add(ref totalErrorWords, result.Errors);

    Console.WriteLine($"file{i}.txt\n  Characters: {result.Characters}\n  Words: {result.Words}\n  Lines: {result.Lines}\n  Errors: {result.Errors}\n");
});

Console.WriteLine("===== TOTAL =====");
Console.WriteLine();
Console.WriteLine($"Files: 10");
Console.WriteLine($"Characters: {totalChars}");
Console.WriteLine($"Words: {totalWords}");
Console.WriteLine($"Lines: {totalLines}");
Console.WriteLine($"Errors: {totalErrorWords}");

FileStatistics analyzeWord(string text)
{
    var fileStats = new FileStatistics();

    fileStats.Characters = text.Length;

    int countWords = 0;
    char[] separators = { ' ', '\t', '\n' };
    string[] words = text.Split(separators, StringSplitOptions.RemoveEmptyEntries);
    for (int i = 0; i < words.Length; i++)
    {
        countWords++;
    }

    fileStats.Words = countWords;

    int countLines = 0;
    string[] lines = text.Split('\n', StringSplitOptions.RemoveEmptyEntries);
    for (int i = 0; i < lines.Length; i++)
    {
        countLines++;
    }

    fileStats.Lines = countLines;

    int countErrors = 0;
    string target = "error";
    string[] wordsError = text.Split(' ');
    for (int i = 0; i < wordsError.Length; i++)
    {
        if (string.Equals(wordsError[i], target, StringComparison.OrdinalIgnoreCase))
        {
            countErrors++;
        }
    }

    fileStats.Errors = countErrors;

    return fileStats;
}


