
using BlogPostsMaster_BL.BlogPostsMasterFeatures;
using CategorieMaster_BL.CategorieMasterFeatures;
using CMSBlogMaster_BL.BlogPostsMasterFeatures;
using CMSBlogMaster_BL.CategorieMasterFeatures;
using CMSBlogMaster_BL.CommentsMasterFeatures;
using CMSBlogMaster_BL.Database;
using CommentsMaster_BL.CommentsMasterFeatures;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<CMSBlogMasterDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddScoped<AddCategorieMasterWrapper, AddCategorieMasterFeatures>();
builder.Services.AddScoped<GetCategorieMasterWrapper, GetCategorieMasterFeatures>();
builder.Services.AddScoped<UpdateCategorieMasterWrapper, UpdateCategorieMasterFeatures>();

builder.Services.AddScoped<AddBlogPostsMasterWrapper, AddBlogPostsMasterFeatures>();
builder.Services.AddScoped<GetBlogPostsMasterWrapper, GetBlogPostsMasterFeatures>();
builder.Services.AddScoped<UpdateBlogPostsMasterWrapper, UpdateBlogPostsMasterFeatures>();

builder.Services.AddScoped<AddCommentsMasterWrapper, AddCommentsMasterFeatures>();
builder.Services.AddScoped< GetCommentsMasterWrapper, GetCommentsMasterFeatures>();
builder.Services.AddScoped<UpdateCommentsMasterWrapper, UpdateCommentsMasterFeatures>();


builder.Services.AddCors(c =>
{
    c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin().AllowAnyMethod().
     AllowAnyHeader());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseCors("AllowOrigin");


app.Run();
[ExcludeFromCodeCoverage]
public static partial class Program { }