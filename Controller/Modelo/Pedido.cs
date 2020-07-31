namespace Intermodular_MVC_VladimirIriarte.Modelo
{
    public class Pedido
    {
        public int id { get; set; }
        public int comensales { get; set; }
        public int id_mesa { get; set; }

        public Pedido()
        {
        }

        public Pedido(int id, int comensales, int id_mesa)
        {
            this.id = id;
            this.comensales = comensales;
            this.id_mesa = id_mesa;
        }

        public Pedido(int comensales, int id_mesa)
        {
            this.id = id;
            this.comensales = comensales;
            this.id_mesa = id_mesa;
        }
    }
}