using Core.Text;
using Tests.Utils;

namespace Tests.Text;

public class ReaderTest
{
    [Fact]
    public void ReadNextByte_WithString()
    {
        const string str = "Hello, World!";
        var reader = new Reader(str, false);
        foreach (var ch in str)
        {
            var result = reader.ReadNextByte();
            Assert.Equal(ch, result);
        }
    }

    [Fact]
    public void ReadNextByte_WithFile()
    {
        var filePath = TestFile.GetFilePathInTestFile("Reader/ReadNextByte_WithFile.txt");
        var reader = new Reader(filePath);
        foreach (var ch in File.ReadAllText(filePath))
        {
            var result = reader.ReadNextByte();
            Assert.Equal(ch, result);
        }
    }
}