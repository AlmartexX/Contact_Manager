
using Contact.UI.ServiceCollection;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddServices(builder);

var app = builder.Build();
app.UseRouting();

app.UseHttpsRedirection();

app.UseStaticFiles();

//app.UseSwagger();
//app.UseSwaggerUI(c =>
//{
//    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
//    c.RoutePrefix = string.Empty;
//});

//app.UseHttpsRedirection();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapFallbackToFile("/Index.html");
});

app.Run();