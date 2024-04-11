namespace Core.Text;

public enum TokenType
{
    Unquoted,
    Quoted,
    Operator,
    BlockStart,
    BlockEnd,
    End
}