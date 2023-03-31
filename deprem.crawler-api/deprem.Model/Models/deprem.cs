using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetTopologySuite.Geometries;

namespace deprem.Model.Models
{
    public class Deprem
    {
        public int Id { get; set; }
        public DateTime? tarih { get; set; }

        public DateTime saat { get; set; }

        public double enlem { get; set; }
        public double boylam  { get; set; }
        public Point Location { get; set; }
        public double derinlik { get; set; }
        public double md { get; set; }
        public double ml { get; set; }
        public double mw { get; set; }
        public string? yer { get; set; }
        public string? cozumNiteliği { get; set; }
    }
}
