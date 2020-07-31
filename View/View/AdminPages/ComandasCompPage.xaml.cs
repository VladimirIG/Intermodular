using Controller.Controles;
using System.Windows.Controls;
using View.CRUD.comandacompleta;

namespace View.AdminPages
{
    /// <summary>
    /// Lógica de interacción para ComandasCompPage.xaml
    /// </summary>
    public partial class ComandasCompPage : Page
    {
        //--------------------------Campos de la clase
        private ListView listViewDeLaPage;

        //--------------------------Constructor
        public ComandasCompPage()
        {
            InitializeComponent();
            listViewDeLaPage = list_ComandasCompletas;
        }

        //--------------------------Botonera
        private void Btn_ComandasListar_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            refrescarLista();
            listViewDeLaPage.ItemsSource = ComandaCompletaController.listarComandaCompleta();
        }

        private void Btn_ComandasMostrar_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            refrescarLista();
            mostrarComandaCompleta mostrar = new mostrarComandaCompleta(listViewDeLaPage);
            mostrar.ShowDialog();
        }

        private void Btn_ComandasAnyadir_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            /*refrescarLista();
            anyadirComandaCompleta mostrar = new anyadirComandaCompleta();
            mostrar.ShowDialog();*/
        }

        private void Btn_ComandasActualizar_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            /* refrescarLista();
             actualizarComandaCompleta mostrar = new actualizarComandaCompleta();
             mostrar.ShowDialog();*/
        }

        private void Btn_ComandasEliminar_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            refrescarLista();
            eliminarComandaCompleta mostrar = new eliminarComandaCompleta();
            mostrar.ShowDialog();
        }

        //--------------------------Métodos auxiliares
        private void refrescarLista()
        {
            listViewDeLaPage.ItemsSource = null;
        }
    }
}