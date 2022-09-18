using System.Collections.Generic;

namespace SideWars.Shared.Utils
{
    /// Creates a SlidingWindowList containing n elements, n being windowSize.
    /// list = [0, ..., n], 0 being earliest, and n being latest added element.
    public class SlidingWindowList<T> : List<T>
    {
        private int windowSize;

        public SlidingWindowList(int windowSize) : base(windowSize)
        {
            this.windowSize = windowSize;
        }

        public new void Add(T item)
        {
            if (base.Count >= windowSize)
                base.RemoveAt(0);

            base.Add(item);
        } 
    }
}
