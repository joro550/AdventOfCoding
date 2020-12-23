using System;
using System.Linq;
using System.Collections.Generic;

namespace AdventOfCode2020.Day23
{
    public static class CupParser
    {
        public static CupCollection Parse(string input) 
            => new(input.Select(c => long.Parse(c.ToString())));
    }

    public class CrabCups
    {
        private readonly CupCollection _cups;

        public CrabCups(CupCollection cups) 
            => _cups = cups;

        public void PlayRound()
        {
            var currentCupId = _cups.GetCurrentCup();
            var cupsPickedUp = _cups.PickUpCups();
            var destination = _cups.GetDestination(currentCupId, cupsPickedUp);

            _cups.PlaceCupsDown(destination, cupsPickedUp);
            _cups.IncreasePosition();
        }

        public long[] GetState() 
            => _cups.GetCurrentCups();
    }

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
            var positionsToPickup = new List<int>();

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
                if (positionsToPickup.Any(x => x == i))
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

            int Increase(int position, int size)
            {
                var toReturn = position + 1;
                if (toReturn >= size)
                    toReturn = 0;
                return toReturn;
            }

            var destinationIndex = GetDestinationIndex(destination, _currentCollection);


            var currentPosition = _currentPosition >= _currentCollection.Length
                ? _currentPosition - _currentCollection.Length
                : _currentPosition;
            
            
            
            var newArrayPosition = _currentPosition == _originalSize ? 0 : _currentPosition;
            
            for (var i = 0; 
                i < _currentCollection.Length; 
                i++, 
                currentPosition = Increase(currentPosition, _currentCollection.Length), 
                newArrayPosition = Increase(newArrayPosition, _originalSize))
            {
                
                if (currentPosition == destinationIndex)
                {
                    newCircle[newArrayPosition] = destination;
                    newArrayPosition = Increase(newArrayPosition, _originalSize);
                    
                    foreach (var cup in cups)
                    {
                        newCircle[newArrayPosition] = cup;
                        newArrayPosition = Increase(newArrayPosition, _originalSize);
                    }

                    if(newArrayPosition != _originalSize)
                        newArrayPosition--;
                    
                    continue;
                }

                newCircle[newArrayPosition] = _currentCollection[currentPosition];
            }
            
            
            Array.Resize(ref _currentCollection, _originalSize);
            _currentCollection = newCircle.ToArray();
        }

        private int GetDestinationIndex(long destination, long[] listToSearch)
        {
            var destinationIndex = 0;
            for (var i = 0; i < listToSearch.Length; i++)
            {
                if (listToSearch[i] != destination)
                    continue;

                destinationIndex = i;
                break;
            }
            return destinationIndex;
        }

        public long GetDestination(long id, IEnumerable<long> pickedUpCups)
        {
            var returnId = id - 1;
            var max = _currentCollection.Max();
            var cupsToExclude = pickedUpCups.ToArray();
            
            while (cupsToExclude.Any(x=> x == returnId) || _currentCollection.All(x => x != returnId))
            {
                returnId--;
                if (returnId <= 0)
                    returnId = max;
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
    

    public record Cup(long Id);
}