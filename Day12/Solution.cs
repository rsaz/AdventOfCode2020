using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using System.IO;

namespace Day12
{
    class Solution
    {
        enum Actions
        {
            North = 'N',
            South = 'S',
            East = 'E',
            West = 'W',
            Left = 'L',
            Right = 'R',
            Forward = 'F'
        }

        public static readonly Vector2 NORTH = new Vector2(1, 0);
        public static readonly Vector2 EAST = new Vector2(0, 1);
        public static readonly Vector2 SOUTH = new Vector2(-1, 0);
        public static readonly Vector2 WEST = new Vector2(0, -1);

        class Instruction
        {
            public Actions Operation { get; set; }
            public int Value { get; set; }
        }

        public static void StartA()
        {
            var lines = File.ReadAllLines("data.txt");

            var instructions = lines.Select(x => new Instruction
            {
                Operation = (Actions)x[0],
                Value = int.Parse(x.Substring(1))
            }).ToList();

            Vector2 direction = EAST;
            Vector2 movement = new Vector2();

            foreach (var instruction in instructions)
            {
                switch (instruction.Operation)
                {
                    case Actions.North:
                        movement += NORTH * instruction.Value;
                        break;

                    case Actions.South:
                        movement += SOUTH * instruction.Value;
                        break;

                    case Actions.East:
                        movement += EAST * instruction.Value;
                        break;

                    case Actions.West:
                        movement += WEST * instruction.Value;
                        break;

                    case Actions.Left:
                        {
                            var value = instruction.Value / 90;

                            for (int i = 0; i < value; i++)
                            {
                                var temp = direction;
                                direction.X = temp.Y;
                                direction.Y = -temp.X;
                            }
                            break;
                        }

                    case Actions.Right:
                        {
                            var value = instruction.Value / 90;

                            for (int i = 0; i < value; i++)
                            {
                                var temp = direction;
                                direction.X = -temp.Y;
                                direction.Y = temp.X;
                            }
                            break;
                        }

                    case Actions.Forward:
                        movement += direction * instruction.Value;
                        break;
                }
            }

            int answer = (int)(Math.Abs(movement.X) + Math.Abs(movement.Y));

            Console.WriteLine($"Day 12A: {answer}");
        }

        public static void StartB()
        {
            //var lines = File.ReadAllLines("Content\\Day12_Test.txt");
            var lines = File.ReadAllLines("data.txt");

            var instructions = lines.Select(x => new Instruction
            {
                Operation = (Actions)x[0],
                Value = int.Parse(x.Substring(1))
            }).ToList();

            Vector2 waypoint = 10 * EAST + 1 * NORTH;
            Vector2 ship = new Vector2();

            foreach (var instruction in instructions)
            {
                switch (instruction.Operation)
                {
                    case Actions.North:
                        waypoint += NORTH * instruction.Value;
                        break;

                    case Actions.South:
                        waypoint += SOUTH * instruction.Value;
                        break;

                    case Actions.East:
                        waypoint += EAST * instruction.Value;
                        break;

                    case Actions.West:
                        waypoint += WEST * instruction.Value;
                        break;

                    case Actions.Left:
                        {
                            var value = instruction.Value / 90;

                            for (int i = 0; i < value; i++)
                            {
                                var temp = waypoint;
                                waypoint.X = temp.Y;
                                waypoint.Y = -temp.X;
                            }
                            break;
                        }

                    case Actions.Right:
                        {
                            var value = instruction.Value / 90;

                            for (int i = 0; i < value; i++)
                            {
                                var temp = waypoint;
                                waypoint.X = -temp.Y;
                                waypoint.Y = temp.X;
                            }
                            break;
                        }

                    case Actions.Forward:
                        ship += waypoint * instruction.Value;
                        break;
                }
            }

            int answer = (int)(Math.Abs(ship.X) + Math.Abs(ship.Y));

            Console.WriteLine($"Day 12B: {answer}");

        }

    }
}
