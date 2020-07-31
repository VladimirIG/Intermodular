using Controller.Logica;
using Intermodular_MVC_VladimirIriarte.Modelo;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace View
{
    /// <summary>
    /// Lógica de interacción para MesaView.xaml
    /// </summary>
    public partial class MesaView : Window
    {
        //--------------------------Campos de la clase
        public int id_mesa;

        private Empleado empleado;
        private List<Producto> productosActivos;
        private Button[] padNumerico;
        private Producto productoSeleccionado;
        public List<LinpedConProducto> linpedConProducto;   //Esta variable es la que utilizaremos para saber si la mesa está vacía o no y también para pasar la lista a la ventana de pago.
                                                            //Esta clase tiene como principal objetivo almacenar un objeto tipo Linped y Producto, y además calcular su precio total.
                                                            //Además podemos rescatar el nombre del producto.

        //--------------------------Constructor
        public MesaView(int id_mesa, Empleado empleado, List<Producto> productosActivos)
        {
            InitializeComponent();
            ((INotifyCollectionChanged)list_Linpeds.Items).CollectionChanged += ListView_CollectionChanged; //Evento que se activa al haber alguna alteración en el ListView de los Linpeds

            padNumerico = new Button[] { btn_Calc_0, btn_Calc_1, btn_Calc_2, btn_Calc_3, btn_Calc_4, btn_Calc_5//Creación del teclado numérico
                , btn_Calc_6, btn_Calc_7, btn_Calc_8, btn_Calc_9, btn_Calc_C };
            asignarListenerAlPadNumerico();//Creación del teclado numérico

            this.id_mesa = id_mesa;
            this.empleado = empleado;
            this.productosActivos = productosActivos;

            lbl_IDMesa.Content = $"Mesa: {id_mesa}";
            lbl_NombreEmpleado.Content = $"Empleado: {empleado.nombre}";

            anyadirProductosAlTab();

            linpedConProducto = null;
            refrescarListaLinpeds();
        }

        //--------------------------Botonera
        private void btn_AnyadirLinped_Click(object sender, RoutedEventArgs e)
        {
            if (productoSeleccionado == null)
            {
                MessageBox.Show("Seleccione un producto primero.", "Sin selección", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                crearLineaPedido();
                deseleccionarProducto();
            }
        }

        private void Btn_EliminarLinped_Click(object sender, RoutedEventArgs e)
        {
            int index = list_Linpeds.SelectedIndex;

            if (index != -1)
            {
                linpedConProducto.RemoveAt(index);
                refrescarListaLinpeds();
                recalcularTotal();
                isListViewEmpty();
            }
            else
            {
                MessageBox.Show("No ha seleccionado ningún producto.", "Sin Selección", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }


        private void btn_ProcesarTicket_Click(object sender, RoutedEventArgs e)
        {
            if (hayComanda())
            {
                int comensales = Int32.Parse(updown_Comensales.Text);
                Pago mostrar = new Pago(id_mesa, comensales, empleado, linpedConProducto, txtb_Comentario.Text);
                mostrar.ShowDialog();
                if (linpedConProducto.Count == 0) //En la ventana de pago borramos todos los items si se ha realizado el pago satisfatoriamente, sino vuelve a esta ventana con los productos sin alterar
                {
                    vaciarMesa();//La lista se vacía si se ha pagado, desde aquí habilitamos la mesa para el Gestor de mesas
                }
                refrescarListaLinpeds();
            }
            else
            {
                MessageBox.Show("No hay pedidos que procesar.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btn_LimpiarLinpeds_Click(object sender, RoutedEventArgs e)
        {
            if ((MessageBox.Show("¿Desea eliminar todos los pedidos de la mesa?", "Vaciar pedidos"
                    , MessageBoxButton.YesNo, MessageBoxImage.Warning)) == MessageBoxResult.Yes)
            {
                vaciarMesa();
            }
        }

        private void btn_Volver_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }

        //--------------------------Métodos principales
        //---------------Generador de botones
        private DockPanel crearUnDockBoton(Producto producto)
        {

            Button button = new Button();
            button.Width = 100;
            button.Height = 100;
            button.Margin = new Thickness(15, 15, 15, 5);
            button.Click += new RoutedEventHandler(seleccionarProducto_Click);
            button.Tag = producto.codigo;

            try  //Si no se encuentra el archivo genera una excepción, por lo que ponemos una imagen por defecto.
            {
                string pathAbsImagen = System.IO.Path.GetFullPath("../../" + producto.imagen);
                button.Content = new Image { Source = new BitmapImage(new Uri(pathAbsImagen, UriKind.Absolute)) };
            }
            catch (Exception)
            {
                string pathAbsImagen = System.IO.Path.GetFullPath("../../IMG/icons/genericfood.png");
                button.Content = new Image { Source = new BitmapImage(new Uri(pathAbsImagen, UriKind.Absolute)) };
            }

            Label nombreProducto = new Label();
            nombreProducto.Content = producto.nombre;
            nombreProducto.FontSize = 12;
            nombreProducto.FontWeight = FontWeights.Bold;
            nombreProducto.HorizontalContentAlignment = HorizontalAlignment.Center;

            //Empalmamos elementos
            DockPanel dockPanel = new DockPanel();
            DockPanel.SetDock(nombreProducto, Dock.Bottom);
            dockPanel.Children.Add(nombreProducto);
            dockPanel.Children.Add(button);

            return dockPanel;
        }

        //---------------Añadir producto al listView
        private void crearLineaPedido()
        {
            int cantidad = Int32.Parse(lbl_CantidadUnidad.Content.ToString());

            if (cantidad == 0)
            {
                MessageBox.Show("La cantidad debe ser mayor a 0.", "Cantidad insuficiente", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            else
            {
                if (linpedConProducto == null)  //Al ya no ser null ahora parecerá en el gestor de mesas como mesa ocupada
                {
                    linpedConProducto = new List<LinpedConProducto>();
                }

                int numLinea = linpedConProducto.Count();

                anyadirOfusionar(new LinpedConProducto(productoSeleccionado, numLinea, cantidad));
                refrescarListaLinpeds();
                recalcularTotal();
            }
        }

        private void anyadirOfusionar(LinpedConProducto aComprobar) //Al añadir un linped buscamos si ya existe uno igual para sumarlo y así evitar 2 líneas del mismo producto.
        {
            foreach (LinpedConProducto item in this.linpedConProducto)
            {
                if (item.Producto.nombre.Equals(aComprobar.Producto.nombre))
                {
                    item.Linped.importe += aComprobar.Linped.importe;
                    item.Linped.cantidad += aComprobar.Linped.cantidad;
                    return;
                }
            }

            if (this.linpedConProducto.Count >= 0)//Si la lista es 0 no se agregaría nunca el primer producto
            {
                this.linpedConProducto.Add(aComprobar);
            }
        }

        //--------------------------Métodos auxiliares
        //---------------Selección de producto
        private void seleccionarProducto_Click(Object sender, RoutedEventArgs e)
        {
            Button esteBoton = (Button)sender;
            int cod_producto = (Int32)esteBoton.Tag;

            foreach (Producto item in productosActivos)
            {
                if (item.codigo == cod_producto)
                {
                    productoSeleccionado = item;
                    btn_AnyadirLinped.IsEnabled = true;
                    btn_InfoProducto.IsEnabled = true;
                }
            }

            try
            {
                string pathAbsImagen = System.IO.Path.GetFullPath("../../" + productoSeleccionado.imagen);
                img_ProductoSeleccionado.Source = new BitmapImage(new Uri(pathAbsImagen, UriKind.Absolute));
            }
            catch (Exception)
            {
                string pathAbsImagen = System.IO.Path.GetFullPath("../../img/icons/genericfood.png");
                img_ProductoSeleccionado.Source = new BitmapImage(new Uri(pathAbsImagen, UriKind.Absolute));
            }
        }

        public void anyadirProductosAlTab()
        {
            foreach (Producto item in productosActivos)
            {
                switch (item.categoria)
                {
                    case "REFRESCO": wrap_Refrescos.Children.Add(crearUnDockBoton(item)); break;
                    case "VINO": wrap_Vinos.Children.Add(crearUnDockBoton(item)); break;
                    case "CERVEZA": wrap_Cervezas.Children.Add(crearUnDockBoton(item)); break;
                    case "ESPIRITUOSA": wrap_Espirituosa.Children.Add(crearUnDockBoton(item)); break;
                    case "ENTRANTE": wrap_Entrantes.Children.Add(crearUnDockBoton(item)); break;
                    case "PRINCIPAL": wrap_Principal.Children.Add(crearUnDockBoton(item)); break;
                    case "POSTRE": wrap_Postres.Children.Add(crearUnDockBoton(item)); break;
                }
            }
        }

        private void deseleccionarProducto()
        {
            lbl_CantidadUnidad.Content = "0";
            productoSeleccionado = null;
            img_ProductoSeleccionado.Source = new BitmapImage(new Uri("IMG/icons/unselected.png", UriKind.Relative));
            btn_AnyadirLinped.IsEnabled = false;
            btn_InfoProducto.IsEnabled = false;
        }

        //---------------Teclado numérico
        private void marcarTecla_Click(Object sender, RoutedEventArgs e)
        {
            Button esteButton = (Button)sender;
            int valorTecla;

            try
            {
                valorTecla = Int32.Parse(esteButton.Content.ToString());
                agregarCifra(valorTecla);
            }
            catch (Exception)
            {
                lbl_CantidadUnidad.Content = 0;     //Si salta el Catch es porque se ha presionado la tecla C
            }
        }

        private void agregarCifra(int valorTecla)
        {
            string cantidadUnidad = lbl_CantidadUnidad.Content.ToString();

            if (cantidadUnidad.Length > 2)
            {
                return;
            }

            if (cantidadUnidad.Equals("0"))
            {
                lbl_CantidadUnidad.Content = valorTecla.ToString();
            }
            else
            {
                lbl_CantidadUnidad.Content = cantidadUnidad + valorTecla.ToString();
            }
        }

        private void asignarListenerAlPadNumerico()
        {
            foreach (Button item in padNumerico)
            {
                item.Click += new RoutedEventHandler(marcarTecla_Click);
            }
        }

        public bool hayComanda()
        {
            if (linpedConProducto != null)
            {
                return true;
            }
            return false;
        }

        private void vaciarMesa()
        {
            this.linpedConProducto = null;
            refrescarListaLinpeds();
            chk_Comentario.IsChecked = false;
            txtb_Comentario.Text = "";
            txtb_Comentario.IsEnabled = false;
            updown_Comensales.Value = 1;
            btn_AnyadirLinped.IsEnabled = false;
            lbl_PrecioTotal.Content = 0.ToString("C");
        }

        private void refrescarListaLinpeds()
        {
            list_Linpeds.ItemsSource = null;
            list_Linpeds.ItemsSource = linpedConProducto;
        }

        private void recalcularTotal()
        {
            Decimal precioTotal = 0;
            foreach (LinpedConProducto item in linpedConProducto)
            {
                precioTotal += item.Linped.importe;
            }
            lbl_PrecioTotal.Content = precioTotal.ToString("C");
        }

        //--------------------------Eventos
        private void chk_Comentario_Checked(object sender, RoutedEventArgs e)
        {
            if (chk_Comentario.IsChecked == true)
            {
                txtb_Comentario.IsEnabled = true;
                txtb_Comentario.Focus();
            }
            else
            {
                txtb_Comentario.Text = "";
                txtb_Comentario.IsEnabled = false;
            }
        }

        private void ListView_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (list_Linpeds.HasItems == true)
            {
                btn_EliminarLinped.IsEnabled = true;
                btn_ProcesarTicket.IsEnabled = true;
                btn_LimpiarLinpeds.IsEnabled = true;
            }
            else
            {
                btn_EliminarLinped.IsEnabled = false;
                btn_ProcesarTicket.IsEnabled = false;
                btn_LimpiarLinpeds.IsEnabled = false;
            }
        }   //Si hay productos se activan los botones.

        private void isListViewEmpty()
        {
            if (list_Linpeds.HasItems == false)
            {
                vaciarMesa();
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)//En lugar de cerrar la ventana al darle la X, la escondemos y mantenemos su estado en segundo plano.
        {
            e.Cancel = true;
            this.Hide();
        }

        private void Btn_InfoProducto_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(productoSeleccionado.especificaciones,"Información del producto", MessageBoxButton.OK);
        }
    }
}