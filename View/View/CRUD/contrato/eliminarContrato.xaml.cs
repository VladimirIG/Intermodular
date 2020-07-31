using Controller.Controles;
using Intermodular_MVC_VladimirIriarte.Modelo;
using System;
using System.Collections.Generic;
using System.Windows;

namespace View.CRUD.contrato
{
    /// <summary>
    /// Lógica de interacción para eliminarContrato.xaml
    /// </summary>
    public partial class eliminarContrato : Window
    {
        //--------------------------Campos de la clase
        private List<Contrato> contratos;

        //--------------------------Constructor
        public eliminarContrato()
        {
            InitializeComponent();

            contratos = ContratoController.listarContrato();
            rellenarComboBox();
        }

        //--------------------------Botonera
        private void bnt_Aceptar_Click(object sender, RoutedEventArgs e)
        {
            if (ContratoController.deleteContrato(Int32.Parse(comb_Numero.Text)))
            {
                this.Close();
            }
        }

        private void bnt_Salir_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        //--------------------------Métodos auxiliares
        private void rellenarComboBox()
        {
            foreach (Contrato contrato in contratos)
            {
                comb_Numero.Items.Add(contrato.numero);
            }
        }
    }
}