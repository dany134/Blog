namespace Blog.API.DTOs
{
    public class PostResponse<T>
    {
        public PostResponse(T data, int count)
        {
            Data = data;
            Count = count;
        }
    
        public T Data { get; set; }
        public int Count { get; set; }

        public static PostResponse<T> Create(T data, int count) 
        {
            return new PostResponse<T>(data, count);
        }



    }
}
