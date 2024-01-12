using System;
using System.Collections.Generic;
using System.Text;

namespace apiproyecto
{
    public class Plato
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public double Precio { get; set; }
        public bool Disponible { get; set; }
        public int RestauranteId { get; set; }
    }
}
