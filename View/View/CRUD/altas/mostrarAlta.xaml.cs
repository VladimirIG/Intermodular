using Controller.Controles;
using Controller.Logica;
using System.Windows;
using System.Windows.Controls;

namespace View.CRUD.altas
{
    /// <summary>
    /// Lógica de interacción para mostrarAlta.xaml
    /// </summary>
    public partial class mostrarAlta : Window
    {
        //--------------------------Campos de la clase
        private ListView listaVista;

        //--------------------------Constructor
        public mostrarAlta(ListView ListaVentanaPadre)
        {
            InitializeComponent();
            listaVista = ListaVentanaPadre;
        }

        //--------------------------Botonera
        private void Bnt_Aceptar_Click(object sender, RoutedEventArgs e)
        {
            if (InputChecker.comprobarFormatoDni(txt_DNI.Text))
            {
                this.listaVista.ItemsSource = AltaTrabajadorController.getAlta(txt_DNI.Text);
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