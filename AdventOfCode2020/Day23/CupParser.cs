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
            
            // Pick up cups
            for (int i = 0, positionToPickUp = _currentPosition + 1; i < Moves; i++, positionToPickUp++)
            {
                if (positionToPickUp >= _currentCollection.Length)
                    positionToPickUp = 0;

                pickedUpCups.Add(_currentCollection[positionToPickUp]);
            }

            // Reposition array
            var newLength = _currentCollection.Length - Moves;
            var newCircle = new long[newLength];
            
            for (int i = 0, replaceVal = 0, positionToPickUp = _currentPosition + 1; replaceVal < newLength; i++)
            {
                if (positionToPickUp >= _currentCollection.Length)
                    break;
            
                if (i >= positionToPickUp && i < positionToPickUp + Moves)
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
            var newNewCircle = new long[_originalSize];

            // place cups in new positions
            for(int i = _currentPosition, arrayPosition = 0;  arrayPosition < _currentCollection.Length; i++,arrayPosition++)
            {
                if (i >= _originalSize)
                    i = 0;
                
                newNewCircle[i] = _currentCollection[arrayPosition];
            }
            
            // Add the picked up cups after the destination
            var destinationIndex = GetDestinationIndex(destination, newNewCircle)+1;
            for (var i = 0; i < cups.Length; i++, destinationIndex ++)
            {
                if (destinationIndex >= _originalSize)
                    destinationIndex = 0;

                // Shift values to the right
                var cache = newNewCircle.ToArray();
                
                for (int j = destinationIndex, newIndex = destinationIndex + 1; newIndex != destinationIndex; j++, newIndex++)
                {
                    if (newIndex >= _originalSize) 
                        newIndex = 0;

                    if (j >= _originalSize)
                        j = 0;

                    newNewCircle[newIndex] = cache[j];
                }
                
                newNewCircle[destinationIndex] = cups[i];
            }
            
            
            Array.Resize(ref _currentCollection, _originalSize);
            _currentCollection = newNewCircle.ToArray();
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
                if (returnId == 0)
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