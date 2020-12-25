using System.Collections.Generic;

namespace AdventOfCode2020.Day23
{
    public class CrabCups
    {
        private readonly ICupCollection _cups;

        public CrabCups(ICupCollection cups) 
            => _cups = cups;

        public CrabCups(IEnumerable<int> cups)
        {
            _cups = new CupCollection(cups);
        }

        public void PlayRound(int moves)
        {
            var currentCupId = _cups.GetCurrentCup();
            var cupsPickedUp = _cups.PickUpCups(moves);
            var destination = _cups.GetDestination(currentCupId);

            _cups.PlaceCupsDown(destination, cupsPickedUp);
            _cups.IncreasePosition();
        }

        public int[] GetState(int fromCupNumber) 
            => _cups.GetCurrentCups(fromCupNumber);
    }
    
    public class CrabCups2
    {
        private readonly LinkedCupCollection _cups;

        public CrabCups2(LinkedCupCollection cups) 
            => _cups = cups;

        public void PlayRound(int moves)
        {
            var currentCupId = _cups.GetCurrentCup();
            var cupsPickedUp = _cups.PickUpCups(moves);
            var destination = _cups.GetDestination2(currentCupId, cupsPickedUp);

            _cups.PlaceCupsDown2(destination, cupsPickedUp);
            _cups.IncreasePosition();
        }

        public int[] GetState(int fromCupNumber) 
            => _cups.GetCurrentCups(fromCupNumber);
    }
}