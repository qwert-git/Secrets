using System.Collections.Generic;
using AutoFixture;
using Secrets.App.Models;
using Secrets.App.Utils;

namespace Secrets.App.Tests.Services.SecretsToRawDataConverter;

public partial class SecretsToJsonConverterTests
{
    public static IEnumerable<object[]> SingleSecretWithExpectedString
    {
        get
        {
            var testSecret = new Fixture().Create<Secret>();
            return new List<object[]>
            {
                new object[]
                {
                    new [] { testSecret },
                    CustomJsonConverter.Serialize(new [] { testSecret })
                }
            };
        }
    }

    public static IEnumerable<object[]> ManySecretsWithExpectedString
    {
        get
        {
            var testSecrets = new Fixture().CreateMany<Secret>();
            return new List<object[]>
            {
                new object[]
                {
                    testSecrets,
                    CustomJsonConverter.Serialize(testSecrets)
                }
            };
        }
    }
}