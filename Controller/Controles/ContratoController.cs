using Controller.Logica;
using Intermodular_MVC_VladimirIriarte.Modelo;
using Newtonsoft.Json;
using RestSharp;
using System.Collections.Generic;
using System.Windows;

namespace Controller.Controles
{
    public class ContratoController
    {
        public static List<Contrato> listarContrato()
        {
            var rest = new RestClient(Connection.LINK);
            var request = new RestRequest("/contrato", Method.GET);
            var response = rest.Execute(request);

            return JsonConvert.DeserializeObject<List<Contrato>>(response.Content);
        }

        public static List<Contrato> getContrato(int numero)
        {
            var rest = new RestClient(Connection.LINK);
            var request = new RestRequest($"/contrato/{numero}", Method.GET);
            var response = rest.Execute(request);

            return JsonConvert.DeserializeObject<List<Contrato>>(response.Content);
        }

        public static bool insertarContrato(int numero, string numero_ss, string comienza, string finaliza, int horas_semana
            , string tipo, string dni_empleado)

        {
            Contrato contrato = new Contrato(numero, numero_ss, comienza, finaliza, horas_semana
                 , tipo, dni_empleado);

            var rest = new RestClient(Connection.LINK);
            var request = new RestRequest("/contrato", Method.POST);

            request.AddHeader("Content-Type", "application/json");
            request.AddJsonBody(contrato);

            var resultado = rest.Execute(request);

            if (resultado.IsSuccessful)
            {
                MessageBox.Show("Contrato Insertado", "Contrato Insertado", MessageBoxButton.OK, MessageBoxImage.Information);
                return true;
            }
            else
            {
                Fallos.falloConexionDB();
                return false;
            }
        }

        public static bool insertarContrato(string numero_ss, string comienza, string finaliza, int horas_semana
         , string tipo, string dni_empleado)

        {
            Contrato contrato = new Contrato(numero_ss, comienza, finaliza, horas_semana
                 , tipo, dni_empleado);

            var rest = new RestClient(Connection.LINK);
            var request = new RestRequest("/contrato", Method.POST);

            request.AddHeader("Content-Type", "application/json");
            request.AddJsonBody(contrato);

            var resultado = rest.Execute(request);

            if (resultado.IsSuccessful)
            {
                MessageBox.Show("Contrato Insertado", "Contrato Insertado", MessageBoxButton.OK, MessageBoxImage.Information);
                return true;
            }
            else
            {
                Fallos.falloConexionDB();
                return false;
            }
        }

        public static bool actualizarContrato(int numero, string numero_ss, string comienza, string finaliza, int horas_semana
            , string tipo, string dni_empleado)
        {
            Contrato contrato = new Contrato(numero, numero_ss, comienza, finaliza, horas_semana
                 , tipo, dni_empleado);

            var rest = new RestClient(Connection.LINK);
            var request = new RestRequest("/contrato", Method.PUT);

            request.AddHeader("Content-Type", "application/json");
            request.AddJsonBody(contrato);

            var resultado = rest.Execute(request);

            if (resultado.IsSuccessful)
            {
                MessageBox.Show("Contrato Actualizado", "Contrato Actualizado", MessageBoxButton.OK, MessageBoxImage.Information);
                return true;
            }
            else
            {
                Fallos.falloConexionDB();
                return false;
            }
        }

        public static bool deleteContrato(int numero)
        {
            var rest = new RestClient(Connection.LINK);
            var request = new RestRequest($"/contrato/{numero}", Method.DELETE);

            var resultado = rest.Execute(request);

            if (resultado.IsSuccessful)
            {
                if (ContratoController.getContrato(numero).Count == 0)
                {
                    MessageBox.Show("Contrato eliminado", "Contrato eliminado", MessageBoxButton.OK, MessageBoxImage.Information);
                    return true;
                }
                else
                {
                    MessageBox.Show("El Contrato no ha podido ser eliminado ", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
            }
            else
            {
                Fallos.falloConexionDB();
                return false;
            }
        }
    }
}