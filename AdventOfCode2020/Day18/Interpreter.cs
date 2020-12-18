using System;

namespace AdventOfCode2020.Day18
{
    public class Interpreter
    {
        private Token _currentToken;
        private readonly TokenQueue _tokenQueue;

        public Interpreter(Token[] tokens)
        {
            _tokenQueue = new TokenQueue(tokens);
            _currentToken = _tokenQueue.GetNext();
        }

        private string Eat(Token expectedToken)
        {
            if (_currentToken != expectedToken)
                throw new Exception("Incorrect token found");

            var currentTokenValue = _currentToken.Value;
            _currentToken = GetNextToken();
            return currentTokenValue;
        }

        private Token GetNextToken()
        {
            _currentToken = _tokenQueue.GetNext();
            return _currentToken;
        }

        private long Term()
        {
            /*
             * term: INTEGER | LPAREN expr RPAREN
             */
            var factor = _currentToken;
            if (factor != Token.LeftParen) 
                return int.Parse(Eat(Token.Integer(_currentToken.Value)));
            
            Eat(Token.LeftParen);
            var total = Expression();
            Eat(Token.RightParen);
            return total;
        }

        private long Expression()
        {
            /*
                Arithmetic expression parser / interpreter.
                expr   : term ((PLUS | MULTIPLY) term)*
                term   : INTEGER | LPAREN expr RPAREN
            */
            var total = Term();
            
            while (_currentToken == Token.Plus || _currentToken == Token.Multiply)
            {
                var token = _currentToken;
                if (token == Token.Plus)
                {
                    Eat(Token.Plus);
                    total += Term();
                }
                else if(token == Token.Multiply)
                {
                    Eat(Token.Multiply);
                    total *=  Term();
                }
            }
            return total;
        }

        public long Run()
        {
            var total = 0L;
            
            while (_currentToken != Token.EndOf) 
                total += Expression();

            return total;
        }
    }
}