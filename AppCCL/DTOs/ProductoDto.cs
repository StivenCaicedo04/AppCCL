namespace AppCCL.DTOs
{
    public class ProductoDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int Stock { get; set; }
        public List<MovimientoInventarioDto>? Movimientos { get; set; }
    }
}
