using Controller.Controles;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using View.CRUD;

namespace View.EmpleadoPages
{
    /// <summary>
    /// Lógica de interacción para HistorialComandas.xaml
    /// </summary>
    public partial class HistorialComandas : Page
    {
        //--------------------------Campos de la clase
        private ComandaCompleta com;

        //--------------------------Constructor
        public HistorialComandas()
        {
            InitializeComponent();
        }

        //--------------------------Botonera
        private void Btn_Refrescar_Click(object sender, RoutedEventArgs e)
        {
            list_ComandasEmpleados.ItemsSource = ComandaCompletaController.listarComandaCompleta();
        }

        private void btn_Mostrar_Click(object sender, RoutedEventArgs e)
        {
            if (com != null)
            {
                mostrarLinpeds mostrar = new mostrarLinpeds(com.id_pedido);
                mostrar.Show();
            }
            else
            {
                MessageBox.Show("Seleccione antes una comanda para mostrar.", "Sin selección", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        
        //--------------------------Eventos
        private void list_ComandasEmpleados_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.com = (ComandaCompleta)(sender as ListView).SelectedItem;
        }
    }
}