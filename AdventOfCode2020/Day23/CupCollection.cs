using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020.Day23
{
    public record CupCollection
    {
        private const int Moves = 3;
        private long[] _currentCollection;
        private int _currentPosition;
        private readonly int _originalSize;
        
        public CupCollection(IEnumerable<long> cups)
        {
            _currentCollection = cups.ToArray();
            _originalSize = _currentCollection.Length;
        }

        public long[] PickUpCups()
        {
            var pickedUpCups = new List<long>();
            var positionsToPickup = new HashSet<int>();

            // Pick up cups
            for (int i = 0, positionToPickUp = _currentPosition + 1; i < Moves; i++, positionToPickUp++)
            {
                if (positionToPickUp >= _currentCollection.Length)
                    positionToPickUp = 0;

                pickedUpCups.Add(_currentCollection[positionToPickUp]);
                positionsToPickup.Add(positionToPickUp);
            }

            // Reposition array
            var newLength = _currentCollection.Length - Moves;
            var newCircle = new long[newLength];
            
            for (int i = 0, replaceVal = 0; replaceVal < newLength; i++)
            {
                if (positionsToPickup.TryGetValue(i, out _))
                    continue;
            
                if (i >= _currentCollection.Length)
                    break;
            
                newCircle[replaceVal] = _currentCollection[i];
                replaceVal++;
            }
            
            Array.Resize(ref _currentCollection, _currentCollection.Length - 3);
            _currentCollection = newCircle;
            
            // Return the picked up cups
            return pickedUpCups.ToArray();
        }

        public void PlaceCupsDown(long destination, long[] cups)
        {
            var newCircle = new long[_originalSize];

            static int Increase(int position, int size)
            {
                var toReturn = position + 1;
                if (toReturn >= size)
                    toReturn = 0;
                return toReturn;
            }

            var currentPosition = _currentPosition >= _currentCollection.Length
                ? _currentCollection.Length - 1
                : _currentPosition;
            
            var newArrayPosition = _currentPosition == _originalSize ? 0 : _currentPosition;
            
            for (var i = 0; 
                i <= _currentCollection.Length; 
                i++, 
                currentPosition = Increase(currentPosition, _currentCollection.Length), 
                newArrayPosition = Increase(newArrayPosition, _originalSize))
            {
                // Check to see if the last number we added was the "destination"
                var positionToCheck = newArrayPosition - 1 < 0 ? newCircle.Length - 1 : newArrayPosition - 1; 
                if (newCircle[positionToCheck] == destination)
                {
                    // If it was we need to add the picked up cups here
                    foreach (var cup in cups)
                    {
                        newCircle[newArrayPosition] = cup;
                        newArrayPosition = Increase(newArrayPosition, _originalSize);
                    }
                    
                    // Add from the remaining cups on the floor
                    if(i < _currentCollection.Length)
                        newCircle[newArrayPosition] = _currentCollection[currentPosition];
                    
                    continue;
                }
                
                // Add from the remaining cups on the floor
                if(i < _currentCollection.Length)
                    newCircle[newArrayPosition] = _currentCollection[currentPosition];
            }
            
            
            Array.Resize(ref _currentCollection, _originalSize);
            _currentCollection = newCircle.ToArray();
        }

        public long GetDestination(long id)
        {
            var returnId = id - 1;
            while (_currentCollection.All(x => x != returnId))
            {
                returnId--;
                if (returnId <= 0)
                    returnId = _currentCollection.Max();
            }

            return returnId;
        }

        public long GetCurrentCup() 
            => _currentCollection[_currentPosition];

        public void IncreasePosition()
        {
            _currentPosition++;
            if (_currentPosition >= _currentCollection.Length)
                _currentPosition = 0;
        }

        public long[] GetCurrentCups() 
            => _currentCollection;
    }
}