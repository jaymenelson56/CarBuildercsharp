using CarBuilder.Models;
List<PaintColor> paints = new List<PaintColor>() 
{
    new PaintColor()
    {
        Id = 1,
        Price = 80.00M,
        Color = "Black"
    },
    new PaintColor()
    {
        Id = 2,
        Price = 20.00M,
        Color = "Red"
    },
    new PaintColor()
    {
        Id = 3,
        Price = 200.00M,
        Color = "Blue"
    },
    new PaintColor()
    {
        Id = 4,
        Price = 100.00M,
        Color = "Green"
    },
    
};
List<Interior> interiors = new List<Interior>()
{
    new Interior()
    {
        Id = 1,
        Price = 200.00M,
        Material = "Faux Leather"
    },
    new Interior()
    {
        Id = 2,
        Price = 400.00M,
        Material = "Leather"
    },
    new Interior()
    {
        Id = 3,
        Price = 100.00M,
        Material = "Fabric"
    },
     new Interior()
    {
        Id = 4,
        Price = 500.00M,
        Material = "Silk"
    }
};
List<Technology> technologies = new List<Technology>()
{
    new Technology()
    {
        Id = 1,
        Price = 500.00M,
        Package = "Basic Package"
    },
    new Technology()
    {
        Id = 2,
        Price = 800.00M,
        Package = "Navigation Package"
    },
    new Technology()
    {
        Id = 3,
        Price = 1000.00M,
        Package = "Visibility Package"
    },
    new Technology()
    {
        Id = 4,
        Price = 1500.00M,
        Package = "Ultra Package"
    }
};
List<Wheels> wheels = new List<Wheels>()
{
    new Wheels()
    {
        Id = 1,
        Price = 500.00M,
        Style = "17-inch Pair Radial"
    },
    new Wheels()
    {
        Id = 2,
        Price = 700.00M,
        Style = "17-inch Pair Radial Black"
    },
    new Wheels()
    {
        Id = 3,
        Price = 800.00M,
        Style = "18-inch Pair Spoke Silver"
    },
    new Wheels()
    {
        Id = 4,
        Price = 700.00M,
        Style = "18-inch Pair Spoke Black"
    },
};


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();



app.Run();


