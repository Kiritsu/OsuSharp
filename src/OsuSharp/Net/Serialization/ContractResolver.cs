using System;
using Newtonsoft.Json.Serialization;
using OsuSharp.Domain;

namespace OsuSharp.Net.Serialization;

internal sealed class ContractResolver : DefaultContractResolver
{
    public static readonly ContractResolver Instance = new();

    private ContractResolver()
    {
    }

    protected override JsonContract CreateContract(Type objectType)
    {
        var contract = base.CreateContract(objectType);

        if (objectType == typeof(Event))
        {
            contract.Converter = EventConverter.Instance;
        }

        return contract;
    }
}