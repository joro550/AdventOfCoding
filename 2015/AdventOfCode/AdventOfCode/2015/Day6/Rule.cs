using System.Linq;

namespace AdventOfCode._2015.Day6
{
    public abstract record Rule(Coordinates From, Coordinates To)
    {
        protected abstract void Execute(Coordinates coords, LightGrid grid);

        public virtual LightGrid ExecuteRule(LightGrid grid)
        {
            for (var x = From.X; x <= To.X; x++)
            {
                for (var y = From.Y; y <= To.Y; y++)
                {
                    Execute(new Coordinates(x, y), grid);
                }
            }

            return grid;
        }
    }
    
    public record TurnOnRule(Coordinates From, Coordinates To) : Rule(From, To)
    {
        protected override void Execute(Coordinates coords, LightGrid grid)
            => grid.Execute(new TurnOnCommand(), coords);
    }
    
    public record TurnOffRule(Coordinates From, Coordinates To): Rule(From, To)
    {
        protected override void Execute(Coordinates coords, LightGrid grid) 
            => grid.Execute(new TurnOffCommand(), coords);
    }
    
    public record ToggleRule(Coordinates From, Coordinates To): Rule(From, To)
    {
        protected override void Execute(Coordinates coords, LightGrid grid) 
            => grid.Execute(new ToggleCommand(), coords);
    }
    
    
    public static class RuleInterpreter
    {
        public static Rule Eval(string code)
        {
            var codeParts = code.Split(" ");

            if (codeParts[0] == "turn")
            {
                var from = Coordinates.FromString(codeParts[2]);
                var to = Coordinates.FromString(codeParts[4]);
                
                if (codeParts[1] == "on")
                    return new TurnOnRule(from, to);
                return new TurnOffRule(from, to);
            }

            var toggleFrom = Coordinates.FromString(codeParts[1]);
            var toggleTo = Coordinates.FromString(codeParts[3]);
            return new ToggleRule(toggleFrom, toggleTo);
        }
    }

    public record Coordinates(int X, int Y)
    {
        public static Coordinates FromString(string codePart)
        {
            var coords = codePart.Split(",");
            return new Coordinates(int.Parse(coords[0]), int.Parse(coords[1]));
        }
    }

    public record Light
    {
        public bool TurnedOn { get; }
        public long Brightness { get; set; }

        public Light(bool turnedOn = false) => TurnedOn = turnedOn;
    };

    public record LightGrid
    {
        private readonly Light[,] _lightGrid;

        private LightGrid(int xMax, int yMax) => _lightGrid = new Light[xMax, yMax];

        public static LightGrid Create(int xMax, int yMax) 
            => new(xMax, yMax);

        public void Execute(LightCommand command, Coordinates coordinates)
        {
            var light = _lightGrid[coordinates.X, coordinates.Y] ?? new Light();
            _lightGrid[coordinates.X, coordinates.Y] = command.Execute(light);
        }

        public int GetLights() 
            => _lightGrid.Cast<Light>().Count(light => light is { TurnedOn: true });
        
        public long GetLightBrightness() 
            => _lightGrid.Cast<Light>().Where(x=> x!= null).Sum(light => light.Brightness);
    }
    
    public abstract record LightCommand
    {
        public abstract Light Execute(Light light);
    }

    public record TurnOnCommand : LightCommand
    {
        public override Light Execute(Light light)
        {
            light.Brightness++;
            return light;
        }
    }

    public record TurnOffCommand() : LightCommand()
    {
        public override Light Execute(Light light)
        {
            if (light.Brightness == 0)
                return light;
            
            light.Brightness--;
            return light;
        }
    }

    public record ToggleCommand() : LightCommand()
    {
        public override Light Execute(Light light)
        {
             light.Brightness += 2;
             return light;
        }
    }
}