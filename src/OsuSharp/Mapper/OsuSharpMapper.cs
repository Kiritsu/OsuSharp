﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using OsuSharp.Domain;
using OsuSharp.Interfaces;

namespace OsuSharp.Mapper;

internal static class OsuSharpMapper
{
    private static readonly MethodInfo Method = typeof(OsuSharpMapper)
        .GetMethod("Transform", BindingFlags.Public | BindingFlags.Static)!;

    public static TImplementation Transform<TImplementation, TModel>(TModel model, IOsuClient client)
        where TModel : class
    {
        var modelType = typeof(TModel);
        var implementingType = typeof(TImplementation);

        var assemblyTypes = implementingType.Assembly
            .GetTypes()
            .Where(x => x.GetInterfaces().Length > 0 && !x.IsInterface)
            .ToList();

        var modelProperties = modelType.GetProperties();
        var implementingProperties = implementingType.GetProperties();
        var implementingConstructors = implementingType.GetConstructors(BindingFlags.NonPublic | BindingFlags.Instance);
        var implementingConstructor = implementingConstructors.First(x => x.GetParameters().Length == 0);
        var implementingInstance = implementingConstructor.Invoke(null);

        foreach (var modelProperty in modelProperties)
        {
            var implementingProperty = implementingProperties
                .FirstOrDefault(x => x.Name == modelProperty.Name);

            if (implementingProperty is null)
            {
                continue;
            }

            var modelValue = modelProperty.GetValue(model);
            if (modelValue is null)
            {
                continue;
            }

            var modelValueType = modelValue.GetType();
            if (!modelValueType.IsClass 
                || modelValueType.Name == "String"  
                || modelValueType.IsAssignableTo(implementingProperty.PropertyType))
            {
                if (implementingProperty.PropertyType.IsEnum)
                {
                    if (!TryGetEnumValue(implementingProperty.PropertyType, modelValue.ToString()!, true,
                            out modelValue))
                    {
                        continue;
                    }
                }

                implementingProperty.SetValue(implementingInstance, modelValue);
                continue;
            }

            if (modelValue is IEnumerable modelValueEnumerable)
            {
                var valueImplementingTypeEnumerable = assemblyTypes.FirstOrDefault(x => x.IsAssignableTo(implementingProperty.PropertyType.GenericTypeArguments.First()));
                var tempList = (IList)Activator.CreateInstance(typeof(List<>).MakeGenericType(valueImplementingTypeEnumerable!))!;

                foreach (var value in modelValueEnumerable)
                {
                    var itfValueEnumerable = Method
                        .MakeGenericMethod(valueImplementingTypeEnumerable!, modelValueType.GenericTypeArguments.First())
                        .Invoke(null, new[] { value, client });

                    if (itfValueEnumerable is IClientEntity)
                    {
                        valueImplementingTypeEnumerable!.GetProperty("Client")!.SetValue(itfValueEnumerable, client);
                    }

                    tempList.Add(itfValueEnumerable);
                }

                implementingProperty.SetValue(implementingInstance, tempList);
                continue;
            }

            var valueImplementingType = assemblyTypes.LastOrDefault(x => x.IsAssignableTo(implementingProperty.PropertyType));
            var itfValue = Method.MakeGenericMethod(valueImplementingType!, modelValueType)
                .Invoke(null, new[] { modelValue, client });

            if (itfValue is IClientEntity)
            {
                valueImplementingType!.GetProperty("Client")!.SetValue(itfValue, client);
            }

            implementingProperty.SetValue(implementingInstance, itfValue);
        }

        if (implementingInstance is IClientEntity)
        {
            implementingType.GetProperty("Client")!.SetValue(implementingInstance, client);
        }

        return (TImplementation)implementingInstance;
    }

    private static bool TryGetEnumValue(Type propertyType, string inputValue, bool ignoreCase,
        [NotNullWhen(true)] out object? value)
    {
        if (Enum.TryParse(propertyType, inputValue, true, out value!))
        {
            return true;
        }

        var fields = propertyType.GetFields();
        foreach (var field in fields)
        {
            var attribute = field.GetCustomAttribute<EnumMemberAttribute>();
            if (attribute is null)
            {
                continue;
            }

            if (string.Equals(attribute.Value!, inputValue, StringComparison.InvariantCultureIgnoreCase))
            {
                value = field.GetValue(null)!;
                return true;
            }
        }

        return false;
    }
}