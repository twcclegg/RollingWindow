using System;

namespace RollingWindowExtremes
{
    public class RollingWindowExtremes<T> where T : IComparable
    {
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

        private bool initilized;

        private readonly T[] _rollWinArray;


        //====================================== my initialy proposed caching algo
        public void AddItem(T currentNum)
        {
            if (!initilized)
            {
                _low = currentNum;
                _high = currentNum;
                initilized = true;
            }

            _rollWinArray[_currentLocation] = currentNum;

            if (currentNum.CompareTo(_low) <= 0)
            {
                _lowLocation = _currentLocation;
                _low = currentNum;
            }
            else if (_currentLocation == _lowLocation)
                ReFindLowest();

            if (currentNum.CompareTo(_high) >= 0)
            {
                _highLocation = _currentLocation;
                _high = currentNum;
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