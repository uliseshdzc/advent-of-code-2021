namespace Day16;

public class Packet
{
    public int Version { get; set; }
    public int TypeId { get; set; }
    public object? Value { get; set; }
}