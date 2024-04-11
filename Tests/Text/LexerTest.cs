using Core.Text;
using Tests.Utils;

namespace Tests.Text;

public class LexerTest
{
    [Fact]
    public void Parse_StatementWithOperators()
    {
        HashSet<string> statements =
            ["foo = bar", "foo != bar", "foo <> bar", "foo < bar", "foo > bar", "foo <= bar", "foo >= bar"];
        foreach (var tokens in statements.Select(statement => new Lexer(new Reader(statement, false)).Parse()))
        {
            Assert.Equal(4, tokens.Count);
            Assert.Equal("foo", tokens[0].Value);
            Assert.Equal(TokenType.Operator, tokens[1].TokenType);
            Assert.Equal("bar", tokens[2].Value);
            Assert.Equal(TokenType.End, tokens[3].TokenType);
        }
    }

    [Fact]
    public void NextToken_OperatorTokens()
    {
        HashSet<string> operators = ["=", "!=", "<>", "<", ">", "<=", ">="];

        foreach (var op in operators)
        {
            var token = new Lexer(new Reader(op, false)).ReadNextToken();
            Assert.Equal(TokenType.Operator, token.TokenType);
            Assert.Equal(op, token.Value);
        }
    }

    [Fact]
    public void NextToken_BlockTokens()
    {
        HashSet<string> operators = ["{", "}"];

        foreach (var op in operators)
        {
            var token = new Lexer(new Reader(op, false)).ReadNextToken();
            Assert.Equal(op, token.Value);
        }
    }

    [Fact]
    public void NextToken_UnquotedScalarTokens()
    {
        HashSet<string> scalars = ["foo", "bar", "save_version", "game_unique_seed", "1580147473"];

        foreach (var scalar in scalars)
        {
            var token = new Lexer(new Reader(scalar, false)).ReadNextToken();
            Assert.Equal(TokenType.Unquoted, token.TokenType);
            Assert.Equal(scalar, token.Value);
        }
    }

    [Fact]
    public void NextToken_QuotedScalarTokens()
    {
        HashSet<string> scalars = ["\"foo\"", "\"bar\"", "\"save_version\"", "\"game_unique_seed\""];

        foreach (var scalar in scalars)
        {
            var token = new Lexer(new Reader(scalar, false)).ReadNextToken();
            Assert.Equal(TokenType.Quoted, token.TokenType);
            Assert.Equal(scalar.Replace("\"", ""), token.Value);
        }
    }

    [Fact]
    public void NextToken_Commit()
    {
        HashSet<string> scalars = ["#", "# \"bar\"", "# \"save_version\"", "# \"game_unique_seed\""];

        foreach (var token in scalars.Select(scalar => new Lexer(new Reader(scalar, false)).ReadNextToken()))
            Assert.Equal(TokenType.End, token.TokenType);
    }

    [Fact]
    public void NextToken_SaveFile()
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
            var tokens = new Lexer(new Reader(TestFile.ReadFileInTestFile(scriptPath), false)).Parse();
            Assert.NotEmpty(tokens);
        }
    }
}