using System;
using NUnit.Framework;
using meowtown_api.Controllers;
using meowtown_api.Models;
using Moq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Meowtown.Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task Test1()
        {
          var optionsBuilder = new DbContextOptionsBuilder<MeowtownContext>();
          optionsBuilder.UseInMemoryDatabase("Cats");
          var dbContext = new MeowtownContext(optionsBuilder.Options);
          var controller = new CatsController(dbContext);

          var cat = new CatInputModel(){
            Name = "Piet"
          };

          var result = await controller.PostCat(cat);
          var createdAtActionResult = result.Result as CreatedAtActionResult;
          var resultCat = createdAtActionResult.Value as Cat;

          Assert.AreEqual(cat.Name, resultCat.Name);
        }
    }
}
