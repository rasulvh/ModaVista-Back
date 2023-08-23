using Microsoft.AspNetCore.Mvc;
using ModaVista_Back.Services.Interfaces;
using ModaVista_Back.ViewModels;

namespace ModaVista_Back.Controllers
{
    public class BlogController : Controller
    {
        private readonly IBlogService _blogService;
        private readonly IBlogCategoryService _blogCategoryService;

        public BlogController(IBlogService blogService,
                              IBlogCategoryService blogCategoryService)
        {
            _blogService = blogService;
            _blogCategoryService = blogCategoryService;
        }

        public async Task<IActionResult> Index()
        {
            var blogs = await _blogService.GetAllAsync();
            var miniBlogs = await _blogService.GetAllAsync();
            var blogCategories = await _blogCategoryService.GetAllAsync();

            foreach (var item in miniBlogs)
            {
                string[] words = item.Heading.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                // Select the first 5 words
                int numberOfWords = Math.Min(5, words.Length);
                string[] firstFourWords = words.Take(numberOfWords).ToArray();

                // Join the selected words with spaces
                string shortenedHeading = string.Join(" ", firstFourWords);

                // Check if the heading is not exactly 5 words
                if (words.Length != 5)
                {
                    // Append three dots to the result
                    shortenedHeading += "...";
                }

                item.Heading = shortenedHeading;
            }


            BlogVM model = new()
            {
                BlogCategories = blogCategories,
                Blogs = blogs.OrderByDescending(m => m.Id).Take(6).ToList(),
                MiniBlogs = miniBlogs.OrderByDescending(m => m.Id).Take(4).ToList()
            };

            return View(model);
        }
    }
}
