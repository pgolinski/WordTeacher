﻿// See https://aka.ms/new-console-template for more information


internal class Round(Collection collection)
{
    private Random _random = new();
    private Dictionary<Entry, bool> _answers = new();
    private List<Entry> _notAnswered = [.. collection.Entries];

    public Entry? GetNext()
    {
        return _notAnswered.Count > 0 ? _notAnswered[_random.Next(_notAnswered.Count)] : null;
    }

    public bool ValidateAnswer(Entry entry, string answer)
    {
        var good = entry.Answer == answer.Trim();

        if (!_answers.ContainsKey(entry))
            _answers[entry] = good;
        if (good)
            _notAnswered.Remove(entry);

        return good;
    }

    public (int GoodAnswers, int Percentage) GetStatistics()
    {
        if (_notAnswered.Count > 0)
            throw new Exception("Round isn't complete");

        var good = _answers.Count(answer => answer.Value);
        var percentage = ((double)good / collection.Entries.Length) * 100;

        return (good, (int)percentage);
    }
}