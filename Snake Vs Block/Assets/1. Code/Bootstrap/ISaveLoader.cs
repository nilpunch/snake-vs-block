namespace Snake.Boot
{
    public interface ISaveLoader<T>
    {
        void Save(T data);
        T Load();
    }
}