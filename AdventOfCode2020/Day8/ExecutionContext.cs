using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020.Day8
{
    public class ExecutionContext
    {
        public int CurrentOperation { get; set; }
        public int Accumulator { get; set; }
        public bool Complete { get; private set; }
        
        private readonly List<int> _lineNumbers = new();

        public void Execute(Operation operation)
        {
            operation.Execute(this);
            
            if(_lineNumbers.All(ln => ln != CurrentOperation))
                _lineNumbers.Add(CurrentOperation);
            else
                Complete = true;
        }
        
    }
}