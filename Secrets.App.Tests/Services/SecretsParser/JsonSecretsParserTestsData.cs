using System.Collections.Generic;
using AutoFixture;
using Secrets.App.Models;
using Secrets.App.Utils;

namespace Secrets.App.Tests.Services.SecretsParser;
public partial class JsonSecretsParserTests
{
    public static IEnumerable<object[]> InvalidFormatTestData =>
        new List<object[]>
        {
            new object[] { "plain text format" },
            new object[] { "{ wrong json: format }" },
            new object[] { "[{ wrong json: format }, { wrong json: format }]" }
        };

    public static IEnumerable<object[]> InvalidJsonSchemeTestData =>
        new List<object[]>
        {
            new object[] { "[{ \"prop1\": \"value1\", \"prop2\": \"value2\" }]" }
        };

    public static IEnumerable<object[]> ValidSecretsJsonTestData =>
        new List<object[]>
        {
            new object[] { CustomJsonConverter.Serialize(new Fixture().CreateMany<Secret>()) }
        };
}
