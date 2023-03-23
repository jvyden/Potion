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

          Token? lastToken = null;
          bool hasMathExpression = false;
          while ((token = NextToken()).Type != TokenType.CloseParentheses)
          {
               tokens.Add(token);

               if (token.IsMathToken)
               {
                    if (lastToken!.Value.Type is not TokenType.IntLiteral and not TokenType.Identifier)
                    {
                         throw new ParseException("Invalid math expression");
                    }
               }
               else if(token.Type is TokenType.IntLiteral or TokenType.Identifier)
               {
                    if (lastToken != null)
                    {
                         if (!lastToken.Value.IsMathToken)
                         {
                              throw new ParseException("Invalid math expression");
                         }
                         
                         ex.Argument = new MathExpression
                         {
                              Left = new IntLiteralExpression(int.Parse(tokens[0].Data)),
                              Right = new IntLiteralExpression(int.Parse(tokens[2].Data)),
                              Operator = tokens[1].Type
                         };
                    }
                    else
                    {
                         ex.Argument = new IntLiteralExpression(int.Parse(tokens[0].Data));
                    }
               }
               
               lastToken = token;
          }
          
          return ex;
     }

     public RootExpression ParseAll()
     {
          RootExpression root = new();
          while (_position < _tokens.Count)
          {
               Token token = NextToken();

               IExpression? parsed = Parse(token);
               if (parsed == null) continue;

               root.Expressions.Add(parsed);
          }

          if (root.Expressions.Count == 0)
               throw new ParseException("No tokens were found.");

          return root;
     }

     private IExpression? Parse(Token token)
     {
          if (token.Type == TokenType.Print)
               return ParsePrint();
          if (token.Type == TokenType.EndLine)
               return null;

          throw new ParseException("No tokens were found.");
     }
}