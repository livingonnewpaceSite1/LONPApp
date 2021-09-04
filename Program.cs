using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using DnetIndexedDb;
using FirstBlazorApp.Models;
using Blazored.LocalStorage;
using MatBlazor;

namespace FirstBlazorApp
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            builder.Services.AddIndexedDbDatabase<EmployeeContext>(o => { o.UseDatabase(new EmployeeOfflineDb()); });
            builder.Services.AddIndexedDbDatabase<EmployeeContext2>(o => { o.UseDatabase(new EmployeeOfflineDb2()); });
           builder.Services.AddMatBlazor();
            builder.Services.AddBlazoredLocalStorage();
            builder.Services.AddSingleton<CustomHttpClient>();
            await builder.Build().RunAsync();
        }
    }
}
