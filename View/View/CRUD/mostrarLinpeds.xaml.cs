using Controller.Logica;
using System.Windows;

namespace View.CRUD
{
    /// <summary>
    /// Lógica de interacción para mostrarLinpeds.xaml
    /// </summary>
    public partial class mostrarLinpeds : Window
    {
        private int id_pedido;

        public mostrarLinpeds(int id_pedido)
        {
            InitializeComponent();
            this.id_pedido = id_pedido;
            lbl_IDpedido.Content = $"#{id_pedido}";

            list_Linpeds.ItemsSource = LinpedConProducto.getLinpedsMostrar(id_pedido);
        }

        private void Bnt_Salir_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}