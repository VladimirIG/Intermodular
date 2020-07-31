namespace Intermodular_MVC_VladimirIriarte.Modelo
{
    public class Comanda
    {
        public int id_pedido { get; set; }
        public int codigo_ticket { get; set; }
        public string dni_empleado { get; set; }
        public string fecha { get; set; }

        public Comanda(int id_pedido, int codigo_ticket, string dni_empleado, string fecha)
        {
            this.id_pedido = id_pedido;
            this.codigo_ticket = codigo_ticket;
            this.dni_empleado = dni_empleado;
            this.fecha = fecha;
        }
    }
}