using System;
using UnityEngine;
using Random = UnityEngine.Random;
using System.Text.RegularExpressions;
public class MatchHandler
{
    private Match _match;
    private const int Index = 1;
    private bool _used = false;
    public MatchHandler(Match match)
    {
        _match = match;
    }

    public float nextFloat()
    {
        if (hasNext())
        {
            _used = true;
            return float.Parse(_match.Groups[Index].Value);
        }
        throw new Exception("Wrong state: Matcher has no next element!");
    }
    public int nextInt()
    {
        if (hasNext())
        {
            _used = true;
            return int.Parse(_match.Groups[Index].Value);
        }
        throw new Exception("Wrong state: Matcher has no next element!");
    }
    public string nextString()
    {
        if (hasNext())
        {
            _used = true;
            return _match.Groups[Index].Value;
        }
        throw new Exception("Wrong state: Matcher has no next element!");
    }

    public bool hasNext()
    {
        if (_used)
        {
            _used = false;
            _match = _match.NextMatch();
        }

        return _match.Success;
    }
}
