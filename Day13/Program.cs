﻿using System;
using System.Collections.Immutable;
using System.IO;
using System.Linq;

namespace Day13
{
    class Program
    {
        static void Main(string[] args)
        {
            var notes = File.ReadAllLines("data.txt");

            var buses = notes[1].Split(',')
              .Select((value, offset) => (value, offset))
              .Where(pair => pair.value != "x")
              .Select(pair => new Bus(long.Parse(pair.value), pair.offset))
              .ToImmutableList();

            var arrival = long.Parse(notes[0]);

            var (nextBusId, _) = buses
              .OrderBy(bus => GetWaitTime(bus.Id))
              .First();

            long GetWaitTime(long schedule) =>
              arrival / schedule * schedule + schedule - arrival;

            var (baseBusId, _) = buses[0];
            var (time, period) = (baseBusId, baseBusId);

            foreach (var (schedule, offset) in buses.Skip(1))
            {
                while ((time + offset) % schedule != 0) time += period;

                period *= schedule;
            }

            Console.WriteLine(nextBusId * GetWaitTime(nextBusId));
            Console.WriteLine(time);
        }
        internal record Bus(long Id, int Offset);
    }
    
}
