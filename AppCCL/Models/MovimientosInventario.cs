using System;
using System.Collections.Generic;

namespace AppCCL.Models;

public partial class MovimientosInventario
{
    public int Id { get; set; }

    public int ProductoId { get; set; }

    public string? TipoMovimiento { get; set; } = null!;

    public int Cantidad { get; set; }

    public DateTime FechaMovimiento { get; set; }

    public virtual Producto Producto { get; set; } = null!;
}
