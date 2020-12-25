using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020.Day23
{
    public class LinkedCupCollection : ICupCollection
    {
        private LinkedListNode<int> _currentPosition;
        private readonly LinkedList<int> _currentState;
        private Dictionary<int, LinkedListNode<int>> _stateDictionary;

        public LinkedCupCollection(IEnumerable<int> currentState)
        {
            var collection = currentState.ToArray();
            _currentState = new LinkedList<int>(collection);

            _stateDictionary =  BuildDictionary();

            _currentPosition = _currentState.First ?? throw new Exception();
        }

        private Dictionary<int, LinkedListNode<int>> BuildDictionary()
        {
            var dictionary = new Dictionary<int, LinkedListNode<int>>();
            
            var node = _currentState.First;
            for (var i = 0; i < _currentState.Count; i++)
            {
                dictionary.Add(node.Value, node);
                node = node.Next;
            }
            return dictionary;
        }

        public int[] PickUpCups(in int moves)
        {
            var pickedUpCups = new int[moves];
            
            var currentPosition = _currentPosition;
            for (var i = 0; i < moves; i++)
            {
                var nextValue = currentPosition?.Next ?? _currentState.First ?? throw new Exception();
                pickedUpCups[i] = nextValue.Value;
                _currentState.Remove(nextValue);
            }

            return pickedUpCups;
        }

        public void PlaceCupsDown(in int destination, in int[] cups)
        {
            var node = _stateDictionary[destination] ?? throw new Exception("Something went wrong");
            
            // ReSharper disable once ForCanBeConvertedToForeach
            for (var index = 0; index < cups.Length; index++)
            {
                _currentState.AddAfter(node, cups[index]);
                node = node.Next ?? throw new Exception("Something went wrong");
                _stateDictionary[node.Value] = node;
            }
        }
        
        public void PlaceCupsDown2(in int destination, in int[] cups)
        {
            var node = _stateDictionary[destination] ?? throw new Exception("Something went wrong");
            
            // ReSharper disable once ForCanBeConvertedToForeach
            for (var index = 0; index < cups.Length; index++)
            {
                _currentState.AddAfter(node, cups[index]);
                node = node.Next ?? throw new Exception("Something went wrong");
                _stateDictionary[node.Value] = node;
            }
        }

        public int GetDestination(in int id)
        {
            var returnId = id - 1;
            while (!_stateDictionary.ContainsKey(returnId))
            {
                returnId--;
                if (returnId <= 0)
                    returnId = _stateDictionary.Keys.Max();
            }

            return returnId;
        }

        public int GetDestination2(in int id, int[] pickedUpCups)
        {
            var returnId = id - 1;
            while (pickedUpCups.Contains(returnId) || !_stateDictionary.ContainsKey(returnId))
            {
                returnId--;
                if (returnId <= 0)
                    returnId = _stateDictionary.Count + 1;
            }

            return returnId;
        }
        public int GetCurrentCup()
        {
            return _currentPosition?.Value ?? 0;
        }

        public void IncreasePosition()
        {
            _currentPosition = _currentPosition?.Next ?? _currentState.First ?? throw new Exception();
        }

        public int[] GetCurrentCups(in int fromCupNumber)
        {
            var returnCups = new List<int>();
            var node = _currentState.Find(fromCupNumber) ?? throw new Exception("Something went wrong");
            
            // ReSharper disable once ForCanBeConvertedToForeach
            for (var i = 0; i < _currentState.Count; i++)
            {
                returnCups.Add(node.Value);
                node = node.Next ?? _currentState.First ?? throw new Exception("Something went wrong");
            }

            return returnCups.ToArray();
        }
    }
}