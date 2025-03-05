namespace Dcode.Caetering.Application.Dto
{
    public class Evento
    {
        public int EventoID { get; set; }

        public string Nombre { get; set; }

        public string Descripcion { get; set; }

        public DateTime Fecha { get; set; }

        public int LocalID { get; set; }

        public int UsuarioID { get; set; }

        public string EstadoEvento { get; set; }

        public string UsuarioCreacion { get; set; }

        public DateTime? FechaCreacion { get; set; }

        public string UsuarioModificacion { get; set; }

        public DateTime? FechaModificacion { get; set; }

        public string UsuarioEliminacion { get; set; }

        public DateTime? FechaEliminacion { get; set; }

        public bool? Estado { get; set; }

        public virtual LocalDto Local { get; set; }

    }
}
