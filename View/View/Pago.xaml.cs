using Controller.Controles;
using Controller.Logica;
using Intermodular_MVC_VladimirIriarte.Modelo;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace View
{
    /// <summary>
    /// Lógica de interacción para Pago.xaml
    /// </summary>
    public partial class Pago : Window
    {
        //--------------------------Campos de la clase
        //INFORMACIÓN PARA LOS INSERTS//

        //Pedido
        private int id_pedido;

        private int comensales;
        private int id_mesa;

        //Linped y producto
        private List<LinpedConProducto> linpedConProductoIDFINAL;

        //Ticket
        private int cod_ticket;

        private Decimal precio_total;
        private string comentario;

        //Comanda
        private string fecha;

        private string dni_empleado;

        //-- Auxiliares
        private Decimal sinIva;

        private Decimal iva;
        private Decimal vuelto;

        //--------------------------Constructor
        public Pago(int id_mesa, int num_comensales, Empleado empleado
            , List<LinpedConProducto> linpedConProducto, string comentario)
        {
            InitializeComponent();

            //Pedido
            this.id_pedido = obtenerNumPedido();
            this.comensales = num_comensales;
            this.id_mesa = id_mesa;

            //Linped y producto
            this.linpedConProductoIDFINAL = reAsignarIdPedido(id_pedido, linpedConProducto);

            //Comanda
            this.fecha = DateTime.Now.ToString("yyyy-MM-dd");
            this.dni_empleado = empleado.dni;

            //Auxiliares
            this.sinIva = getPrecioTotalSin();
            this.iva = (sinIva * 10) / 100;

            //Ticket
            this.cod_ticket = obtenerNumTicket();
            this.comentario = comentario;
            this.precio_total = sinIva + iva;

            rdb_efectivo.IsChecked = true; //Pago en efectivo por defecto
            list_Linpeds.ItemsSource = linpedConProductoIDFINAL;

            //Rellenar labels
            lbl_Mesa.Content = this.id_mesa;
            lbl_Empleado.Content = empleado.nombre;
            lbl_Comensales.Content = comensales.ToString();
            lbl_Fecha.Content = fecha;
            lbl_Ticket.Content = this.cod_ticket;

            lbl_SinIva.Content = sinIva.ToString("C");
            lbl_Iva.Content = iva.ToString("C");
            lbl_Con.Content = precio_total.ToString("C");
            lbl_Apagar.Content = precio_total.ToString("C");
            lbl_PorComen.Content = (precio_total / comensales).ToString("C");
        }

        //--------------------------Botonera
        private void rdb_efectivo_Checked(object sender, RoutedEventArgs e)
        {
            txt_NumTarjeta.IsEnabled = false;
            txt_NumTarjeta.Text = "";
            txt_Recibido.IsEnabled = true;
        }

        private void rdb_tarjeta_Checked(object sender, RoutedEventArgs e)
        {
            txt_NumTarjeta.IsEnabled = true;
            txt_Recibido.IsEnabled = false;
            txt_Recibido.Text = "";
        }

        private void btn_Pagar_Click(object sender, RoutedEventArgs e)
        {
            if ((bool)rdb_efectivo.IsChecked)
            {
                if (verificarPagoEfectivo())
                {
                    guardarEnBBDD();
                   
                    if (MessageBoxResult.Yes == MessageBox.Show($"Transacción realizada satisfactoriamente.\n\n\tVuelto: {vuelto.ToString("C")}\n\n¿Desea imprimir el ticket?", "Comanda guardada.", MessageBoxButton.YesNo, MessageBoxImage.Information))
                    {
                        imprimirTicket();
                    }

                    this.Close();
                    linpedConProductoIDFINAL.Clear();
                }
            }
            else
            {
                if (verificaPagoTarjeta())
                {
                    guardarEnBBDD();

                    if (MessageBoxResult.Yes == MessageBox.Show($"Transacción realizada satisfactoriamente.\n\n\tVuelto: {vuelto.ToString("C")}\n\n¿Desea imprimir el ticket?", "Comanda guardada.", MessageBoxButton.YesNo, MessageBoxImage.Information))
                    {
                        imprimirTicket();
                    }

                    this.Close();
                    linpedConProductoIDFINAL.Clear();
                }
            }
        }
        private void btn_Volver_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        //--------------------------Métodos principales
        private List<LinpedConProducto> reAsignarIdPedido(int id_pedido, List<LinpedConProducto> linpedConProductoParaActualizar) //linpedConProducto tenía de id_pedido -1, como ya sabemos que numero pedido es toca reasignarlo
        {
            foreach (LinpedConProducto item in linpedConProductoParaActualizar)
            {
                item.Linped.id_pedido = id_pedido;
            }
            return linpedConProductoParaActualizar;
        }

        private bool verificarPagoEfectivo()
        {
            string dineroRecibido = txt_Recibido.Text.Replace('.', ',');

            try
            {
                Decimal dineroRecib = Decimal.Parse(dineroRecibido);

                if ((vuelto = dineroRecib - precio_total) >= 0)     //Comprobamos que el el dinero recibido es igual o mayor al vuelto y lo asignamos a la varialbe vuelto
                {
                    return true;
                }
                MessageBox.Show("La cantidad ingresada no es suficiente.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);

                return false;
            }
            catch (Exception)
            {
                MessageBox.Show("El formato de dinero percibido introducido no es correcto.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }

        private bool verificaPagoTarjeta()
        {
            string numTarjetaRecibido = txt_NumTarjeta.Text;
            if ((InputChecker.isOnlyNumbers(numTarjetaRecibido)) && (numTarjetaRecibido.Length == 16))
            {
                return true;
            }
            MessageBox.Show("El formato de numero de tarjeta introducido no es correcto.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

            return false;
        }

        private void guardarEnBBDD()
        {
            PedidoController.insertarPedido(this.id_pedido, this.comensales, this.id_mesa);

            foreach (LinpedConProducto item in linpedConProductoIDFINAL)
            {
                LinpedController.insertarLinped(item.Linped.id_pedido, item.Linped.linea, item.Linped.cantidad
                    , item.Linped.codigo_producto, item.Linped.importe);
            }

            TicketController.insertarTicket(this.cod_ticket, this.precio_total, this.comentario);
            ComandaController.insertarComanda(this.id_pedido, this.cod_ticket, this.dni_empleado, this.fecha);
        }

        private void imprimirTicket()
        {
            PrintDialog printer = new PrintDialog();
            if (printer.ShowDialog() == true)
            {
                printer.PrintVisual(grid_Print, "Invoice");
            }
        }

        //--------------------------Métodos auxiliares
        private int obtenerNumTicket()  //Comprobamos el número más alto de los tickets y le sumamos 1, 
        {                               //si se borrasen ticket y se hiciese un Count() nos daría error 
            int mayor = -1;
            foreach (Ticket item in TicketController.listarTicket())
            {
                if (mayor < item.codigo)
                {
                    mayor = item.codigo;
                }
            }
            if (mayor < 0) return 1;

            return mayor + 1;
        }

        private int obtenerNumPedido()
        {
            int mayor = -1;             //Lo mismo que obtenerNumTicket()
            foreach (Pedido item in PedidoController.listarPedido())
            {
                if (mayor < item.id)
                {
                    mayor = item.id;
                }
            }
            if (mayor < 0) return 1;

            return mayor + 1;
        }

        private Decimal getPrecioTotalSin()
        {
            Decimal precio = 0;
            foreach (LinpedConProducto item in linpedConProductoIDFINAL)
            {
                precio += item.Linped.importe;
            }
            return precio;
        }
    }
}