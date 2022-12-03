using Blog.Contracts;
using Moq;
using System.Net.NetworkInformation;
using System;
using Blog.DAL.Entities;
using FluentAssertions;
using Blog.Service.Posts.Query;
using Blog.Service.Posts.Command;

namespace Blog.Service.Tests
{
    public class BlogPostTests
    {
       
        private readonly Mock<IBlogPostRepository> _RepoMock = new Mock<IBlogPostRepository>();

        [Fact]
        public async Task Posts_ShouldReturnList()
        {
            var handler = new QueryAllPosts.Handler(_RepoMock.Object);
            //Arrange
            var posts = new List<BlogPost>
            {
                new BlogPost {Id = 1,Slug ="augmented-reality-ios-application-2", Title = "Augmented Reality iOS Application", Body = "The app is simple to use, and will help you decide on your best furniture fit",
                Description = "Rubicon Software Development and Gazzda furniture are proud to launch an augmented reality app", Tags = "tag1, tag2", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now},
                new BlogPost {Id = 2,Slug ="internet-trends-2018", Title = "Internet Trends 2018", Body = "Test body",
                Description = "Test desc", Tags = "test tag1, test tag2", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now},


            }.AsEnumerable();
            string tag = "";

            _RepoMock.Setup(x => x.GetPostsAsync(tag)).Returns(Task.FromResult(posts));
            //Act
            var request = new QueryAllPosts.Query { Tag = tag };
            var result = await handler.Handle(request, CancellationToken.None);
            //Assert

            result.Should().BeEquivalentTo(posts);

        }
        [Fact]
        public async Task Posts_ShouldNotReturn()
        {
            //Arrange
            var posts = new List<BlogPost>().AsEnumerable();
            string tag = "";
            var handler = new QueryAllPosts.Handler(_RepoMock.Object);
            var request = new QueryAllPosts.Query { Tag = tag };

            _RepoMock.Setup(x => x.GetPostsAsync(tag)).Returns(Task.FromResult(posts));
            //Act
            var result = await handler.Handle(request, CancellationToken.None);

            //Assert
            result.Should().BeEmpty();
        }
        [Fact]
        public async Task Post_ShouldGetbyId()
        {

            //Arrange
            var handler = new QueryPostsBySlug.Handler(_RepoMock.Object);
            var slug = "test-slug";
            var post = new BlogPost()
            {
                Id = 1,
                Title = "Test Title",
                Body = "This is a test body",
                Slug = slug,
                Description = "This is a description",
                Tags = "tag1,tag2",
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };

            _RepoMock.Setup(x => x.GetPostBySlugAsync(slug)).ReturnsAsync(post);
            var request = new QueryPostsBySlug.Query { Slug = slug };
            //Act
            BlogPost result = await handler.Handle(request, CancellationToken.None);

            //Assert
            result.Slug.Should().Be(slug);
        }
        [Fact]
        public async Task Post_ShouldBeDeleted()
        {
            //Arrange
            var handler = new DeletePost.Handler(_RepoMock.Object);
            var slug = "test-slug";
            var request = new DeletePost.Command { Slug = slug };

            _RepoMock.Setup(x => x.DeletePostAsync(slug)).ReturnsAsync(true);

            //Act
            var result = await handler.Handle(request, CancellationToken.None);

            //Assert
            result.IsSuccess.Should().BeTrue();
        }
        [Fact]
        public async Task Post_ShouldBeCreated()
        {
            //Arrange
            var post = new BlogPost()
            {
                Id = 1,
                Title = "Test Title",
                Body = "This is a test body",
                Slug = "test-title",
                Description = "This is a description",
                Tags = "tag1,tag2",
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };
            var request = new CreatePost.Command { BlogPost = post };
            var handler = new CreatePost.Handler(_RepoMock.Object);

            _RepoMock.Setup(x => x.InsertPostAsync(post)).ReturnsAsync(true);

            //Act
            var result = await handler.Handle(request, CancellationToken.None);

            //Assert
            result.IsSuccess.Should().BeTrue();
        }
    }
}