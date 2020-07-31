using Intermodular_MVC_VladimirIriarte.Modelo;
using System;
using System.Windows;

namespace View
{
    /// <summary>
    /// Lógica de interacción para AdminView.xaml
    /// </summary>
    public partial class AdminView : Window
    {
        //--------------------------Campos de la clase
        private Empleado admin;
        private EmpleadoView sesion;
        bool flag_btn_IrEmpleado = false;

        //--------------------------Constructor
        public AdminView(Empleado admin, EmpleadoView sesion)
        {
            InitializeComponent();
            if (sesion != null) this.sesion = sesion;   //Si venimos de la ventana de EmpleadoView, la sesión no será null como la del login, así que guardamos los pedidos abiertos de la ventana EmpleadoView
            this.admin = admin;
            lbl_UsuarioAdmin.Content = admin.nombre;

            altasTab.Content = new AdminPages.AltasPage(); //Cargamos el abanico de pages que se mostrarán en las tabs.
            comandasCompTab.Content = new AdminPages.ComandasCompPage();
            contratoTab.Content = new AdminPages.ContratoPage();
            empleadoTab.Content = new AdminPages.EmpleadoPage();
            mesaTab.Content = new AdminPages.MesaPage();
            productoTab.Content = new AdminPages.ProductoPage();

            //Reloj
            System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Start();
        }

        //--------------------------Botonera
        private void btn_IrEmpleadoView_Click(object sender, RoutedEventArgs e)
        {
            flag_btn_IrEmpleado = true;
            if (sesion != null)
            {
                this.sesion.Show(); //Mostramos la ventana EmpleadoView guardada en la sesion
                this.Close();
            }
            else
            {
                EmpleadoView mostrar = new EmpleadoView(admin);
                mostrar.Show();
                this.Close();
            }
        }

        //--------------------------Eventos
        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            lbl_Fecha.Content = DateTime.Now.ToShortDateString();
            lbl_Hora.Content = DateTime.Now.ToShortTimeString();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e) //Para controlar de que no haya comandas abiertas.
        {
            if (sesion != null)    //método del GestorMesas
            {
                if (!flag_btn_IrEmpleado)
                {
                    MessageBoxResult resultado = MessageBox.Show("Existe una sesión de empleados abierta, si posee comandas estas se perderían, ¿Está seguro de que quiere salir?", "Salir", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                    if (resultado == MessageBoxResult.Yes)
                    {
                        System.Environment.Exit(0);
                    }
                    else
                    {
                        e.Cancel = true;
                    }
                }
            }
        }
    }
}