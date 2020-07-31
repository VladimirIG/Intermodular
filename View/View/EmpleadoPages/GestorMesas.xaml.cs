using Controller.Controles;
using Intermodular_MVC_VladimirIriarte.Modelo;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace View.EmpleadoPages
{
    /// <summary>
    /// Lógica de interacción para GestorMesas.xaml
    /// </summary>
    public partial class GestorMesas : Page
    {
        //--------------------------Campos de la clase
        private Empleado empleado;
        public Empleado Empleado { get => empleado; set => empleado = value; }

        private List<Producto> productosActivos;
        private List<Mesa> mesasActivas;

        public List<MesaView> mesasViews; //IMPORTANTE, por cada mesa creada se crea una ventana de MesaView asociada a la mesa que almacenará los datos de cada pedido
        //en mi programa, los insert y modificaciones en la base de datos solo se hacen cuando el cliente ha pagado, la información se guarda en el programa hasta entonces.

        //--------------------------Constructor
        public GestorMesas(Empleado empleado)
        {
            InitializeComponent();
            this.Empleado = empleado;
            this.productosActivos = getProductosActivos(); 
            this.mesasActivas = getMesasActivas();          
            this.mesasViews = getMesasViews();  //Por cada mesa añadimos una ventana MesaView con un ID de mesa a la que pertenece, devolvemos la lista resultante

            crearBotonesParaMesas();
        }

        //--------------------------Métodos principales
        private Button crearUnDockBoton(int numeroMesa, int numeroSillas)   //Plantilla con la que creamos todos los botones de la mesa
        {
            //Creamos elementos y configuramos elementos
            Button button = new Button();
            button.Width = 90;
            button.Height = 90;
            button.Margin = new Thickness(20, 5, 0, 5);
            button.Click += new RoutedEventHandler(abrirMesaView_Click);
            button.Tag = numeroMesa;    //AQUÍ RECIBE EL NUMERO DE MESA ASOCIADO AL BOTÓN
            DockPanel stackPanel = new DockPanel();
            Label num_mesa = new Label();
            num_mesa.Content = $"{numeroMesa}";
            num_mesa.FontSize = 35;
            num_mesa.HorizontalContentAlignment = HorizontalAlignment.Center;

            Label num_sillas = new Label();
            num_sillas = new Label();
            num_sillas.Content = $"Sillas: {numeroSillas}";
            num_sillas.FontSize = 12;

            //Empalmamos elementos
            DockPanel dockPanel = new DockPanel();
            DockPanel.SetDock(num_sillas, Dock.Bottom);
            dockPanel.Children.Add(num_sillas);
            dockPanel.Children.Add(num_mesa);

            button.Background = new SolidColorBrush(Color.FromArgb(160, 173, 255, 47));
            button.Content = dockPanel;

            return button;
        }

        private void crearBotonesParaMesas() //Por cada botón generado buscamos su campo 'Zona' y lo metemos en su correspondiente WrapPanel  
        {
            foreach (var item in mesasActivas)
            {
                Button button = crearUnDockBoton(item.id, item.n_sillas);

                switch (item.zona)
                {
                    case "TERRAZA": wrap_TERRAZA.Children.Add(button); break;
                    case "P0": wrap_P0.Children.Add(button); break;
                    case "P1": wrap_P1.Children.Add(button); break;
                    case "P2": wrap_P2.Children.Add(button); break;
                }
                button = null;
            }
        }

        private void abrirMesaView_Click(object sender, RoutedEventArgs e) 
        {
            Button esteBoton = (Button)sender;
            MesaView mesaView = buscarMesaView(esteBoton.Tag);
            mesaView.ShowDialog();
            comprobarOcupacionMesa(esteBoton, mesaView);
        }

        //--------------------------Métodos auxiliares
        private List<MesaView> getMesasViews()
        {
            List<MesaView> mesasViews = new List<MesaView>();

            foreach (Mesa mesaActiva in mesasActivas)
            {
                mesasViews.Add(new MesaView(mesaActiva.id, Empleado, productosActivos));
            }

            return mesasViews;
        }

        private List<Producto> getProductosActivos()
        {
            List<Producto> productos = ProductoController.listarProducto();
            List<Producto> productosActivos = new List<Producto>();

            foreach (Producto item in productos)
            {
                if (item.activo == true)
                    productosActivos.Add(item);
            }
            return productosActivos;
        }

        private List<Mesa> getMesasActivas()
        {
            List<Mesa> mesas = MesaController.listarMesa();
            List<Mesa> mesasActivas = new List<Mesa>();

            foreach (Mesa item in mesas)
            {
                if (item.activa == true)
                    mesasActivas.Add(item);
            }
            return mesasActivas;
        }

        private MesaView buscarMesaView(object tag)
        {
            foreach (MesaView item in mesasViews)
            {
                if (item.id_mesa == (Int32)tag)
                {
                    return item;
                }
            }
            return null;
        }

        private void comprobarOcupacionMesa(Button botonLanzador, MesaView mesaView)
        {
            if (mesaView.linpedConProducto == null)
            {
                botonLanzador.Background = new SolidColorBrush(Color.FromArgb(160, 173, 255, 47));
            }
            else
            {
                botonLanzador.Background = new SolidColorBrush(Color.FromArgb(200, 255, 172, 173));
            }
        }

        public bool hayComandasAbiertas()
        {
            foreach (MesaView mesaV in mesasViews)
            {
                if (mesaV.hayComanda())
                {
                    return true;
                }
            }
            return false;
        }
    }
}