using System.Text;

namespace Core.Text;

public class Lexer(Reader reader) : IDisposable
{
    private static readonly HashSet<char> Operators = ['=', '!', '>', '<'];
    private TextToken? _nextToken;
    private bool IsTokenBuffered => _nextToken is not null;

    public void Dispose()
    {
        reader.Dispose();
        GC.SuppressFinalize(this);
    }

    private int ReadNextByte()
    {
        return reader.ReadNextByte();
    }

    private int PeekNextByte()
    {
        return reader.PeekNextByte();
    }

    public TextToken ReadNextToken()
    {
        if (IsTokenBuffered)
        {
            var bufferedToken = _nextToken!;
            _nextToken = null;
            return bufferedToken;
        }

        while (true)
        {
            var peek = PeekNextByte();
            if (peek == -1) break;

            TextToken? token;
            var ch = (char)peek;
            switch (ch)
            {
                case var c when char.IsWhiteSpace(c):
                    ReadNextByte();
                    continue;
                case '#':
                    while (ReadNextByte() is not ('\n' or -1)) ReadNextByte();
                    break;
                case '{':
                    token = new TextToken(((char)ReadNextByte()).ToString(), TokenType.BlockStart);
                    return token;
                case '}':
                    token = new TextToken(((char)ReadNextByte()).ToString(), TokenType.BlockEnd);
                    return token;
                case var c when IsOperator(c):
                    token = new TextToken(ReadOperator(), TokenType.Operator);
                    return token;
                case var c when !char.IsWhiteSpace(c) && c != '"':
                    return new TextToken(ReadScalar(), TokenType.Unquoted);
                case '"':
                    ReadNextByte();
                    var value = ReadScalar();
                    return new TextToken(value, TokenType.Quoted);
                default:
                    throw new Exception($"Unexpected character: {ch}");
            }
        }

        return new TextToken(((char)ReadNextByte()).ToString(), TokenType.End);
    }

    public TextToken PeekNextToken()
    {
        if (!IsTokenBuffered)
        {
            _nextToken = ReadNextToken();
        }

        return _nextToken!;
    }

    private static bool IsOperator(char ch)
    {
        return Operators.Contains(ch);
    }

    private string ReadOperator()
    {
        var sb = new StringBuilder();
        sb.Append((char)ReadNextByte());
        var nextChar = (char)PeekNextByte();
        if (IsOperator(nextChar)) sb.Append((char)ReadNextByte());

        return sb.ToString();
    }

    private string ReadScalar()
    {
        var sb = new StringBuilder();
        while (true)
        {
            var nextByte = PeekNextByte();
            if ((char)nextByte == '}') break;
            if (nextByte == -1 || char.IsWhiteSpace((char)nextByte) || IsOperator((char)nextByte) || nextByte == '"')
            {
                if (nextByte == '"') ReadNextByte();
                break;
            }

            sb.Append((char)ReadNextByte());
        }

        return sb.ToString();
    }

    public List<TextToken> Parse()
    {
        var tokens = new List<TextToken>();
        TextToken token;
        while ((token = ReadNextToken()).TokenType != TokenType.End) tokens.Add(token);

        tokens.Add(token);
        return tokens;
    }
}