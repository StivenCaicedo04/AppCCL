using System;
using System.Collections.Generic;

namespace AppCCL.Models;

public partial class Producto
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public int Stock { get; set; }

    public DateTime? Fechacreacion { get; set; }

    public virtual ICollection<MovimientosInventario> MovimientosInventarios { get; set; } = new List<MovimientosInventario>();
}
