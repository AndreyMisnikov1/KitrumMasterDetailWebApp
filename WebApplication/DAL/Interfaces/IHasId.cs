namespace DAL.Interfaces
{
    using System;

    public interface IHasId<out T> where T : IEquatable<T>
    {
        T Id { get; }
    }
}
