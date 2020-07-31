using Controller.Controles;
using System.Windows.Controls;
using View.CRUD.contrato;

namespace View.AdminPages
{
    /// <summary>
    /// Lógica de interacción para ContratoPage.xaml
    /// </summary>
    public partial class ContratoPage : Page
    {
        //--------------------------Campos de la clase
        private ListView listViewDeLaPage;

        //--------------------------Constructor
        public ContratoPage()
        {
            InitializeComponent();
            listViewDeLaPage = list_Contratos;
        }

        //--------------------------Botonera
        private void btn_ContratoListar_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            refrescarLista();
            listViewDeLaPage.ItemsSource = ContratoController.listarContrato();
        }

        private void btn_ContratoMostrar_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            refrescarLista();
            mostrarContrato mostrar = new mostrarContrato(listViewDeLaPage);
            mostrar.ShowDialog();
        }

        private void btn_ContratoAnyadir_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            refrescarLista();
            anyadirContrato mostrar = new anyadirContrato();
            mostrar.ShowDialog();
        }

        private void btn_ContratoActualizar_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            refrescarLista();
            actualizarContrato mostrar = new actualizarContrato();
            mostrar.ShowDialog();
        }

        private void btn_ContratoEliminar_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            refrescarLista();
            eliminarContrato mostrar = new eliminarContrato();
            mostrar.ShowDialog();
        }

        //--------------------------Métodos auxiliares
        private void refrescarLista()
        {
            listViewDeLaPage.ItemsSource = null;
        }
    }
}