using Events.Domain.Interfaces;
using Events.Domain.ValueObjetcs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Events.Domain.Entities
{
    public class CurrentLocation : AuditableEntity
    {
        public int UserId { get; set; }
        public Coordinate CoordinateA { get; set; }
        public Coordinate CoordinateB { get; set; }
    }
}
