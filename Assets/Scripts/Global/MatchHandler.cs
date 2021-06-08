using System.Text.RegularExpressions;

public class MatchHandler
{
    private readonly Match _match;
    private int _i = 1;
    public MatchHandler(Match match)
    {
        this._match = match;
    }

    public float nextFloat()
    {
        return float.Parse(_match.Groups[_i++].Value);
    }
    public int nextInt()
    {
        return int.Parse(_match.Groups[_i++].Value);
    }
    public string nextString()
    {
        return _match.Groups[_i++].Value;
    }
}
