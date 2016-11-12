namespace ML.Web
{
    public interface IServiceFactory
    {
        TService GetService<TService>();
    }
}
