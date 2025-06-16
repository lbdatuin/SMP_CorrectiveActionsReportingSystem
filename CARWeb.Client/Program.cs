using CARWeb.Client;
using CARWeb.Client.Response;
using CARWeb.Client.Services.ClientAuthService;
using CARWeb.Client.Services.ClientCARLabelService;
using CARWeb.Client.Services.ClientDepartmentService;
using CARWeb.Client.Services.ClientUserManagementService;
using CARWeb.Client.Utilities;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();
builder.Services.AddScoped<SubmitModal>();
builder.Services.AddScoped<ModifiedSnackBar>();

//SERVICES
builder.Services.AddScoped<IClientAuthService, ClientAuthService>();
builder.Services.AddScoped<IClientCARLabelService, ClientCARLabelService>();
builder.Services.AddScoped<IClientDepartmentService, ClientDepartmentService>();
builder.Services.AddScoped<IClientUserManagementService, ClientUserManagementService>();

builder.Services.AddMudServices();
builder.Services.AddAuthorizationCore();

await builder.Build().RunAsync();
