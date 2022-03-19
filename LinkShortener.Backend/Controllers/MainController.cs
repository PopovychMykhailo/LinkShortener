using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using LinkShortener.Backend.Domain.Entities.Implimentations;
using LinkShortener.Backend.Domain.Repositories.Interfaces;
using LinkShortener.Backend.Services.Interfaces;
using LinkShortener.Backend.Services;
using LinkShortener.Backend.Models;
using System.Net;

namespace LinkShortener.Backend.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class MainController : ControllerBase
    {
        private IShortLinkGenerator Generator { get; set; }
        private IBaseRepository<LinkItem> LinkItems { get; set; }



        public MainController(IShortLinkGenerator generator, IBaseRepository<LinkItem> links)
        {
            Generator = generator;
            LinkItems = links;
        }


        [HttpPost("Create")]        // Create
        public IActionResult Create(LinkItemModel model)
        {
            // Validation
            if (model.LongLink == null)
                return BadRequest();

            // Add to DB
            LinkItem newEntity = Mapping(model, Guid.NewGuid());
            LinkItems.Create(newEntity);

            return RedirectToLinkItemById(newEntity.Id);
        }

        [HttpGet("AllLinks")]       // Read all LinkItems
        public JsonResult AllLinks()
        {
            return new JsonResult(LinkItems.GetAll());
        }

        [HttpGet("Link/{id}")]      // Read one LinkItem
        public JsonResult Link(Guid id)
        {

            return new JsonResult(LinkItems.GetById(id));
        }

        [HttpPut("Update/{id}")]    // Update
        public IActionResult Put(Guid id, LinkItemModel model)
        {
            // Existence check
            var toUpdate = LinkItems.GetById(id);
            if (toUpdate == null)
                return BadRequest("Wrong ID of the link");

            toUpdate.LongLink = model.LongLink;
            LinkItems.Update(toUpdate);

            return RedirectToLinkItemById(id);
        }

        [HttpDelete("AllDelete")]   // Delete all
        public IActionResult AllDelete()
        {
            var list = LinkItems.GetAll();
            foreach (var item in list)
                LinkItems.Delete(item.Id);

            return Ok();
        }

        [HttpDelete("Delete/{id}")] // Delete
        public IActionResult Delete(Guid id)
        {
            var toDelete = LinkItems.GetById(id);
            if (toDelete == null)
                return NotFound();

            LinkItems.Delete(id);
            return Ok();
        }



        private LinkItem Mapping(LinkItemModel model, Guid userId)
        {
            return new LinkItem()
            {
#warning Брати UserId з токену користувача
                UserId = userId,
                Id = Guid.NewGuid(),
                LongLink = model.LongLink,
                ShortLink = Guid.NewGuid().ToString(),
                CreatedDate = DateTime.Now,
            };
        }

        private RedirectToActionResult RedirectToLinkItemById(Guid id)
        {
            return RedirectToAction(
                  nameof(MainController.Link)
                , nameof(MainController).CutController()
                , new { id = id });
        }
    }
}
