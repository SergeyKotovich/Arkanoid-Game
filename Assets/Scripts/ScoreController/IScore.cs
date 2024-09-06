using System;

public interface IScore
{
    public event Action<int> ScoreChanged;
}