using System;

namespace RollingWindow
{
    public class RollingWindowExtremes<T> where T : IComparable
    {
        /// <summary>
        /// Computes the max and min of a stream of IComparables in O(1) time
        /// </summary>
        /// <param name="window">The size of the window to keep the max and min for</param>
        public RollingWindowExtremes(int window)
        {
            _window = window;
            _rollWinArray = new T[_window];
        }

        private readonly int _window;

        private T _low;
        private int _lowLocation;
        private T _high;
        private int _highLocation;
        private int _currentLocation;

        private bool _initilized;

        private readonly T[] _rollWinArray;


        /// <summary>
        /// Adds a new item to the list as the newest, the oldest one is removed
        /// and the max and min are recalculated.
        /// </summary>
        /// <param name="item"></param>
        public void AddItem(T item)
        {
            if (!_initilized)
            {
                _low = item;
                _high = item;
                _initilized = true;
            }

            _rollWinArray[_currentLocation] = item;

            if (item.CompareTo(_low) <= 0)
            {
                _lowLocation = _currentLocation;
                _low = item;
            }
            else if (_currentLocation == _lowLocation)
                ReFindLowest();

            if (item.CompareTo(_high) >= 0)
            {
                _highLocation = _currentLocation;
                _high = item;
            }
            else if (_currentLocation == _highLocation)
                ReFindHeighest();

            _currentLocation++;
            if (_currentLocation == _window) _currentLocation = 0; //this is faster
            //CurrentLocation = CurrentLocation % Window; //this is slower

        }

        //full iteration run each time lowest is overwritten.
        private void ReFindLowest()
        {
            _low = _rollWinArray[0];
            _lowLocation = 0;
            for (var i = 1; i < _window; i++)
            {
                if (_rollWinArray[i].CompareTo(_low) >= 0)
                    continue;
                _low = _rollWinArray[i];
                _lowLocation = i;
            }
        }

        //full iteration run each time heighest is overwritten.
        private void ReFindHeighest()
        {
            _high = _rollWinArray[0];
            _highLocation = 0;
            for (var i = 1; i < _window; i++)
            {
                if (_rollWinArray[i].CompareTo(_high) <= 0)
                    continue;
                _high = _rollWinArray[i];
                _highLocation = i;
            }
        }

        public T GetCurrentLow() => _low;

        public T GetCurrentHigh() => _high;

    }
}