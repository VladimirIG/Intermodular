namespace Intermodular_MVC_VladimirIriarte.Modelo
{
    public class Ticket
    {
        public int codigo { get; set; }
        public decimal precio_total { get; set; }
        public string comentario { get; set; }

        public Ticket()
        {
        }

        public Ticket(int codigo, decimal precio_total, string comentario)
        {
            this.codigo = codigo;
            this.precio_total = precio_total;
            this.comentario = comentario;
        }

        public Ticket(decimal precio_total, string comentario)
        {
            this.precio_total = precio_total;
            this.comentario = comentario;
        }
    }
}