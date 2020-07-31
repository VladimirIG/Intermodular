using Controller.Controles;
using Intermodular_MVC_VladimirIriarte.Modelo;
using System;
using System.Collections.Generic;
using System.Windows;

namespace View.CRUD.mesa
{
    /// <summary>
    /// Lógica de interacción para eliminarMesa.xaml
    /// </summary>
    public partial class eliminarMesa : Window
    {
        //--------------------------Campos de la clase
        private List<Mesa> mesas;

        //--------------------------Constructor
        public eliminarMesa()
        {
            InitializeComponent();
            mesas = MesaController.listarMesa();
            rellenarComboBox();
        }

        //--------------------------Botonera
        private void btn_Aceptar_Click(object sender, RoutedEventArgs e)
        {
            if (MesaController.deleteMesa(Int32.Parse(comb_Id.SelectedItem.ToString())))
            {
                this.Close();
            }
        }

        private void btn_Salir_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        //--------------------------Métodos auxiliares
        private void rellenarComboBox()
        {
            foreach (Mesa mesa in mesas)
            {
                comb_Id.Items.Add(mesa.id);
            }
        }
    }
}