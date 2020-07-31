using Controller.Controles;
using System.Windows.Controls;
using View.CRUD.altas;

namespace View.AdminPages
{
    /// <summary>
    /// Lógica de interacción para AltasPage.xaml
    /// </summary>
    public partial class AltasPage : Page
    {
        //--------------------------Campos de la clase
        private ListView listViewDeLaPage;

        //--------------------------Constructor
        public AltasPage()
        {
            InitializeComponent();
            listViewDeLaPage = list_Altas;
        }

        //--------------------------Botonera
        private void Btn_AltasListar_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            refrescarLista();
            listViewDeLaPage.ItemsSource = AltaTrabajadorController.listarAlta();
        }

        private void Btn_AltasMostrar_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            refrescarLista();
            mostrarAlta mostrar = new mostrarAlta(listViewDeLaPage);
            mostrar.ShowDialog();
        }

        private void Btn_AltasAnyadir_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            refrescarLista();
            anyadirAlta mostrar = new anyadirAlta(listViewDeLaPage);
            mostrar.ShowDialog();
        }

        private void Btn_AltasActualizar_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            refrescarLista();
            actualizarAlta mostrar = new actualizarAlta();
            mostrar.ShowDialog();
        }

        private void Btn_AltasEliminar_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            refrescarLista();
            eliminarAlta mostrar = new eliminarAlta();
            mostrar.ShowDialog();
        }

        //--------------------------Métodos Auxiliares
        private void refrescarLista()
        {
            listViewDeLaPage.ItemsSource = null;
        }
    }
}