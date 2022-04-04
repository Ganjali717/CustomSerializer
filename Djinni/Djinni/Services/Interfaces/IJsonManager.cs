namespace Djinni.Services.Interfaces
{
    public interface IJsonManager
    {
        string CustomSerializer(object value);

        T DeSerialize<T>(string serializeData, T target) where T : new();
    }
}
