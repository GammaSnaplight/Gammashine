namespace Gammashine
{
    public interface IActress<T, K>
    {
        public T Act(K data);
    }

    public interface IElegantActress<T, K> : IActress<T, K>
         where K : IFoldables<K>
    {
        public K Actress { get; set; }
    }
}
