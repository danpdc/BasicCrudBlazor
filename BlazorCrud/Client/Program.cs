using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BlazorCrud.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            builder.Services.AddMsalAuthentication(opt => 
            {
                builder.Configuration.Bind("AzureAd", opt.ProviderOptions.Authentication);
                opt.ProviderOptions.DefaultAccessTokenScopes.Add("https://graph.microsoft.com/openid");
            });

            await builder.Build().RunAsync();
        }
    }
}
