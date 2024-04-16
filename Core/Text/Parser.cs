using Core.Node;

namespace Core.Text;

public class Parser(Lexer lexer)
{
    private readonly ArrayNode _rootNode = new();

    public ArrayNode ParseFile()
    {
        while (lexer.PeekNextToken().TokenType is not TokenType.End)
        {
            _rootNode.Add(Parse());
        }

        return _rootNode;
    }

    public Node.Node Parse()
    {
        var token = lexer.ReadNextToken();
        return ParseToken(token);
    }

    private Node.Node ParseToken(TextToken token)
    {
        switch (token.TokenType)
        {
            case TokenType.Unquoted:
            case TokenType.Quoted:
                return lexer.PeekNextToken().TokenType is TokenType.Operator
                    ? ParseAssignment(token)
                    : ParseValue(token);
            case TokenType.Operator:
                return ParseOperator(token);
            case TokenType.BlockStart:
                return ParseBlock();
            case TokenType.End:
            case TokenType.BlockEnd:
            default:
                throw new Exception($"Unexpected token: {token.TokenType} {token.Value}");
        }
    }

    private OperatorNode ParseOperator(TextToken token)
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

    private Node.Node ParseValue(TextToken token)
    {
        return new ValueNode(token.Value, token.TokenType == TokenType.Quoted);
    }

    private Node.Node ParseBlock()
    {
        var node = ParseArray();
        return node;
    }

    private ArrayNode ParseArray()
    {
        var node = new ArrayNode();
        var token = lexer.ReadNextToken();
        while (token.TokenType != TokenType.BlockEnd)
        {
            node.Add(ParseToken(token));
            token = lexer.ReadNextToken();
        }


        return node;
    }

    private AssignmentNode ParseAssignment(TextToken token)
    {
        var node = new AssignmentNode()
        {
            Name = new ValueNode(token.Value),
            Operator = ParseOperator(lexer.ReadNextToken()),
            Value = ParseToken(lexer.ReadNextToken())
        };

        return node;
    }
}