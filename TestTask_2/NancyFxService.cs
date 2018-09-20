using Nancy;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace TestTask_2
{
    public interface INancyFxService
    {
        ActionResult<List<WordBook>> GetAll(List<WordBook> wordBookList);
        ActionResult<WordBook> GetById(object id, List<WordBook> wordBookList);
        ActionResult<HttpStatusCode> PostAddItem(object obj, List<WordBook> wordBookList);
        ActionResult<HttpStatusCode> DeleteItem(object obj, List<WordBook> wordBookList);
    }

    public class NancyFxService : INancyFxService
    {
        public ActionResult<List<WordBook>> GetAll(List<WordBook> wordBookList)
        {
            return wordBookList.ToList();
        }

        public ActionResult<WordBook> GetById(object id, List<WordBook> wordBookList)
        {
            var item = wordBookList.Find(WordBook => WordBook.Id == (id as WordBook).Id);

            if (item != null)
                return item;
            else
                return null;
        }

        public ActionResult<HttpStatusCode> PostAddItem(object obj, List<WordBook> wordBookList)
        {
            var word = obj as WordBook;

            if (word.Id < 0 || word.Name == "")
            {
                return HttpStatusCode.RequestedRangeNotSatisfiable;
            }
            else
            {
                var item = wordBookList.Find(WordBook => WordBook.Id == word.Id);

                if (item != null)
                {
                    wordBookList[wordBookList.FindIndex(x => x.Id == word.Id)].Name = word.Name;
                    return HttpStatusCode.Accepted;
                }
                else
                {
                    wordBookList.Add(new WordBook { Id = word.Id, Name = word.Name });
                    return HttpStatusCode.Created;
                }
            }
        }

        public ActionResult<HttpStatusCode> DeleteItem(object obj, List<WordBook> wordBookList)
        {
            var item = wordBookList.Find(WordBook => WordBook.Id == (obj as WordBook).Id);

            if (item != null)
            {
                wordBookList.Remove(item);
                return HttpStatusCode.Accepted;
            }
            else
            {
                return HttpStatusCode.NotFound;
            }
        }
    }
}