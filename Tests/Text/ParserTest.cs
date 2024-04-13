using Core.Text;

namespace Tests.Text;

public class ParserTest
{
    [Fact]
    public void Parse_String()
    {
        HashSet<string> statements =
            ["foo", "bar"];
        foreach (var node in statements.Select(statement =>
                     new Parser(new Lexer(new Reader(statement, false))).Parse()))
        {
            Assert.Equal(NodeType.String, node.Type);
        }
    }

    [Fact]
    public void Parse_Int()
    {
        HashSet<string> statements =
            ["-100", "-10", "-1", "0", "1", "100", "1000"];
        foreach (var node in statements.Select(statement =>
                     new Parser(new Lexer(new Reader(statement, false))).Parse()))
        {
            Assert.Equal(NodeType.Int, node.Type);
        }
    }

    [Fact]
    public void Parse_Real()
    {
        HashSet<string> statements =
            ["-100.0", "-10.0", "-1.0", "0.0", "1.0", "100.0", "1000.0", "3.14"];
        foreach (var node in statements.Select(statement =>
                     new Parser(new Lexer(new Reader(statement, false))).Parse()))
        {
            Assert.Equal(NodeType.Real, node.Type);
        }
    }

    [Fact]
    public void Parse_Operator()
    {
        HashSet<string> statements =
            ["=", "!=", "<>", "<", ">", "<=", ">="];
        foreach (var node in statements.Select(statement =>
                     new Parser(new Lexer(new Reader(statement, false))).Parse()))
        {
            Assert.Equal(NodeType.Operator, node.Type);
        }
    }

    [Fact]
    public void Parse_Array()
    {
        HashSet<string> statements =
            ["{ 0 0 }", "{ foo bar }"];
        foreach (var node in statements.Select(statement =>
                     new Parser(new Lexer(new Reader(statement, false))).Parse()))
        {
            Assert.Equal(NodeType.Array, node.Type);
        }
    }
}