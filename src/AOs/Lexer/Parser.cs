partial class Lexer
{
    private void Parse(string[] toks)
    {
        List<string> current_list = new();

        for (int i = 0; i < toks.Length; i++)
        {
            string tok = toks[i];

            if (tok == ";")
            {
                this.Tokens.Add(current_list.ToArray());
                current_list = new List<string>();
            }

            else if (Is_expr(tok))
            {
                string expr = tok;
                string whitespaces = "";

                i++;
                while (i < toks.Length && (Is_expr(toks[i]) || Utils.String.IsWhiteSpace(toks[i])))
                {
                    if (i < toks.Length-1 && Utils.String.IsWhiteSpace(toks[i]) && !Is_expr(toks[i+1]))
                    {
                        whitespaces = toks[i];
                        break;
                    }

                    expr += toks[i];
                    i++;
                }

                i--;

                current_list.Add(Evaluate(expr));
                if (Utils.String.IsWhiteSpace(whitespaces))
                    current_list.Add(whitespaces);
            }

            else
                current_list.Add(tok);
        }

        // Add the last sublist to the result list
        this.Tokens.Add(current_list.ToArray());
    }
}