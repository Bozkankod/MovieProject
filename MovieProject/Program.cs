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
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Movie Project App");
    c.RoutePrefix = string.Empty;

});

app.UseHangfireDashboard("/hangfire",new DashboardOptions
{
    DashboardTitle = "Movie Recommendation App Hangfire Dasboard",
    AppPath = "/"
});

RecurringJobs.Add();
app.Run();