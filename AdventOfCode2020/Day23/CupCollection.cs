using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020.Day23
{
    public record CupCollection : ICupCollection
    {
        private const int Moves = 3;
        private HashSet<int> _currentCollection;
        private int _currentPosition;
        private readonly int _originalSize;
        
        public CupCollection(IEnumerable<int> cups)
        {
            _currentCollection = new HashSet<int>(cups);
            _originalSize = _currentCollection.Count;
        }

        public int[] PickUpCups(in int moves)
        {
            var pickedUpCups = new List<int>();
            var positionsToPickup = new HashSet<int>();
            var currentCollection = _currentCollection.ToArray();

            // Pick up cups
            for (int i = 0, positionToPickUp = _currentPosition + 1; i < moves; i++, positionToPickUp++)
            {
                if (positionToPickUp >= currentCollection.Length)
                    positionToPickUp = 0;

                pickedUpCups.Add(currentCollection[positionToPickUp]);
                positionsToPickup.Add(positionToPickUp);
            }

            // Reposition array
            var newLength = currentCollection.Length - moves;
            var newCircle = new int[newLength];
            
            for (int i = 0, replaceVal = 0; replaceVal < newLength; i++)
            {
                if (positionsToPickup.TryGetValue(i, out _))
                    continue;
            
                if (i >= currentCollection.Length)
                    break;

                newCircle[replaceVal] = currentCollection[i];
                replaceVal++;
            }

            _currentCollection = new HashSet<int>(newCircle);
            
            // Return the picked up cups
            return pickedUpCups.ToArray();
        }

        public void PlaceCupsDown(in int destination, in int[] cups)
        {
            var newCircle = new int[_originalSize];
            var currentCollection = _currentCollection.ToArray();

            static int Increase(int position, int size)
            {
                var toReturn = position + 1;
                if (toReturn >= size)
                    toReturn = 0;
                return toReturn;
            }

            var currentPosition = _currentPosition >= currentCollection.Length
                ? currentCollection.Length - 1
                : _currentPosition;
            
            var newArrayPosition = _currentPosition == _originalSize ? 0 : _currentPosition;
            
            for (var i = 0; 
                i <= currentCollection.Length; 
                i++, 
                currentPosition = Increase(currentPosition, currentCollection.Length), 
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
                    if(i < currentCollection.Length)
                        newCircle[newArrayPosition] = currentCollection[currentPosition];
                    
                    continue;
                }
                
                // Add from the remaining cups on the floor
                if(i < currentCollection.Length)
                    newCircle[newArrayPosition] = currentCollection[currentPosition];
            }
            
            _currentCollection = new HashSet<int>(newCircle);
        }

        public int GetDestination(in int id)
        {
            var returnId = id - 1;
            while (!_currentCollection.TryGetValue(returnId, out _))
            {
                returnId--;
                if (returnId <= 0)
                    returnId = _currentCollection.Max();
            }

            return returnId;
        }

        public int GetCurrentCup()
            => _currentCollection.ElementAt(_currentPosition);

        public void IncreasePosition()
        {
            _currentPosition++;
            if (_currentPosition >= _currentCollection.Count)
                _currentPosition = 0;
        }

        public int[] GetCurrentCups(in int fromCupNumber) 
            => _currentCollection.ToArray();
    }
}