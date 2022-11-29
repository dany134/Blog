using Blog.API.Helpers;
using Blog.Contracts;
using Blog.Contracts.Repositories;
using Blog.Contracts.Services;
using Blog.DAL;
using Blog.Repository;
using Blog.Service;
using Microsoft.EntityFrameworkCore;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var config = builder.Configuration;
builder.Services.AddDbContext<BlogContext>(x => x.UseSqlite(config.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("Blog.API")));

builder.Services.AddScoped<IBlogPostRepository, BlogPostRepository>();
builder.Services.AddScoped<IBlogPostService, BlogPostsService>();
builder.Services.AddScoped<ICommentService, CommentService>();
builder.Services.AddScoped<ICommentRepository, CommentRepository>();

builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);



var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<BlogContext>();
    await dbContext.Database.MigrateAsync();
    if (dbContext != null)
    {
        await SeedBlogs.Seed(dbContext);
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
