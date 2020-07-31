using Controller.Controles;
using System.Windows;
using System.Windows.Controls;
using View.CRUD.mesa;

namespace View.AdminPages
{
    /// <summary>
    /// Lógica de interacción para MesaPage.xaml
    /// </summary>
    public partial class MesaPage : Page
    {
        //--------------------------Campos de la clase
        private ListView listViewDeLaPage;

        //--------------------------Constructor
        public MesaPage()
        {
            InitializeComponent();
            listViewDeLaPage = list_Mesas;
        }

        //--------------------------Botonera
        private void btn_MesaListar_Click(object sender, RoutedEventArgs e)
        {
            refrescarLista();
            listViewDeLaPage.ItemsSource = MesaController.listarMesa();
        }

        private void btn_MesaMostrar_Click(object sender, RoutedEventArgs e)
        {
            refrescarLista();
            mostrarMesa mostrar = new mostrarMesa(listViewDeLaPage);
            mostrar.ShowDialog();
        }

        private void btn_MesaAnyadir_Click(object sender, RoutedEventArgs e)
        {
            refrescarLista();
            anyadirMesa mostrar = new anyadirMesa();
            mostrar.ShowDialog();
        }

        private void Btn_MesaActualizar_Click(object sender, RoutedEventArgs e)
        {
            refrescarLista();
            actualizarMesa mostrar = new actualizarMesa();
            mostrar.ShowDialog();
        }

        private void btn_MesaEliminar_Click(object sender, RoutedEventArgs e)
        {
            refrescarLista();
            eliminarMesa mostrar = new eliminarMesa();
            mostrar.ShowDialog();
        }

        //--------------------------Métodos auxiliares
        private void refrescarLista()
        {
            listViewDeLaPage.ItemsSource = null;
        }
    }
}