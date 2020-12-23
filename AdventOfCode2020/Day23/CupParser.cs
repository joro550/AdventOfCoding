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
            var destination = _cups.GetDestination(currentCupId);

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
        private int _currentPosition = 0;

        public CupCollection(IEnumerable<long> cups) 
            => _currentCollection = cups.ToArray();

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
            var newLength = _currentCollection.Length + cups.Length;
            var newCircle = new long[newLength];

            for (int i = 0, newArrayPosition =0; i < _currentCollection.Length; i++, newArrayPosition++)
            {
                if (_currentCollection[i] == destination)
                {
                    newCircle[newArrayPosition] = _currentCollection[i];
                    newArrayPosition++;
                    
                    foreach (var cup in cups)
                    {
                        newCircle[newArrayPosition] = cup;
                        newArrayPosition++;
                    }
                    newArrayPosition--;
                    continue;
                }
                
                newCircle[newArrayPosition] = _currentCollection[i];
            }
            
            Array.Resize(ref _currentCollection, newLength);
            _currentCollection = newCircle;
        }

        public long GetDestination(long id)
        {
            var max = _currentCollection.Max();
            var returnId = id - 1;
            while (_currentCollection.All(x => x != returnId))
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