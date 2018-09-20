using System;
using Nancy;
using Nancy.ModelBinding;

namespace TestTask_2
{
    public class NancyFxModule : NancyModule
    {
        public NancyFxModule(INancyFxService nancyFxService)
        {
            Get("/", args => "The first ten elements created a constructor");

            Get("/api/wordbook", async args => await Response.AsJson(nancyFxService.GetAll(Bootstrapper.WordBookLists).Value));

            Get("/api/{id}", async args =>
            {
                try
                {
                    var wordbook = nancyFxService.GetById(this.Bind<WordBook>() as object, Bootstrapper.WordBookLists);

                    if (wordbook != null)
                        return await Response.AsJson(wordbook.Value);
                    else
                        return await Response.AsJson(HttpStatusCode.NoContent.ToString(), HttpStatusCode.NoContent);
                }
                catch (Exception ex)
                {
                    return await Response.AsJson(HttpStatusCode.UnsupportedMediaType.ToString(), HttpStatusCode.UnsupportedMediaType);
                }
            });

            Post("/api/add", async args =>
            {
                try
                {
                    var wordbook = nancyFxService.PostAddItem(this.Bind<WordBook>() as object, Bootstrapper.WordBookLists);
                    return await Response.AsJson(wordbook.Value.ToString(), wordbook.Value);
                }
                catch (Exception ex)
                {
                    return await Response.AsJson(HttpStatusCode.UnsupportedMediaType.ToString(), HttpStatusCode.UnsupportedMediaType);
                }
            });

            Delete("/api/del", async args =>
            {
                try
                {
                    var wordbook = nancyFxService.DeleteItem(this.Bind<WordBook>() as object, Bootstrapper.WordBookLists);
                    return await Response.AsJson(wordbook.Value.ToString(), wordbook.Value);
                }
                catch (Exception ex)
                {
                    return await Response.AsJson(HttpStatusCode.UnsupportedMediaType.ToString(), HttpStatusCode.UnsupportedMediaType);
                }
            });
        }
    }
}