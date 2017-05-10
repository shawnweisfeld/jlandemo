﻿using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.OData;
using Microsoft.Azure.Mobile.Server;
using jlanmobiletestService.DataObjects;
using jlanmobiletestService.Models;
using System.Net;
using System.Data.Entity;

namespace jlanmobiletestService.Controllers
{
    public class AuthorController : TableController<Author>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            jlanmobiletestContext context = new jlanmobiletestContext();
            DomainManager = new EntityDomainManager<Author>(context, Request, true);
        }

        // GET tables/Author
        public IQueryable<Author> GetAllAuthors()
        {
            return Query();
        }

        // GET tables/Author/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public SingleResult<Author> GetAuthor(string id)
        {
            return Lookup(id);
        }

        // PATCH tables/Author/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task<Author> PatchAuthor(string id, Delta<Author> patch)
        {
            return UpdateAsync(id, patch);
        }

        // POST tables/Author
        public async Task<IHttpActionResult> PostAuthor(Author item)
        {
            Author current = await InsertAsync(item);
            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        // DELETE tables/Author/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task<int> DeleteAuthor(string id)
        {
            jlanmobiletestContext context = new jlanmobiletestContext();

            var author = context.Authors
                .Include(x => x.TodoItems)
                .SingleOrDefault(x => x.Id == id);

            if (author == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            author.Deleted = true;

            foreach (var todo in author.TodoItems)
            {
                todo.Deleted = true;
            }

            return context.SaveChangesAsync();
        }
    }
}