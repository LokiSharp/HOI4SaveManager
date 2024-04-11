using System.Text;

namespace Core.Text;

public class Reader : IDisposable
{
    private readonly StreamReader _streamReader;

    public Reader(string input, bool isFile = true)
    {
        if (isFile)
        {
            if (File.Exists(input))
                _streamReader = new StreamReader(input);
            else
                throw new FileNotFoundException($"File {input} does not exist.");
        }
        else
        {
            _streamReader = new StreamReader(new MemoryStream(Encoding.UTF8.GetBytes(input)));
        }
    }

    public void Dispose()
    {
        _streamReader.Close();
        GC.SuppressFinalize(this);
    }

    public int PeekNextByte()
    {
        return _streamReader.Peek();
    }

    public int ReadNextByte()
    {
        return _streamReader.Read();
    }
}