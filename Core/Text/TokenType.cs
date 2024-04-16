namespace Core.Text;

public enum TokenType : byte
{
    Unquoted,
    Quoted,
    Operator,
    BlockStart,
    BlockEnd,
    End
}