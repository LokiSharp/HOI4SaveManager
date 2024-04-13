using Core.Text;
using Tests.Utils;

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

    [Fact]
    public void Parse_Assignment()
    {
        HashSet<string> statements =
            ["0 = 0", "foo = bar"];
        foreach (var node in statements.Select(statement =>
                     new Parser(new Lexer(new Reader(statement, false))).Parse()))
        {
            Assert.Equal(NodeType.Assignment, node.Type);
        }
    }

    [Fact]
    public void Parse_SaveFile()
    {
        HashSet<string> scriptPaths =
        [
            "SaveFile/Provinces.txt",
            "SaveFile/Equipments.txt",
            "SaveFile/States.txt",
            "SaveFile/DivisionTemplates.txt",
            "SaveFile/StrategicOperatives.txt",
            "SaveFile/CharacterManager.txt",
            "SaveFile/RailWay.txt",
            "SaveFile/EquipmentMarket.txt",
            "SaveFile/Countries.txt",
            "SaveFile/Faction.txt",
            "SaveFile/StrategicAir.txt",
            "SaveFile/UnitLeader.txt",
            "SaveFile/Weather.txt",
            "SaveFile/SupplySystem2.txt",
            "SaveFile/Other.txt"
        ];
        foreach (var scriptPath in scriptPaths)
        {
            var node = new Parser(new Lexer(new Reader(TestFile.ReadFileInTestFile(scriptPath), false))).Parse();
            var reParsedNode = new Parser(new Lexer(new Reader(node.ToString()!, false))).Parse();
            Assert.NotNull(node);
            Assert.Equal(reParsedNode.ToString(), node.ToString());
        }
    }
}