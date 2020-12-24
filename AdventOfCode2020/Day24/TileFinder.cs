using System;
using System.Collections.Generic;

namespace AdventOfCode2020.Day24
{
    public static class TileFinder
    {
        public static Dictionary<string, Tile> FindTiles(string input)
        {
            var tiles = new Dictionary<string, Tile>();
            
            foreach (var line in input.Split(Environment.NewLine))
            {
                var position = new Position(0, 0);


                for (var i = 0; i < line.Length; i++)
                {
                    var direction = line[i].ToString();
                    switch (direction)
                    {
                        case "n":
                            i++;
                            direction += line[i].ToString();
                            break;
                        case "s":
                            i++;
                            direction += line[i].ToString();
                            break;
                    }

                    position = direction switch
                    {
                        "nw" => position with {X = position.X + 0.5f, Y = position.Y - 0.5f},
                        "ne" => position with {X = position.X + 0.5f, Y = position.Y + 0.5f},
                        "e" => position with {Y = position.Y + 1f},
                        "sw" => position with {X = position.X - 0.5f, Y = position.Y - 0.5f},
                        "se" => position with {X = position.X - 0.5f, Y = position.Y + 0.5f},
                        "w" => position with {Y = position.Y - 1f},
                        _ => position
                    };
                }
                
                if (tiles.ContainsKey(position.GetKey()))
                {
                    tiles[position.GetKey()] = tiles[position.GetKey()].FlipColour();
                }
                else
                {
                    tiles.Add(position.GetKey(), new Tile(position, Colour.Black));
                }
            }              
            
            return tiles;
        }
    }
}