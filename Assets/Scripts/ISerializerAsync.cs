using System.Threading.Tasks;

public interface ISerializerAsync<T>
{
    public void  SerializeAsync(T data);
    public Task<T> DeserializeAsync();
}