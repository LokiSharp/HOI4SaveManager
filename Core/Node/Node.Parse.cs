using Core.Text;

namespace Core.Node;

public abstract partial class Node
{
    public static ArrayNode ParseFile(string input) => ParseFile(new Reader(input, false));

    public static ArrayNode ParseFile(Reader reader) => ParseFile(new Lexer(reader));

    public static ArrayNode ParseFile(Lexer lexer)
    {
        ArrayNode rootNode = new();

        while (lexer.PeekNextToken().TokenType is not TokenType.End)
        {
            rootNode.Add(Parse(lexer));
        }

        return rootNode;
    }

    public static Node Parse(string input) => Parse(new Reader(input, false));

    public static Node Parse(Reader reader) => Parse(new Lexer(reader));

    public static Node Parse(Lexer lexer)
    {
        var token = lexer.ReadNextToken();
        return ParseToken(token, lexer);
    }

    private static Node ParseToken(TextToken token, Lexer lexer)
    {
        switch (token.TokenType)
        {
            case TokenType.Unquoted:
            case TokenType.Quoted:
                return lexer.PeekNextToken().TokenType is TokenType.Operator
                    ? ParseAssignment(token, lexer)
                    : ParseValue(token);
            case TokenType.Operator:
                return ParseOperator(token);
            case TokenType.BlockStart:
                return ParseBlock(lexer);
            case TokenType.End:
            case TokenType.BlockEnd:
            default:
                throw new Exception($"Unexpected token: {token.TokenType} {token.Value}");
        }
    }

    private static OperatorNode ParseOperator(TextToken token)
    {
        var operatorType = token.Value switch
        {
            "=" => OperatorType.Equal,
            "!=" => OperatorType.NotEqual,
            "<>" => OperatorType.NotEqual,
            "<" => OperatorType.LessThan,
            ">" => OperatorType.GreaterThan,
            "<=" => OperatorType.LessThanOrEqual,
            ">=" => OperatorType.GreaterThanOrEqual,
            _ => throw new Exception("Unsupported operator: " + token.Value)
        };

        return new OperatorNode(token.Value, operatorType);
    }

    private static Node ParseValue(TextToken token)
    {
        return new ValueNode(token.Value, token.TokenType == TokenType.Quoted);
    }

    private static Node ParseBlock(Lexer lexer)
    {
        var node = ParseArray(lexer);
        return node;
    }

    private static ArrayNode ParseArray(Lexer lexer)
    {
        var node = new ArrayNode();
        var token = lexer.ReadNextToken();
        while (token.TokenType != TokenType.BlockEnd)
        {
            node.Add(ParseToken(token, lexer));
            token = lexer.ReadNextToken();
        }


        return node;
    }

    private static AssignmentNode ParseAssignment(TextToken token, Lexer lexer)
    {
        var node = new AssignmentNode
        {
            Name = new ValueNode(token.Value),
            Operator = ParseOperator(lexer.ReadNextToken()),
            Value = ParseToken(lexer.ReadNextToken(), lexer)
        };

        return node;
    }
}