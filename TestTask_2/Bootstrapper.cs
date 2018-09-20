using Nancy;
using System.Collections.Generic;

namespace TestTask_2
{
    public class Bootstrapper : DefaultNancyBootstrapper
    {
        public static List<WordBook> WordBookLists { get; set; }

        public Bootstrapper()
        {
            WordBookLists = new List<WordBook>();
            for (var i = 0; i < 10; i++)
            {
                var word = new WordBook
                {
                    Id = i + 1,
                    Name = "Word #" + (i + 1)
                };
                WordBookLists.Add(word);
            }
        }
    }
}