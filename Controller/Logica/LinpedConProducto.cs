using Controller.Controles;
using Intermodular_MVC_VladimirIriarte.Modelo;
using System.Collections.Generic;

namespace Controller.Logica
{
    public class LinpedConProducto //Esta clase es principalmente para poder mostrar la información en el ListView de MesaView
    {
        private Linped linped;
        private Producto producto;

        public Linped Linped { get => linped; set => linped = value; }
        public Producto Producto { get => producto; set => producto = value; }

        public LinpedConProducto(Producto producto, int linea, int cantidad)
        {
            this.Linped = new Linped(-1, linea, cantidad, producto.codigo, (producto.precio) * cantidad);
            this.Producto = producto;
        }

        public static List<LinpedConProducto> getLinpedsMostrar(int id_pedido)
        {
            List<Linped> linpeds = LinpedController.getFromPedidoId(id_pedido);
            List<Producto> productos = ProductoController.listarProducto();

            List<LinpedConProducto> linpedConProductos = new List<LinpedConProducto>();
            foreach (Linped itemLin in linpeds)
            {
                foreach (Producto itemProd in productos)
                {
                    if (itemLin.codigo_producto == itemProd.codigo)
                    {
                        linpedConProductos.Add(new LinpedConProducto(itemProd, itemLin.linea, itemLin.cantidad));
                    }
                }
            }

            return linpedConProductos;
        }

        /*
         ¿Por qué -1? Porque hasta que no se saque el ticket no sabemos que pedido es, puede haber una cancelación o algún otro
         problema y tendriamos inconsistencia en la secuencialidad de las id de pedido, por ejemplo:
         si asignamos el pedido en la creación de las ventanas, la mesa 8 puede no ser utilizada durante todo el día, por lo que
         el resto de mesas seguirian sumando Id's.

         Por esta razón, el id es reasignado cuando el empleado ENVÍA la información en la Base de datos.
         */
    }
}