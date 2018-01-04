using System;

namespace TheWorld.Models
{
    public class Stop
    {
        // these fields match up with 
        public int Id { get; set; }
        public string Name { get; set; }
        public double Latitude { get; set; }
        public double Longitude {get; set;}

        public int Order { get; set; }
        public DateTime Arrival { get; set; }

    }
}