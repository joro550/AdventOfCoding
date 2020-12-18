using System;

namespace AdventOfCode2020.Day18
{
    public class InterpreterWithPrecedence
    {
        private Token _currentToken;
        private readonly TokenQueue _tokenQueue;

        public InterpreterWithPrecedence(Token[] tokens)
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

        private long Factor()
        {
            /*
             * factor: INTEGER | LPAREN expr RPAREN
             */
            var factor = _currentToken;
            if (factor != Token.LeftParen)
                return int.Parse(Eat(Token.Integer(_currentToken.Value)));

            Eat(Token.LeftParen);
            var total = Expression();
            Eat(Token.RightParen);
            return total;
        }

        private long Term()
        {
            var total = Factor();
            
            /*
             * term   : factor ((PLUS) factor)*
             */
            while (_currentToken == Token.Plus)
            {
                Eat(Token.Plus);
                total += Factor();
            }

            return total;
        }

        private long Expression()
        {
            /*
             * expr   : term ((MULTIPLY) term)*
             */
            var total = Term();

            
            while (_currentToken == Token.Multiply)
            {
                Eat(Token.Multiply);
                total *= Factor();
            }

            return total;
        }

        public long Run()
        {
            /*
                Arithmetic expression parser / interpreter.
                expr   : term ((MULTIPLY) term)*
                term   : factor ((PLUS) factor)*
                factor: INTEGER | LPAREN expr RPAREN
            */
            var total = 0L;

            while (_currentToken != Token.EndOf)
                total += Expression();

            return total;
        }
    }
}
