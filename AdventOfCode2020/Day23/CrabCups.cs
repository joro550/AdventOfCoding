using System.Collections.Generic;

namespace AdventOfCode2020.Day23
{
    public class CrabCups
    {
        private readonly CupCollection _cups;

        public CrabCups(CupCollection cups) 
            => _cups = cups;

        public CrabCups(IEnumerable<long> cups)
        {
            _cups = new CupCollection(cups);
        }

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
}