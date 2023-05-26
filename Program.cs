using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapPost("/products", (Produto produto) =>
{
    ProductRepository.Add(produto);
    return Results.Created($"/products/{produto.Code}", produto.Code);
});

app.MapGet("/products/{code}",([FromRoute] string code) =>
{
    var product = ProductRepository.GetBy(code);
    if(product != null)
    {
        return Results.Ok(product);    
    }else
    {
        return Results.NotFound();
    }
});

app.MapPut("/products",(Produto produto) =>
{
    var productSaved = ProductRepository.GetBy(produto.Code);
    productSaved.Name = produto.Name;
    return Results.Ok();

});


app.MapDelete("/products/{code}",([FromRoute] string code) =>
{
    var productSaved = ProductRepository.GetBy(code);
    ProductRepository.Remove(productSaved);
    return Results.Ok();
});


app.MapGet("/configuration/database", (IConfiguration configuration)=>
{
    return Results.Ok($"{configuration["database:Connection"]}/{configuration["database:Port"]}");
});

app.Run();

public static class ProductRepository
{
    public static List<Produto> Products { get; set; } =  Products = new List<Produto>();


    public static void Add(Produto product)
    {

        Products.Add(product);
        
    }

    public static Produto GetBy(string code )
    {
        return Products.FirstOrDefault( p => p.Code == code);
    }

    public static void Remove(Produto produto)
    {
        Products.Remove(produto);
    }
}

public  class Produto
{
    public string Code  { get; set; }
    public string Name { get; set; }

}