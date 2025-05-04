using Microsoft.EntityFrameworkCore;
using SchoolPortal.Core.IRepo;
using SchoolPortal.Data.DataBase;
using SchoolPortal.Data.implements;
using SchoolPortal.Data.Interface;
using SchoolPortal.Data.Mapping;
using SchoolPortal.Data.Repo;
using SchoolPortal.Data.UnitOfWork;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("MyConnection")));

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IStudentService, StudentService>();
builder.Services.AddScoped<ICourseService, CourseService>();
builder.Services.AddScoped<IInstructorService, InstructorService>();
builder.Services.AddScoped<IDepartmentService, DepartmentService>();
builder.Services.AddScoped<IEnrollmentService,EnrollmentService >();

builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(typeof(SchoolPortal.Data.AssemblyMarker).Assembly));

builder.Services.AddAutoMapper(typeof(SchoolPortal.Data.AssemblyMarker).Assembly);
builder.Services.AddAutoMapper(typeof(MappingProfile)); // Or typeof(Program) if you're scanning all profiles in the assembly

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
// Add this line BEFORE `app.UseAuthorization()`
app.UseMiddleware<SchoolPortal.Middleware.ExceptionHandlingMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
