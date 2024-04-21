using System.ComponentModel;
using CarBuilder.Models;
using CarBuilder.Models.DTOs;
List<PaintColor> paints = new List<PaintColor>
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
List<Interior> interiors = new List<Interior>
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
List<Technology> technologies = new List<Technology>
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
List<Wheels> wheels = new List<Wheels>
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

List<Order> orders = new List<Order> 
{
    new Order
    {
        Id = 1,
        Timestamp = DateTime.Today,
        WheelId = 1,
        PaintId = 1,
        InteriorId = 1,
        TechnologyId = 1,
        IsFulfilled = false
    }

};


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseCors(options =>
                {
                    options.AllowAnyOrigin();
                    options.AllowAnyMethod();
                    options.AllowAnyHeader();
                });

}

app.UseHttpsRedirection();

app.MapGet("/paints", () =>
{
    return paints.Select(s => new PaintColorDTO
    {
        Id = s.Id,
        Price = s.Price,
        Color = s.Color
    });
});

app.MapGet("/interiors", () =>
{
    return interiors.Select(i => new InteriorDTO
    {
        Id = i.Id,
        Price = i.Price,
        Material = i.Material
    });
});

app.MapGet("/technologies", () =>
{
    return technologies.Select(s => new TechnologyDTO
    {
        Id = s.Id,
        Price = s.Price,
        Package = s.Package
    });
});

app.MapGet("/wheels", () =>
{
    return wheels.Select(w => new WheelsDTO
    {
        Id = w.Id,
        Price = w.Price,
        Style = w.Style
    });
});

app.MapGet("/orders", () =>
{
    List<Order> unfulfilledOrders = orders.Where(o => !o.IsFulfilled).ToList();
    
    return unfulfilledOrders.Select(o => new OrderDTO
    {
        Id = o.Id,
        Timestamp = o.Timestamp,
        WheelId = o.WheelId,
        Wheels = new WheelsDTO
        {
            Id = o.WheelId,
            Price = wheels.First(w => w.Id == o.WheelId).Price,
            Style = wheels.First(s => s.Id == o.WheelId).Style,
        },
        TechnologyId = o.TechnologyId,
        Technology = new TechnologyDTO
        {
            Id = o.TechnologyId,
            Price = technologies.First(t => t.Id == o.TechnologyId).Price,
            Package = technologies.First(t => t.Id == o.TechnologyId).Package
        },
        PaintId = o.PaintId,
        PaintColor = new PaintColorDTO
        {
            Id = o.PaintId,
            Price = paints.First(p => p.Id == o.PaintId).Price,
            Color = paints.First(p => p.Id == o.PaintId).Color
        },
        InteriorId = o.InteriorId,
        Interior = new InteriorDTO
        {
            Id = o.InteriorId,
            Price = interiors.First(i => i.Id == o.InteriorId).Price,
            Material = interiors.First(i => i.Id == o.InteriorId).Material
        },
    });
});

app.MapPost("/orders", (Order newOrder) =>
{
    Wheels wheel = wheels.FirstOrDefault(w => w.Id == newOrder.WheelId);
    if (wheel == null)
    {
        return Results.BadRequest();
    }
    Technology tech = technologies.FirstOrDefault(t => t.Id == newOrder.TechnologyId);
    if (tech == null)
    {
        return Results.BadRequest();
    }
    PaintColor paint = paints.FirstOrDefault(p => p.Id == newOrder.PaintId);
    if (paint == null)
    {
        return Results.BadRequest();
    }
    Interior interi = interiors.FirstOrDefault(i => i.Id == newOrder.InteriorId);
    if (interi == null)
    {
        return Results.BadRequest();
    }

   newOrder.Id = orders.Count > 0 ? orders.Max(o => o.Id) + 1 : 1; 
   orders.Add(newOrder);
    return Results.Created($"/orders/{newOrder.Id}", new OrderDTO
    {
        Id = newOrder.Id,
        Timestamp = DateTime.Now,
        WheelId = newOrder.WheelId,
        Wheels = new WheelsDTO
        {
            Id = wheel.Id,
            Price = wheel.Price,
            Style = wheel.Style,
        },
        TechnologyId = newOrder.TechnologyId,
        Technology = new TechnologyDTO
        {
            Id = tech.Id,
            Price = tech.Price,
            Package = tech.Package
        },
        PaintId = newOrder.PaintId,
        PaintColor = new PaintColorDTO
        {
            Id = paint.Id,
            Price = paint.Price,
            Color = paint.Color
        },
        InteriorId = newOrder.InteriorId,
        Interior = new InteriorDTO
        {
            Id = interi.Id,
            Price = interi.Price,
            Material = interi.Material
        },
        IsFulfilled = false
    });




});

app.MapPut("/orders/{id}/fulfill", (int id) =>
{
    Order orderToFulfill = orders.FirstOrDefault(o => o.Id == id);
    if(orderToFulfill==null)
    {
        return Results.NotFound();
    }
    orderToFulfill.IsFulfilled = true;
    

    return Results.Ok();
   
});

app.Run();


