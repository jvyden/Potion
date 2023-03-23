using Potion.Lang.Lexing;
using Potion.Lang.Parsing.Expressions;

namespace Potion.Lang.Parsing;

public class Parser
{
     private readonly List<Token> _tokens;
     private int _position;

     public Parser(IEnumerable<Token> tokens)
     {
          _tokens = tokens.ToList();
     }

     private Token NextToken()
     {
          Token token = _tokens[_position];
          _position++;
          return token;
     }

     private PrintExpression ParsePrint()
     {
          PrintExpression ex = new();

          List<Token> tokens = new();
          Token token = NextToken();
          if (token.Type != TokenType.OpenParentheses)
          {
               throw new ParseException("Expected (");
          }

          while ((token = NextToken()).Type != TokenType.CloseParentheses)
          {
               tokens.Add(token);
          }

          ex.Tokens = tokens;

          return ex;
     }

     public IExpression ParseAll()
     {
          while (_position < _tokens.Count)
          {
               Token token = NextToken();

               return Parse(token);
          }

          throw new ParseException("No tokens were found.");
     }

     private IExpression Parse(Token token)
     {
          if (token.Type == TokenType.Print)
               return ParsePrint();
          
          throw new ParseException("No tokens were found.");
     }
}