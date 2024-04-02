namespace Lexer
{
    partial class Tokenizer
    {
        public List<Token> tokens = [];

        private string line;
        private string tok = "";
        private int i;

        public Tokenizer(string line)
        {
            this.line = line;

            // Loop through all chars in the line.
            for (i = 0; i < line.Length; i++)
            {
                // '#' means that the following text is a comment.
                if (tok == "#")
                    break;

                else if (Utils.String.IsWhiteSpace(tok))
                    ClearToken(); // Remove whitespaces from the line.

                // ';' will be used to separate two different commands.
                // Useful for multiple commands in single line.
                else if (tok == ";")
                    AppendToken(TokenType.EOL);

                else if (">@!".Contains(tok.FirstOrDefault()))
                    AppendToken(TokenType.SYMBOL);

                else if (IsIdentifier(tok.FirstOrDefault()) && !IsExpr(tok.FirstOrDefault()) && tok.Length == 1)
                {
                    AdvanceChar(IsIdentifier);
                    AppendToken(EntryPoint.AllCommands.Contains(tok.ToLower()) ? TokenType.KEYWORD : TokenType.IDENTIFIER);
                }

                else if (IsExpr(tok.FirstOrDefault()) && tok.Length == 1)
                {
                    AdvanceChar(IsExprWithLetter);
                    // If the token contains any letter then it's an identifier.
                    // Remove whitespace from token only when it's an expr.
                    tok = tok.Any(char.IsLetter) ? tok : tok.Replace(" ", "");
                    AppendToken(tok.Any(char.IsLetter) ? TokenType.IDENTIFIER : TokenType.EXPR);
                }

                else if (tok == "\"" || tok == "'")
                {
                    if (!MakeString(tok.FirstOrDefault()))
                    {
                        tokens = [];
                        break;
                    }

                    AppendToken(TokenType.STRING);
                }

                else
                    tok += line[i];
            }

            tok = "";
            AppendToken(TokenType.EOL);
        }

        private void ClearToken()
        {
            i--; // Move to previous char
            tok = "";
        }

        private void AppendToken(TokenType type)
        {
            tokens.Add(new Token(tok.Trim(), type));
            ClearToken();
        }

        private void AdvanceChar(Func<char, bool> func)
        {
            while (i < line.Length && func(line[i]))
            {
                tok += line[i];
                i++; // Move to next char
            }
        }

        private void Advance(TokenType type, Func<char, bool> func)
        {
            AdvanceChar(func);
            AppendToken(type);
        }
    }
}
