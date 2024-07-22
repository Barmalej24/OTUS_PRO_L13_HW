namespace OTUS_PRO_L13_HW.Test
{
    public interface ISrializable<T>
    {
        string Serialize(T entity);
        T Deserialize(string path);
    }
}