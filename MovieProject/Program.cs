using Hangfire;
using MovieProject.Extensions;
using MovieProject.Schedules;


var builder = WebApplication.CreateBuilder(args);


    
var configuration = builder.Configuration;
builder.Services.AddControllers();
builder.Services.AddQueue(configuration);
builder.Services.AddDependencies(configuration);
builder.Services.AddDbContext(configuration);
builder.Services.AddJwt(configuration);
builder.Services.AddSwagger();

var app = builder.Build();
app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});
app.UseSwagger();
app.UseReDoc(options =>
{
    options.DocumentTitle = "Movie Recommendation App";
    options.SpecUrl = "/swagger/v1/swagger.json";
});

app.UseHangfireDashboard("/hangfire",new DashboardOptions
{
    DashboardTitle = "Movie Recommendation App Hangfire Dasboard",
    AppPath = "/api-docs/index.html"
});

RecurringJobs.Add();
app.Run();