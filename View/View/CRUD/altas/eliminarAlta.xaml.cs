using Controller.Controles;
using Controller.Modelo;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace View.CRUD.altas
{
    /// <summary>
    /// Lógica de interacción para eliminarAlta.xaml
    /// </summary>
    public partial class eliminarAlta : Window
    {
        //--------------------------Campos de la clase
        private List<AltaTrabajador> altas;
        private AltaTrabajador altita;
       
        //--------------------------Constructor
        public eliminarAlta()
        {
            InitializeComponent();
            altas = AltaTrabajadorController.listarAlta();
            rellenarComboBox();
        }
       
        //--------------------------Botonera
        private void Bnt_Aceptar_Click(object sender, RoutedEventArgs e)
        {
            if (AltaTrabajadorController.deleteAltaTrabajador(comb_DNI.SelectedValue.ToString()))
            {
                this.Close();
            }
        }

        private void Bnt_Salir_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        //--------------------------Métodos auxiliares
        private void rellenarComboBox()
        {
            foreach (AltaTrabajador alta in altas)
            {
                comb_DNI.Items.Add(alta.dni);
            }
        }
        
        //--------------------------Eventos
        private void Comb_NumContrato_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string dni = comb_DNI.SelectedValue.ToString();
            altita = AltaTrabajadorController.getAlta(dni)[0];
            txt_NumContrato.Text = altita.numero_ss;
        }
    }
}