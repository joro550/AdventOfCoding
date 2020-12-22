using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020.Day19
{
    public record TileMatch(Tile Tile, Dictionary<Side, (Tile Tile, bool WasFlipped)> Matches)
    {
        public void AddSideMatch(Side side, Tile match, bool wasFlipped)
        {
            if (!Matches.ContainsKey(side))
            {
                Matches.Add(side, (match, wasFlipped));
            }
        }

        private TileMatch TurnLeft()
        {
            var tile = Tile.RotateLeft();
            
            var valueTuples = new Dictionary<Side, (Tile Tile, bool WasFlipped)>();
            valueTuples = AddIfPresent(valueTuples, Side.Top, Side.Right);
            valueTuples = AddIfPresent(valueTuples, Side.Right, Side.Bottom);
            valueTuples = AddIfPresent(valueTuples, Side.Left, Side.Top);
            valueTuples = AddIfPresent(valueTuples, Side.Bottom, Side.Left);
            
            return new TileMatch(tile, valueTuples);
        }

        private TileMatch FlipHorizontally()
        {
            var tile = Tile.FlipHorizontally();
            
            var valueTuples = new Dictionary<Side, (Tile Tile, bool WasFlipped)>();
            valueTuples = AddIfPresent(valueTuples, Side.Top, Side.Top);
            valueTuples = AddIfPresent(valueTuples, Side.Right, Side.Left);
            valueTuples = AddIfPresent(valueTuples, Side.Left, Side.Right);
            valueTuples = AddIfPresent(valueTuples, Side.Bottom, Side.Bottom);

            return new TileMatch(tile, valueTuples);
        }

        private TileMatch FlipVertically()
        {
            var tile = Tile.FlipVertically();
            var valueTuples = new Dictionary<Side, (Tile Tile, bool WasFlipped)>();
            valueTuples = AddIfPresent(valueTuples, Side.Top, Side.Bottom);
            valueTuples = AddIfPresent(valueTuples, Side.Right, Side.Right);
            valueTuples = AddIfPresent(valueTuples, Side.Left, Side.Left);
            valueTuples = AddIfPresent(valueTuples, Side.Bottom, Side.Bottom);
            return new TileMatch(tile, valueTuples);
        }

        private Dictionary<Side, (Tile Tile, bool WasFlipped)> AddIfPresent(Dictionary<Side, (Tile Tile, bool WasFlipped)> dictionary, Side oldSide, Side newSide)
        {
            if (Matches.ContainsKey(oldSide))
            {
                // Now that we've flipped it it doesn't need flipping again
                dictionary[newSide] = (Matches[oldSide].Tile, false);
            }
            return dictionary;
        }

        public (TileMatch matches , Dictionary<long, TileMatch> tileMatches) HandleVertical(Side side, Dictionary<long, TileMatch> tileMatches)
        {
            var tile = Matches[side];

            tileMatches = CheckConnections(tileMatches);
            return !tile.WasFlipped ? (this, tileMatches) : (FlipVertically(), tileMatches);
        }

        private Dictionary<long, TileMatch> CheckConnections(Dictionary<long, TileMatch> tileMatches)
        {
            if (Matches.ContainsKey(Side.Right))
                tileMatches[Matches[Side.Right].Tile.Id] = tileMatches[Matches[Side.Right].Tile.Id].CheckCorrectSide(Side.Right, Tile.Id);
            if (Matches.ContainsKey(Side.Left))
                tileMatches[Matches[Side.Left].Tile.Id] = tileMatches[Matches[Side.Left].Tile.Id].CheckCorrectSide(Side.Left, Tile.Id);
            
            if (Matches.ContainsKey(Side.Top))
                tileMatches[Matches[Side.Top].Tile.Id] = tileMatches[Matches[Side.Top].Tile.Id].CheckCorrectSide(Side.Top, Tile.Id);
            if(Matches.ContainsKey(Side.Bottom))
                tileMatches[Matches[Side.Bottom].Tile.Id] = tileMatches[Matches[Side.Bottom].Tile.Id].CheckCorrectSide(Side.Bottom, Tile.Id);

            return tileMatches;
        }

        private TileMatch CheckCorrectSide(Side direction, long tileId)
        {
            return direction switch
            {
                Side.Right when !Matches.ContainsKey(Side.Left) || Matches[Side.Left].Tile.Id != tileId => RotateUntil(Side.Left, tileId),
                Side.Left when !Matches.ContainsKey(Side.Right) || Matches[Side.Right].Tile.Id != tileId => RotateUntil(Side.Right, tileId),
                Side.Top when !Matches.ContainsKey(Side.Bottom) || Matches[Side.Bottom].Tile.Id != tileId => RotateUntil(Side.Bottom, tileId),
                Side.Bottom when !Matches.ContainsKey(Side.Top) || Matches[Side.Top].Tile.Id != tileId => RotateUntil(Side.Top,tileId),
                _ => this
            };
        }

        private TileMatch RotateUntil(Side direction, long titleId)
        {
            var match = this;
            while (!match.Matches.ContainsKey(direction) || match.Matches.ContainsKey(direction) && match.Matches[direction].Tile.Id != titleId)
            {
                match = match.TurnLeft();
            }

            return match;
        }
        
        
        public (TileMatch matches , Dictionary<long, TileMatch> tileMatches) HandleHorizontal(Side side, Dictionary<long, TileMatch> tileMatches)
        {
            var tile = Matches[side];
            
            tileMatches = CheckConnections(tileMatches);
            return !tile.WasFlipped ? (this, tileMatches) : (FlipHorizontally(), tileMatches);
        }

        public static Dictionary<long, TileMatch> Handle(Dictionary<long, TileMatch> tileMatches)
        {
            // get top left tile
            var topLeftTile = tileMatches.Values.Single(x =>
                !x.Matches.ContainsKey(Side.Top) && x.Matches.ContainsKey(Side.Left));

            // Orient the top row
            var tile = topLeftTile;
            var previousTile = tile;
            while (tile != null)
            {
                tileMatches[tile.Tile.Id] = tile.HandleTopRow(previousTile.Tile.Id);
                
                // Go down and turn tiles so they match
                var cacheTile = tile;
                for (; cacheTile != null;)
                {
                    tileMatches[cacheTile.Tile.Id] = cacheTile.HandlePiece(previousTile.Tile.Id);
                    previousTile = cacheTile;
                    
                    cacheTile = cacheTile.Matches.ContainsKey(Side.Bottom) 
                        ? tileMatches[cacheTile.Matches[Side.Bottom].Tile.Id] 
                        : null;
                }
                
                // Go to the next tile;
                previousTile = tile;
                tile = tile.Matches.ContainsKey(Side.Right) ? tileMatches[tile.Matches[Side.Right].Tile.Id] : null;
            }


            tile = tileMatches[topLeftTile.Matches[Side.Bottom].Tile.Id];
            previousTile = tile;

            for (; tile != null;)
            {
                var cacheTile = tile;
                for (int x = 0; cacheTile != null;x ++)
                {

                    tileMatches[cacheTile.Tile.Id] = cacheTile.HandleHorizontal(x == 0, previousTile.Tile.Id);
                    previousTile = cacheTile;
                    
                    cacheTile = cacheTile.Matches.ContainsKey(Side.Right) 
                        ? tileMatches[cacheTile.Matches[Side.Right].Tile.Id] 
                        : null;
                }
                
                tile = tile.Matches.ContainsKey(Side.Bottom) ? tileMatches[tile.Matches[Side.Bottom].Tile.Id] : null;
            }

            return tileMatches;
        }

        private TileMatch HandleHorizontal(bool isSidePiece, long previousId)
        {
            switch (isSidePiece)
            {
                case true when !Matches.ContainsKey(Side.Left):
                case false when Matches[Side.Left].Tile.Id != previousId:
                    return FlipHorizontally();
            }

            return this;
        }
        
        
        private TileMatch HandlePiece(long previousId)
        {
            var tileMatch = this;

            var rotateCheck = Side.Top;

            if (!tileMatch.Matches.ContainsKey(rotateCheck) ||
                tileMatch.Matches.ContainsKey(rotateCheck) && tileMatch.Matches[rotateCheck].Tile.Id != previousId)
            {
                tileMatch = RotateUntil(rotateCheck, previousId);
            }

            return tileMatch;
        }

        private TileMatch HandleTopRow(long leftId)
        {
            var tileMatch = this;

            if (!tileMatch.Matches.ContainsKey(Side.Left) ||
                tileMatch.Matches.ContainsKey(Side.Left) && tileMatch.Matches[Side.Left].Tile.Id != leftId)
            {
                tileMatch = RotateUntil(Side.Left, leftId);
            }

            if (tileMatch.Matches.ContainsKey(Side.Top)) 
                tileMatch = FlipVertically();

            return tileMatch;
        }
    }
}