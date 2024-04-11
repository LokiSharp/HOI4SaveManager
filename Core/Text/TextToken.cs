namespace Core.Text;

public class TextToken(string value, TokenType tokenType)
{
    public TokenType TokenType { get; set; } = tokenType;
    public string Value { get; set; } = value;
}