namespace AppCCL.DTOs
{
    public class MovimientoInventarioDto
    {
        public int ProductoId { get; set; }

        public required string TipoMovimiento { get; set; }

        public int Cantidad { get; set; }
    }
}
