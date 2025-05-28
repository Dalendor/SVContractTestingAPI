using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PactNet.Verifier;
using Xunit.Abstractions;
using PactNet.Output.Xunit;
using Microsoft.EntityFrameworkCore;
using SVContractTestingAPI.Data;

namespace SVContractTestingApi.Tests.PactTests;

public class FamiliesProviderFixture
{   
    public class FamiliesProviderPactTests : IClassFixture<FamiliesProviderFixture>
    {
        private readonly ITestOutputHelper output;

        public FamiliesProviderPactTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void EnsureFamiliesProviderHonoursPactWithConsumer()
        {
            var config = new PactVerifierConfig
            {
                Outputters = new List<PactNet.Infrastructure.Outputters.IOutput>
                {
                    new XunitOutput(output)
                }
            };

            string pactPath = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, 
                "..", "..", "..", "..", "..", "pacts", "ReactFrontend-DotNetBackend.json"));


            using var pactVerifier = new PactVerifier("FamiliesList", config);

            pactVerifier
                .WithHttpEndpoint(new Uri("https://localhost:7063"))
                .WithFileSource(new FileInfo(pactPath))
                .Verify();
        }
    }
}