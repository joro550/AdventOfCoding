namespace AdventOfCode2020.Day23
{
    public interface ICupCollection
    {
        int[] PickUpCups(in int moves);
        void PlaceCupsDown(in int destination, in int[] cups);
        int GetDestination(in int id);
        int GetCurrentCup();
        void IncreasePosition();
        int[] GetCurrentCups(in int fromCupNumber);
    }
}