using FluentValidation;
using FluentValidation.AspNetCore;
using TaskManager.CrossCutting.DTO;
using TaskManager.CrossCutting.Mapper;
using TaskManager.CrossCutting.Validations;
using TaskManager.Repository.Config;
using TaskManager.Repository.Interface;
using TaskManager.Repository.Persistence;
using TaskManager.Service;
using TaskManager.Service.Interface;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddTransient<IValidator<CreateTaskRequestDTO>, CreateTaskValidator>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<TaskDbContext>();
builder.Services.AddScoped<ITaskService, TaskService>();
builder.Services.AddScoped<ITaskRepository, TaskRepository>();
builder.Services.AddAutoMapper(typeof(MappingProfile));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
