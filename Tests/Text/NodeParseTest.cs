using Core.Node;
using Tests.Utils;

namespace Tests.Text;

public class NodeParseTest
{
    [Fact]
    public void Parse_String()
    {
        HashSet<string> statements =
            ["foo", "bar"];
        foreach (var node in statements.Select(Node.Parse))
        {
            Assert.Equal(typeof(ValueNode), node.GetType());
        }
    }

    [Fact]
    public void Parse_Int()
    {
        HashSet<string> statements =
            ["-100", "-10", "-1", "0", "1", "100", "1000"];
        foreach (var node in statements.Select(Node.Parse))
        {
            Assert.Equal(typeof(ValueNode), node.GetType());
        }
    }

    [Fact]
    public void Parse_Real()
    {
        HashSet<string> statements =
            ["-100.0", "-10.0", "-1.0", "0.0", "1.0", "100.0", "1000.0", "3.14"];
        foreach (var node in statements.Select(Node.Parse))
        {
            Assert.Equal(typeof(ValueNode), node.GetType());
        }
    }

    [Fact]
    public void Parse_Operator()
    {
        HashSet<string> statements =
            ["=", "!=", "<>", "<", ">", "<=", ">="];
        foreach (var node in statements.Select(Node.Parse))
        {
            Assert.Equal(typeof(OperatorNode), node.GetType());
        }
    }

    [Fact]
    public void Parse_Array()
    {
        HashSet<string> statements =
            ["{ 0 0 }", "{ foo bar }"];
        foreach (var node in statements.Select(Node.Parse))
        {
            Assert.Equal(typeof(ArrayNode), node.GetType());
        }
    }

    [Fact]
    public void Parse_Assignment()
    {
        HashSet<string> statements =
            ["0 = 0", "foo = bar"];
        foreach (var node in statements.Select(Node.Parse))
        {
            Assert.Equal(typeof(AssignmentNode), node.GetType());
        }
    }

    [Fact]
    public void ParseFile_SaveFile()
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
            var node = Node.ParseFile(TestFile.ReadFileInTestFile(scriptPath));
            var reParsedNode = Node.Parse(node.ToString());
            Assert.NotNull(node);
            Assert.Equal(reParsedNode.ToString(), node.ToString());
        }
    }

    [Fact]
    public void ParseFile_Hoi4Save()
    {
        var node = Node.ParseFile(TestFile.ReadFileInTestFile("SaveFile/Hoi4Save.hoi4"));
        Assert.NotNull(node);
        Assert.Equal(74, node.AsArray().Count);
    }
}