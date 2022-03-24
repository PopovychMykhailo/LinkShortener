using System;
using Microsoft.AspNetCore.Mvc;
using LinkShortener.Resource.Domain.Entities.Implimentations;
using LinkShortener.Resource.Domain.Repositories.Interfaces;
using LinkShortener.Resource.Services.Interfaces;
using LinkShortener.Resource.Services;
using LinkShortener.Resource.Models;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using LinkShortener.Resource.Database;

namespace LinkShortener.Resource.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class MainController : ControllerBase
    {
        private IShortLinkGenerator Generator { get; set; }
        //private IBaseRepository<LinkItem> LinkItems { get; set; }
        private AppDbContext Context;

        private Guid UserId => Guid.Parse(User.Claims.Single(c => c.Type == ClaimTypes.NameIdentifier).Value);




        public MainController(IShortLinkGenerator generator, AppDbContext context)
        {
            Generator = generator;
            Context = context;
        }


        [HttpPost(nameof(Create))]
        [Authorize]
        public IActionResult Create(CreateLinkItemModel model)
        {
            // Validation
            if (model.LongLink == null)
                return BadRequest(new { error_text = "LongLink cannot be empty!" });

            // Add to DB
            LinkItem newEntity = new(UserId, model.LongLink);
            Context.Links.Add(newEntity);
            Context.SaveChanges();

            return RedirectToLinkItemById(newEntity.Id);
        }

        [HttpGet(nameof(AllLinks))]
        [Authorize]
        public IActionResult AllLinks()
        {
            var list = Context.Links.Where(l => l.UserId == UserId);
            return new JsonResult(list);
        }

        [HttpGet(nameof(Link) + "/{id}")]
        [Authorize]
        public IActionResult Link(Guid id)
        {
            // Existence check
            var link = Context.Links.Where(l => l.UserId == UserId && l.Id == id);
            if (link == null)
                return NotFound(new { error_text = "Wrong ID of the link" });

            return new JsonResult(link);
        }

        [HttpPut(nameof(Update) + "/{id}")]
        [Authorize]
        public IActionResult Update(Guid id, LinkItemModel model)
        {
            // Existence check
            var toUpdate = Context.Links.FirstOrDefault(l => l.UserId == UserId && l.Id == id);
            if (toUpdate == null)
                return NotFound(new { error_text = "Wrong ID of the link" });

            // Change the LinkItem
            toUpdate.LongLink = model.LongLink;
            Context.Links.Update(toUpdate);
            Context.SaveChanges();

            return RedirectToLinkItemById(id);
        }

        [HttpDelete(nameof(Delete) + "/{id}")]
        [Authorize]
        public IActionResult Delete(Guid id)
        {
            // Existence check
            var toDelete = Context.Links.FirstOrDefault(l => l.UserId == UserId && l.Id == id);
            if (toDelete == null)
                return NotFound(new { error_text = "Wrong ID of the link" });

            // Remove
            Context.Links.Remove(toDelete);
            Context.SaveChanges();

            return Ok(new { message_text = "The link successfully deleted!" });
        }

        [HttpDelete(nameof(AllDelete))]
        [Authorize]
        public IActionResult AllDelete()
        {
            var list = Context.Links.Where(l => l.UserId == UserId);
            foreach (var item in list)
                Context.Links.Remove(item);
            Context.SaveChanges();

            return Ok(new { message_text = "All links successfully deleted!" });
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
