using Blog.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.DAL
{
    public class SeedBlogs
    {
        public static async Task Seed(BlogContext context) 
        {
            if (context == null)
            {
                return;
            }
            try
            {
                if (!context.BlogPosts.Any()) 
                {
                    var blog = new BlogPost
                    {
                        Slug = "augmented-reality-ios-application",
                        Body = "The app is simple to use, and will help you decide on your best furniture fit.",
                        Title = "Augmented Reality iOS Application",
                        Description = "Rubicon Software Development and Gazzda furniture are proud to launch an augmented reality app.",
                        CreatedAt = DateTime.Now,
                        UpdatedAt = DateTime.Now
                    };
                    context.BlogPosts.Add(blog);
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
    }
}
