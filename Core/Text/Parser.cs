﻿namespace Core.Text;

public class Parser(Lexer lexer)
{
    public Node Parse()
    {
        var token = lexer.ReadNextToken();
        return ParseToken(token);
    }

    private Node ParseToken(TextToken token)
    {
        switch (token.TokenType)
        {
            case TokenType.Unquoted:
            case TokenType.Quoted:
                return ParseValue(token);
            case TokenType.Operator:
                return ParseOperator(token);
            case TokenType.BlockStart:
                return ParseBlock();
            case TokenType.End:
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
        if (int.TryParse(token.Value, out var intValue))
        {
            return new IntNode(intValue);
        }

        if (double.TryParse(token.Value, out var doubleValue))
        {
            return new RealNode(doubleValue);
        }

        return new StringNode(token.Value);
    }

    private Node ParseBlock()
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
            node.ChildNodes.Add(ParseToken(token));
            token = lexer.ReadNextToken();
        }

        return node;
    }
}