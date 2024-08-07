namespace ScaneqCuencaBackend.Models.ResponseModels
{
    public class GenericResponse<T>
    {
        public string Message { get; set; } = "null";
        public T? Model { get; set; }
        public int Code { get; set; }
    }
}
