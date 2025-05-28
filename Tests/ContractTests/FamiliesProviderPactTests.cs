using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using PactNet.Verifier;
using Xunit;
using Xunit.Abstractions;
using PactNet.Output.Xunit;
using System.Text.Json;

namespace SVContractTestingApi.Tests.PactTests;

public class FamiliesProviderFixture { }

public class FamiliesProviderPactTests : IClassFixture<FamiliesProviderFixture>
{
    private readonly ITestOutputHelper output;

    public FamiliesProviderPactTests(ITestOutputHelper output)
    {
        this.output = output;
    }

    [Theory(DisplayName = "Verify individual Pact interaction")]
    [MemberData(nameof(GetInteractions))]
    public void VerifyIndividualInteraction(JsonElement interaction, string description)
    {
        var config = new PactVerifierConfig
        {
            Outputters = new List<PactNet.Infrastructure.Outputters.IOutput>
            {
                new XunitOutput(output)
            },
        };

        string pactPath = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory,
            "..", "..", "..", "..", "..", "pacts", "ReactFrontend-DotNetBackend.json"));

        var originalPactJson = File.ReadAllText(pactPath);
        var pact = JsonSerializer.Deserialize<JsonElement>(originalPactJson);

        var tempPact = new
        {
            consumer = pact.GetProperty("consumer"),
            provider = pact.GetProperty("provider"),
            interactions = new[] { interaction },
            metadata = pact.GetProperty("metadata")
        };

        var tempPactJson = JsonSerializer.Serialize(tempPact, new JsonSerializerOptions { WriteIndented = true });
        var tempFilePath = Path.GetTempFileName();
        File.WriteAllText(tempFilePath, tempPactJson);

        using var pactVerifier = new PactVerifier("FamiliesList", config);

        pactVerifier
            .WithHttpEndpoint(new Uri("https://localhost:7063"))
            .WithFileSource(new FileInfo(tempFilePath))
            .Verify();

        File.Delete(tempFilePath);
    }

    public static IEnumerable<object[]> GetInteractions()
    {
        string pactPath = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory,
            "..", "..", "..", "..", "..", "pacts", "ReactFrontend-DotNetBackend.json"));



        var pactJson = File.ReadAllText(pactPath);
        var pact = JsonSerializer.Deserialize<JsonElement>(pactJson);

        foreach (var interaction in pact.GetProperty("interactions").EnumerateArray())
        {
            var description = interaction.GetProperty("description").GetString();
            yield return new object[] { interaction, description! };
        }
    }
}
