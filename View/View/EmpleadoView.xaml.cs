using Intermodular_MVC_VladimirIriarte.Modelo;
using System;
using System.Windows;

namespace View
{
    /// <summary>
    /// Lógica de interacción para EmpleadoView.xaml
    /// </summary>
    public partial class EmpleadoView : Window 
    {
        //--------------------------Campos de la clase
        private EmpleadoView sesion = null; //sesion se asignará si el empleadoAdmin pasa a la ventana admin
        private Empleado empleado; 
        public EmpleadoPages.GestorMesas paginaAbierta; //Para poder pasar la variable por un foreach y ver de que las comandas están vacías para así cerrar el programa
        
        //--------------------------Constructor
        public EmpleadoView(Empleado empleado)
        {
            sesion = this;
            InitializeComponent();
            lbl_Empleado.Content = empleado.nombre;
            page_GestorMesa.Content = paginaAbierta = new EmpleadoPages.GestorMesas(empleado); //Frame para las page gestorMesa e historial, la almacenamos para preguntarle si hay comandas abiertas
            page_Historial.Content = new EmpleadoPages.HistorialComandas();

            this.empleado = empleado;

            //Reloj
            System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Start();
        }

        //--------------------------Botonera
        private void btn_PasarAadmin_Click(object sender, RoutedEventArgs e)
        {
            if (empleado.isadmin)
            {
                sesion = this;
                this.Hide();
                AdminView mostrar = new AdminView(empleado, sesion);
                mostrar.Show();
            }
            else
            {
                MessageBox.Show("Su usuario no tiene privilegios de administrador.", "Credenciales incorrectas", MessageBoxButton.OK, MessageBoxImage.Hand);
            }
        }

        //--------------------------Eventos
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e) //Para controlar de que no haya comandas abiertas.
        {
            if (paginaAbierta.hayComandasAbiertas())    //método del GestorMesas
            {
                MessageBoxResult resultado = MessageBox.Show("Hay comandas abiertas, si se cierra el programa se perderán, ¿Está seguro de que quiere salir?", "Salir", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (resultado == MessageBoxResult.Yes)
                {
                    System.Windows.Application.Current.Shutdown();
                }
                else
                {
                    e.Cancel = true;
                }
            }
            else
            {
                System.Windows.Application.Current.Shutdown();
            }
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            lbl_Fecha.Content = DateTime.Now.ToShortDateString();
            lbl_Hora.Content = DateTime.Now.ToShortTimeString();
        }
    }
}