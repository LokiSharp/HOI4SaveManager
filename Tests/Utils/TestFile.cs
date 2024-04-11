namespace Tests.Utils;

public class TestFile
{
    public static string GetFilePathInTestFile(string filePath)
    {
        var basePath = AppContext.BaseDirectory;
        return Path.Combine(basePath, "TestFiles", filePath);
    }

    public static string ReadFileInTestFile(string filePath)
    {
        return new StreamReader(GetFilePathInTestFile(filePath)).ReadToEnd();
    }
}