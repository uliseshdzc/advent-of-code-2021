using Common;
using Day16;
using System.Text;

const int VersionBits = 3;
const int TypeBits = 3;
const int LiteralValueBits = 5;
const int Type0Bits = 15;
const int Type1Bits = 11;

string input = await Utils.GetInput(day: 16);

var message = string.Join(
    string.Empty,
    input.Trim().Select(c =>
    {
        int value = Convert.ToInt16(c.ToString(), 16);
        string binary = Convert.ToString(value, 2);

        return binary.PadLeft(4, '0');
    }));

static Packet GetPacket(ref string message, ref int versionSum)
{
    var packet = new Packet
    {
        Version = Convert.ToInt32(message[0..VersionBits], 2),
        TypeId = Convert.ToInt32(message[VersionBits..(VersionBits + TypeBits)], 2),
    };

    message = message[(VersionBits + TypeBits)..];
    versionSum += packet.Version;

    // Literal value packet
    if (packet.TypeId == 4)
    {
        var stringValue = new StringBuilder();
        bool isLastSubpacket = false;
        while (!isLastSubpacket)
        {
            isLastSubpacket = message[0] == '0';
            stringValue.Append(message[1..LiteralValueBits]);
            message = message[LiteralValueBits..];
        }

        packet.Value = Convert.ToInt64(stringValue.ToString(), 2);
        return packet;
    } 

    // Operator packet
    var firstBit = message[0];
    message = message[1..];
    if (firstBit == '0')
    {
        var length = Convert.ToInt32(message[..Type0Bits], 2);
        message = message[Type0Bits..];
        var operatorMessage = message[..length];
        var value = new List<Packet>();
        message = message[length..];
        while (operatorMessage.Length > 0)
        {
            value.Add(GetPacket(ref operatorMessage, ref versionSum));
        }
        packet.Value = value;
    }
    else
    {
        var count = Convert.ToInt32(message[..Type1Bits], 2);
        message = message[Type1Bits..];
        var value = new List<Packet>();
        for (int i = 0; i < count; i++)
        {
            value.Add(GetPacket(ref message, ref versionSum));
        }
        packet.Value = value;
    }

    return packet;
}

int versionSum = 0;
Packet packet = GetPacket(ref message, ref versionSum);
Console.WriteLine(versionSum);

static long GetValue(Packet packet)
{
    if (packet.TypeId == 4)
        return (long?)packet.Value ?? throw new InvalidCastException();

    var value = packet.Value as List<Packet> ?? throw new InvalidCastException();
    return packet.TypeId switch
    {
        0 => value.Sum(GetValue),
        1 => value.Aggregate(1L, (acc, packet) => acc * GetValue(packet)),
        2 => value.Min(GetValue),
        3 => value.Max(GetValue),
        5 => GetValue(value[0]) > GetValue(value[1]) ? 1 : 0,
        6 => GetValue(value[0]) < GetValue(value[1]) ? 1 : 0,
        7 => GetValue(value[0]) == GetValue(value[1]) ? 1 : 0,
        _ => throw new InvalidCastException(),
    };
}

Console.WriteLine(GetValue(packet));