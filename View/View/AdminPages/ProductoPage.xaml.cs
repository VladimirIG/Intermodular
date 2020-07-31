using Controller.Controles;
using System.Windows.Controls;
using View.CRUD.producto;

namespace View.AdminPages
{
    /// <summary>
    /// Lógica de interacción para ProductoPage.xaml
    /// </summary>
    public partial class ProductoPage : Page
    {
        //--------------------------Campos de la clase
        private ListView listViewDeLaPage;

        //--------------------------Constructor
        public ProductoPage()
        {
            InitializeComponent();
            listViewDeLaPage = list_Productos;
        }

        //--------------------------Botonera
        private void btn_ProductoListar_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            refrescarLista();
            listViewDeLaPage.ItemsSource = ProductoController.listarProducto();
        }

        private void btn_ProductoMostrar_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            refrescarLista();
            mostrarProducto mostrar = new mostrarProducto(listViewDeLaPage);
            mostrar.ShowDialog();
        }

        private void btn_ProductoAnyadir_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            refrescarLista();
            anyadirProducto mostrar = new anyadirProducto();
            mostrar.ShowDialog();
        }

        private void btn_ProductoActualizar_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            refrescarLista();
            actualizarProducto mostrar = new actualizarProducto();
            mostrar.ShowDialog();
        }

        private void btn_ProductoEliminar_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            refrescarLista();
            eliminarProducto mostrar = new eliminarProducto();
            mostrar.ShowDialog();
        }

        //--------------------------Métodos auxiliares
        private void refrescarLista()
        {
            listViewDeLaPage.ItemsSource = null;
        }
    }
}