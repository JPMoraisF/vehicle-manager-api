namespace VehicleManager.Models
{
    public class ServiceResponse<T> 
    {
        public bool HasErrors { get { return ErrorList.Any(); } set { ErrorList.Any(); } }

        public List<string> ErrorList { get; set; } = new List<string>();

        public T? Data { get; set; }
    }
}
