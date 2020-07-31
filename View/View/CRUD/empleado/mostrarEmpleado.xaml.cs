using Controller.Controles;
using Controller.Logica;
using System.Windows;
using System.Windows.Controls;

namespace View.CRUD.empleado
{
    /// <summary>
    /// Lógica de interacción para mostrarEmpleado.xaml
    /// </summary>
    public partial class mostrarEmpleado : Window
    {
        //--------------------------Campos de la clase
        private ListView listaVista;

        //--------------------------Constructor
        public mostrarEmpleado(ListView ListaVentanaPadre)
        {
            InitializeComponent();
            this.listaVista = ListaVentanaPadre;
        }

        //--------------------------Botonera
        private void Bnt_Aceptar_Click(object sender, RoutedEventArgs e)
        {
            if (InputChecker.comprobarFormatoDni(txt_DNI_empleado.Text))
            {
                this.listaVista.ItemsSource = EmpleadoController.getEmpleado(txt_DNI_empleado.Text);
                this.Close();
            }
            else
            {
                Fallos.falloFormato("DNI/NIE");
            }
        }

        private void Bnt_Salir_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}