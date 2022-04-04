using OsuSharp.Interfaces;

namespace OsuSharp.Domain;

public class Cursor : ICursor
{
    public string ApprovedDate { get; internal set; } = null!;
    public string Id { get; internal set; } = null!;

    internal Cursor()
    {

    }
}