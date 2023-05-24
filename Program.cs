using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapPost("/products", (Produto produto) =>
{
    ProductRepository.Add(produto);
});

app.MapGet("/products/{code}",([FromRoute] string code) =>
{
    var product = ProductRepository.GetBy(code);
    return product;
});

app.MapPut("/products",(Produto produto) =>
{
    var productSaved = ProductRepository.GetBy(produto.Code);
    productSaved.Name = produto.Name;

});


app.MapDelete("/products/{code}",([FromRoute] string code) =>
{
    var productSaved = ProductRepository.GetBy(code);
    ProductRepository.Remove(productSaved);

});


app.Run();

public static class ProductRepository
{
    public static List<Produto> Products { get; set; }

    public static void Add(Produto product)
    {
        if(Products == null)
        {
            Products = new List<Produto>();
        }
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