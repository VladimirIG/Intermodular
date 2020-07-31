using Controller.Controles;
using Controller.Logica;
using Intermodular_MVC_VladimirIriarte.Modelo;
using System;
using System.Collections.Generic;
using System.Windows;

namespace View.CRUD.contrato
{
    /// <summary>
    /// Lógica de interacción para anyadirContrato.xaml
    /// </summary>
    public partial class anyadirContrato : Window
    {
        //--------------------------Campos de la clase
        private List<Contrato> contratos;
        private List<Empleado> empleados;

        private string numero_ss;
        private string comienza;
        private string finaliza;
        private int horas_semana;
        private string tipo;
        private string dni_empleado;

        //--------------------------Constructor
        public anyadirContrato()
        {
            InitializeComponent();

            contratos = ContratoController.listarContrato();
            empleados = EmpleadoController.listarEmpleado();
            rellenarComboBox();
        }

        //--------------------------Botonera
        private void bnt_Aceptar_Click(object sender, RoutedEventArgs e)
        {
            if (comprobarAsignarCampos())
            {
                if (ContratoController.insertarContrato(numero_ss, comienza, finaliza, horas_semana
                     , tipo, dni_empleado))
                {
                    this.Close();
                }
            }
        }

        private void bnt_Salir_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        //--------------------------Métodos principales
        private bool comprobarAsignarCampos()
        {
            string errores = "";
            bool resultado = true;

            if (!InputChecker.isOnlyNumbers(txt_NumSS.Text) || (txt_NumSS.Text.Length < 6))
            {
                errores += "\n -Número SS: Solo se admiten numeros .Debe tener almenos 6 caracteres";
                resultado = false;
            }
            else
            {
                this.numero_ss = txt_NumSS.Text;
            }
            try
            {
                DateTime com = (DateTime)date_Comienza.SelectedDate;
                DateTime fin = (DateTime)date_Finaliza.SelectedDate;
                this.comienza = com.ToString("yyyy-MM-dd");
                this.finaliza = fin.ToString("yyyy-MM-dd");
            }
            catch (Exception)
            {
                errores += "\n-Fechas: El Formato es diferente de 'yyyy-MM- dd'. Ejemplo correcto: '2019-05-13' ";
                resultado = false;
            }
            try
            {
                this.horas_semana = (Int32)updown_HorasSemana.Value;
            }
            catch (Exception)
            {
                errores += "\n-Horas a la semana: Solo se admiten numeros.\n";
                resultado = false;
            }

            this.tipo = comb_TipoContrato.Text;         //estos campos van mediante combobox, no hace falta verificarlos
            this.dni_empleado = comb_DNIempleado.Text;

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
            foreach (Empleado emp in empleados)
            {
                comb_DNIempleado.Items.Add(emp.dni);
            }
        }
    }
}