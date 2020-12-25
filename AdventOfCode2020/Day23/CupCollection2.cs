using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020.Day23
{
    public class CupCollection2 : ICupCollection
    {
        private int _currentPosition;
        private readonly int _originalSize;
        
        private int[] _currentCollection;
        private int[] _currentCollectionWithoutPicks; 
        
        public CupCollection2(IEnumerable<int> cups)
        {
            _currentCollection = cups.ToArray();
            
            _originalSize = _currentCollection.Length;
        }

        public int[] PickUpCups(in int moves)
        {
            var pickedUpCups = new int[moves];
            var positionsToPickup = new HashSet<int>();

            // Pick up cups
            for (int i = 0, positionToPickUp = _currentPosition + 1; i < moves; i++, positionToPickUp++)
            {
                if (positionToPickUp >= _currentCollection.Length)
                    positionToPickUp = 0;

                pickedUpCups[i] = _currentCollection[positionToPickUp];
                positionsToPickup.Add(positionToPickUp);
            }

            // Reposition array
            var newLength = _currentCollection.Length - moves;
            var newCircle = new int[newLength];
            
            for (int i = 0, replaceVal = 0; replaceVal < newLength; i++)
            {
                if (positionsToPickup.TryGetValue(i, out _))
                    continue;
            
                if (i >= _currentCollection.Length)
                    break;

                newCircle[replaceVal] = _currentCollection[i];
                replaceVal++;
            }

            _currentCollectionWithoutPicks = newCircle;
            
            // Return the picked up cups
            return pickedUpCups;
        }

        public void PlaceCupsDown(in int destination, in int[] cups)
        {
            static int Increase(in int position, in int size)
            {
                var toReturn = position + 1;
                if (toReturn >= size) 
                    toReturn = 0;
                return toReturn;
            }
            
            var newArrayPosition = _currentPosition == _originalSize ? 0 : _currentPosition;

            var newCircle = new int[_originalSize];

            for (int i = 0,
                currentPosition = _currentPosition >= _currentCollectionWithoutPicks.Length
                    ? _currentCollectionWithoutPicks.Length - 1
                    : _currentPosition; 
                i <= _currentCollectionWithoutPicks.Length; 
                i++, 
                currentPosition = Increase(currentPosition, _currentCollectionWithoutPicks.Length), 
                newArrayPosition = Increase(newArrayPosition, _originalSize))
            {
                // Check to see if the last number we added was the "destination"
                var positionToCheck = newArrayPosition - 1 < 0 ? newCircle.Length - 1 : newArrayPosition - 1; 
                if (newCircle[positionToCheck] == destination)
                {
                    // If it was we need to add the picked up cups here
                    // ReSharper disable once ForCanBeConvertedToForeach - Performance
                    for (var index = 0; index < cups.Length; index++)
                    {
                        newCircle[newArrayPosition] = cups[index];
                        newArrayPosition = Increase(newArrayPosition, _originalSize);
                    }

                    // Add from the remaining cups on the floor
                    if(i < _currentCollectionWithoutPicks.Length)
                        newCircle[newArrayPosition]= _currentCollectionWithoutPicks[currentPosition];
                    
                    continue;
                }
                
                // Add from the remaining cups on the floor
                if(i < _currentCollectionWithoutPicks.Length)
                    newCircle[newArrayPosition]= _currentCollectionWithoutPicks[currentPosition];
            }

            _currentCollection = newCircle;
        }

        public int GetDestination(in int id)
        {
            var returnId = id - 1;
            while (_currentCollectionWithoutPicks.All(x => x != returnId))
            {
                returnId--;
                if (returnId <= 0)
                    returnId = _currentCollectionWithoutPicks.Max();
            }

            return returnId;
        }

        public int GetCurrentCup()
            => _currentCollection[_currentPosition];

        public void IncreasePosition()
        {
            _currentPosition++;
            if (_currentPosition >= _currentCollection.Length)
                _currentPosition = 0;
        }

        public int[] GetCurrentCups(in int fromCupNumber)
        {
            var numberToStartFrom = fromCupNumber;
            var cupIndex = _currentCollection
                .Select((value, index) => new {Value = value, Index = index})
                .Single(x => x.Value == numberToStartFrom).Index;

            var returnState = new List<int>();
            for (var i = 0; i < _currentCollection.Length; i++, cupIndex++)
            {
                if (cupIndex == _currentCollection.Length)
                    cupIndex = 0;
                returnState.Add(_currentCollection[cupIndex]);
            }

            return returnState.ToArray();
        } 
    }
    
    
    public static class EnumerableThings
    {
        public static IEnumerable<(T item, int index)> WithIndex<T>(this IEnumerable<T> source) => source.Select((item, index) => (item, index));
    }
}