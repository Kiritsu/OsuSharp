using System;
using Newtonsoft.Json.Serialization;

namespace OsuSharp.Serialization
{
    internal sealed class ContractResolver : DefaultContractResolver
    {
        public static readonly ContractResolver Instance = new ContractResolver();
        
        protected override JsonContract CreateContract(Type objectType)
        {
            var contract = base.CreateContract(objectType);

            if (objectType == typeof(Optional<>))
            {
                contract.Converter = OptionalConverter.Instance;
            }

            return contract;
        }
    }
}