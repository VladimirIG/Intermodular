﻿using Controller.Controles;
using Controller.Logica;
using Intermodular_MVC_VladimirIriarte.Modelo;
using System;
using System.Collections.Generic;
using System.Windows;

namespace View.CRUD.comandacompleta
{
    /// <summary>
    /// Lógica de interacción para eliminarComandaCompleta.xaml
    /// </summary>
    public partial class eliminarComandaCompleta : Window
    {
        //--------------------------Campos de la clase
        private List<Empleado> empleados;

        private int id_pedido;
        private int cod_ticket;
        private string dni_empleado;

        //--------------------------Constructor
        public eliminarComandaCompleta()
        {
            InitializeComponent();
            empleados = EmpleadoController.listarEmpleado();
            rellenarComboBox();
        }

        //--------------------------Botonera
        private void Bnt_Aceptar_Click(object sender, RoutedEventArgs e)
        {
            if (comprobarAsignarCampos())
            {
                if (ComandaCompletaController.deleteComanda(id_pedido, cod_ticket, dni_empleado))
                {
                    this.Close();
                }
            }
        }

        private void Bnt_Salir_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        //--------------------------Métodos principales
        private bool comprobarAsignarCampos()
        {
            string errores = "";
            bool resultado = true;

            try
            {
                this.id_pedido = Int32.Parse(txt_IDpedido.Text);
            }
            catch (Exception)
            {
                errores += "-ID Pedido: Solo se admiten numeros.\n";
                resultado = false;
            }
            try
            {
                this.cod_ticket = Int32.Parse(txt_CodTicket.Text);
            }
            catch (Exception)
            {
                errores += "-Codigo Ticket: Solo se admiten numeros.";
                resultado = false;
            }
            if (!resultado)
            {
                Fallos.multiFalloFormato(errores);
                return false;
            }

            this.dni_empleado = comb_DNIempleado.Text;
            return true;
        }

        //--------------------------Métodos auxiliares
        private void rellenarComboBox()
        {
            foreach (Empleado empleado in empleados)
            {
                comb_DNIempleado.Items.Add(empleado.dni);
            }
        }
    }
}