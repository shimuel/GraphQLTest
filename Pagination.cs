using System;
using System.Collections.Generic;
using System.Linq;

namespace GraphQLTest
{
    public interface IPaginate<T>
    {
        int From { get; }

        int Index { get; }

        int Size { get; }

        int Count { get; }

        int Pages { get; }

        IList<T> Items { get; }

        bool HasPrevious { get; }

        bool HasNext { get; }
    }


    public class PaginatedGraphType<T> :  IPaginate<T>
    {
        public PaginatedGraphType(IEnumerable<T> source, int index, int size, int from)
        {
            var enumerable = source as T[] ;//? source.ToArray();

            if (from > index)
                throw new ArgumentException($"indexFrom: {from} > pageIndex: {index}, must indexFrom <= pageIndex");

            size = source.Count() > 0 && size == -1 ? source.Count() : size;

            Index = index;
            Size = size;
            From = from;

            Count = enumerable.Count();
            Pages = (int)Math.Ceiling(Count / (double)Size);

            Items = enumerable.Count() > 0 ? enumerable.Skip((Index - From) * Size).Take(Size).ToList() : enumerable.ToList();
        
        }

        public PaginatedGraphType()
        {
            Items = new T[0];
        }

        public int From { get; set; }
        public int Index { get; set; }
        public int Size { get; set; }
        public int Count { get; set; }
        public int Pages { get; set; }
        public IList<T> Items { get; set; }
        public bool HasPrevious => Index - From > 0;
        public bool HasNext => Index - From + 1 < Pages;
    }

    
}