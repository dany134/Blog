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
                        Tags = "tag1,tag2,tag3",
                        CreatedAt = DateTime.Now,
                        UpdatedAt = DateTime.Now
                    };
                    context.BlogPosts.Add(blog);
                    var blog2 = new BlogPost
                    {
                        Slug = "test-slug-1",
                        Body = "Test Slug 1",
                        Title = "This is a test title ",
                        Description = "Description.",
                        Tags = "A1, A2, A3",
                        CreatedAt = DateTime.Now,
                        UpdatedAt = DateTime.Now
                    };
                    context.BlogPosts.Add(blog2);
                    var blog3 = new BlogPost
                    {
                        Slug = "test-slug-2",
                        Body = "This is a second test body for seeding.",
                        Title = "Test Slug 2",
                        Description = "Second description.",
                        Tags = "B1, B2, B3",
                        CreatedAt = DateTime.Now,
                        UpdatedAt = DateTime.Now
                    };
                    context.BlogPosts.Add(blog3);

                }
                if (!context.Comments.Any()) 
                {
                    var comment = new Comment
                    {
                        Body = "Comment on first post",
                        Slug = "augmented-reality-ios-application",
                        CreatedAt = DateTime.Now,
                        UpdatedAt = DateTime.Now
                    };
                    var comment2 = new Comment
                    {
                        Body = "Second comment on first post",
                        Slug = "augmented-reality-ios-application",
                        CreatedAt = DateTime.Now,
                        UpdatedAt = DateTime.Now
                    };
                    context.Comments.Add(comment2);

                    var comment3 = new Comment
                    {
                        Body = "First comment on second post",
                        Slug = "test-slug-1",
                        CreatedAt = DateTime.Now,
                        UpdatedAt = DateTime.Now
                    };
                    context.Comments.Add(comment3);

                }
                await context.SaveChangesAsync();

            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
    }
}
