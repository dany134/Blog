namespace Blog.API.DTOs
{
    public class PostResponse<T>
    {
        public PostResponse(T data, int count)
        {
            BlogPosts = data;
            PostCount = count;
        }
    
        public T BlogPosts { get; set; }
        public int PostCount { get; set; }

        public static PostResponse<T> Create(T data, int count) 
        {
            return new PostResponse<T>(data, count);
        }



    }
}
