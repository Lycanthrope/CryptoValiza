﻿using Newtonsoft.Json;
using System.Text;

namespace CryptoValiza.Exchanges.Common.Utils;

internal static class StreamExtensions
{
    private const int EightKilobytes = 8192;

    public static T? ReadAndDeserializeFromJson<T>(this Stream stream)
    {
        if (stream == null)
        {
            throw new ArgumentNullException(nameof(stream));
        }

        if (!stream.CanRead)
        {
            throw new NotSupportedException("Can't read from this stream.");
        }

        using var streamReader = new StreamReader(stream);
        using var jsonTextReader = new JsonTextReader(streamReader);

        var jsonSerializer = new JsonSerializer();
        return jsonSerializer.Deserialize<T>(jsonTextReader);
    }

    public static void SerializeToJsonAndWrite<T>(this Stream stream, T objectToWrite)
    {
        if (stream == null)
        {
            throw new ArgumentNullException(nameof(stream));
        }

        if (!stream.CanWrite)
        {
            throw new NotSupportedException("Can't write to this stream.");
        }

        using var streamWriter = new StreamWriter(stream, new UTF8Encoding(), EightKilobytes, true);
        using var jsonTextWriter = new JsonTextWriter(streamWriter);
        var jsonSerializer = new JsonSerializer();
        jsonSerializer.Serialize(jsonTextWriter, objectToWrite);
        jsonTextWriter.Flush();
    }
}
