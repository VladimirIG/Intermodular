using Controller.Controles;
using System.Windows;
using System.Windows.Controls;
using View.CRUD.empleado;

namespace View.AdminPages
{
    /// <summary>
    /// Lógica de interacción para EmpleadoPage.xaml
    /// </summary>
    public partial class EmpleadoPage : Page
    {
        //--------------------------Campos de la clase
        private ListView listViewDeLaPage;

        //--------------------------Constructor
        public EmpleadoPage()
        {
            InitializeComponent();
            listViewDeLaPage = list_Empleados;
        }

        //--------------------------Botonera
        private void btn_EmpleadoListar_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            refrescarLista();
            listViewDeLaPage.ItemsSource = EmpleadoController.listarEmpleado();
        }

        private void Btn_EmpleadoMostrar_Click_1(object sender, RoutedEventArgs e)
        {
            refrescarLista();
            mostrarEmpleado mostrar = new mostrarEmpleado(listViewDeLaPage);
            mostrar.ShowDialog();
        }

        private void Btn_EmpleadoAnyadir_Click_1(object sender, RoutedEventArgs e)
        {
            refrescarLista();
            anyadirEmpleado mostrar = new anyadirEmpleado();
            mostrar.ShowDialog();
        }

        private void Btn_EmpleadoActualizar_Click_1(object sender, RoutedEventArgs e)
        {
            refrescarLista();
            actualizarEmpleado mostrar = new actualizarEmpleado();
            mostrar.ShowDialog();
        }

        private void Btn_EmpleadoEliminar_Click_1(object sender, RoutedEventArgs e)
        {
            refrescarLista();
            eliminarEmpleado mostrar = new eliminarEmpleado();
            mostrar.ShowDialog();
        }

        //--------------------------Métodos auxiliares
        private void refrescarLista()
        {
            listViewDeLaPage.ItemsSource = null;
        }
    }
}