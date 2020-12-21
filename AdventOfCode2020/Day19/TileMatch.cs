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
            var startingTile = tileMatches.Values.First(x => x.Matches.Count == 2);
            var ourTileId = tileMatches.Values.First(x => x.Matches.Count == 2).Tile.Id;
            var cornerType = startingTile.GetCornerType();
            
            // Do the top/bottom row
            var direction = cornerType == CornerType.TopLeft || cornerType == CornerType.BottomLeft
                ? Side.Right
                : Side.Left;
            
            var previousTile = startingTile;
            var tile = tileMatches[startingTile.Matches[direction].Tile.Id];
            do
            {
                tileMatches[tile.Tile.Id] = tileMatches[tile.Tile.Id].HandleTopOrBottom(cornerType, previousTile.Tile.Id);
                previousTile = tile;
                
                tile = tile.Matches.ContainsKey(direction) ? tileMatches[tile.Matches[direction].Tile.Id] : null;
            } while (tile != null);
            
            // Do the sides
            var sideType = cornerType == CornerType.TopLeft || cornerType == CornerType.TopRight
                ? Side.Left
                : Side.Right;
            
            previousTile = startingTile;
            tile = tileMatches[startingTile.Matches[direction].Tile.Id];
            direction = cornerType == CornerType.TopLeft || cornerType == CornerType.TopRight
                ? Side.Bottom
                : Side.Top;
            
            do
            {
                tileMatches[tile.Tile.Id] = tileMatches[tile.Tile.Id].HandleLeftOrRight(cornerType, sideType, previousTile.Tile.Id);
                previousTile = tile;
                
                tile = tile.Matches.ContainsKey(direction) ? tileMatches[tile.Matches[direction].Tile.Id] : null;
            } while (tile != null);
            
            
            // Go vertically flipping things
            var horizontal = cornerType == CornerType.TopLeft || cornerType == CornerType.BottomLeft
                ? Side.Right
                : Side.Left;
            
            tile = tileMatches[startingTile.Matches[direction].Tile.Id];
            previousTile = tile;
            do
            {
                var cacheTile = tile;
                do
                {
                    tileMatches[tile.Tile.Id] = tileMatches[tile.Tile.Id].HandleVerticalCenter(cornerType, previousTile.Tile.Id);
                    previousTile = tile;
                    tile = tile.Matches.ContainsKey(direction) ? tileMatches[tile.Matches[direction].Tile.Id] : null;
                } while (tile != null);
                

                tile = cacheTile.Matches.ContainsKey(horizontal) ? tileMatches[cacheTile.Matches[horizontal].Tile.Id] : null;
            } while (tile != null);
            
            // Go horizontal flipping things
            tile = tileMatches[startingTile.Matches[direction].Tile.Id];
            previousTile = tile;
            do
            {
                var cacheTile = tile;
                do
                {
                    tileMatches[tile.Tile.Id] = tileMatches[tile.Tile.Id]
                        .HandleHorizontalCenter(cornerType, previousTile.Tile.Id);
                    previousTile = tile;
                    tile = tile.Matches.ContainsKey(horizontal) ? tileMatches[tile.Matches[horizontal].Tile.Id] : null;
                } while (tile != null);


                tile = cacheTile.Matches.ContainsKey(direction)
                    ? tileMatches[cacheTile.Matches[direction].Tile.Id]
                    : null;
            } while (tile != null);

            return tileMatches;
        }
        
        private TileMatch HandleHorizontalCenter(CornerType cornerType, long id)
        {
            var direction = cornerType == CornerType.TopLeft || cornerType == CornerType.BottomLeft
                ? Side.Right
                : Side.Left;
            var tile = this;

            if (!Matches.ContainsKey(direction) || Matches[direction].Tile.Id != id)
                tile = tile.FlipHorizontally();
            return tile;
        }

        private TileMatch HandleVerticalCenter(CornerType cornerType, long id)
        {
            var verticalDirection = cornerType == CornerType.TopLeft || cornerType == CornerType.TopRight
                ? Side.Top
                : Side.Bottom;
            var tile = this;
            if (!Matches.ContainsKey(verticalDirection) || Matches[verticalDirection].Tile.Id != id) 
                tile = RotateUntil(verticalDirection, id);
            return tile;
        }

        private TileMatch HandleLeftOrRight(CornerType cornerType, Side side, long id)
        {
            var verticalDirection = cornerType == CornerType.TopLeft || cornerType == CornerType.TopRight
                ? Side.Top
                : Side.Bottom;
            var horizontalDirection = side == Side.Left ? Side.Left : Side.Right;
             
            var tile = this;
            if (!Matches.ContainsKey(verticalDirection) || Matches[verticalDirection].Tile.Id != id) 
                tile = RotateUntil(verticalDirection, id);
            
            if (tile.Matches.ContainsKey(horizontalDirection)) 
                tile.FlipHorizontally();
            return tile;
        }


        private TileMatch HandleTopOrBottom(CornerType cornerType, long id)
        {
            var horizontalDirection = cornerType == CornerType.TopLeft || cornerType == CornerType.BottomLeft
                ? Side.Left
                : Side.Right;

            var direction = cornerType == CornerType.TopLeft || cornerType == CornerType.TopRight
                ? Side.Top
                : Side.Bottom;

            var tile = this;
            if (!Matches.ContainsKey(horizontalDirection) || Matches[horizontalDirection].Tile.Id != id)
                tile = RotateUntil(horizontalDirection, id);
                
            if (tile.Matches.ContainsKey(direction)) 
                tile.FlipVertically();

            return tile;
        }


        private CornerType GetCornerType()
        {
            if (!Matches.ContainsKey(Side.Left))
                return Matches.ContainsKey(Side.Top) ? CornerType.BottomLeft : CornerType.TopLeft;
            return Matches.ContainsKey(Side.Top) ? CornerType.BottomRight : CornerType.TopRight; 
        }

        private bool WeAreASidePiece()
            => Matches.Count == 3;

        private bool WeAreACornerPiece() 
            => Matches.Count == 2;

        private enum CornerType
        {
            TopLeft,
            TopRight,
            BottomLeft,
            BottomRight
        }
    }
}