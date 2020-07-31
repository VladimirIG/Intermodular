using Controller.Logica;
using Controller.Modelo;
using Newtonsoft.Json;
using RestSharp;
using System.Collections.Generic;
using System.Windows;

namespace Controller.Controles
{
    public class AltaTrabajadorController
    {
        public static List<AltaTrabajador> listarAlta()
        {
            var rest = new RestClient(Connection.LINK);
            var request = new RestRequest("/altatrabajador/", Method.GET);
            var response = rest.Execute(request);

            return JsonConvert.DeserializeObject<List<AltaTrabajador>>(response.Content);
        }

        public static List<AltaTrabajador> getAlta(string dni)
        {
            var rest = new RestClient(Connection.LINK);
            var request = new RestRequest($"/AltaTrabajador/{dni}", Method.GET);
            var response = rest.Execute(request);

            return JsonConvert.DeserializeObject<List<AltaTrabajador>>(response.Content);
        }

        public static bool insertarAlta(string dni, string passw, string nombre, string apellidos
            , string rango, string tlf, string disponibilidad, bool isadmin, bool activo,
            /**/string numero_ss, string comienza, string finaliza, int horas_semana
            , string tipo)
        {
            AltaTrabajador altaTrabajador = new AltaTrabajador(dni, passw, nombre, apellidos
            , rango, tlf, disponibilidad, isadmin, activo,
            /**/numero_ss, comienza, finaliza, horas_semana, tipo);

            var rest = new RestClient(Connection.LINK);
            var request = new RestRequest("/altatrabajador", Method.POST);

            request.AddHeader("Content-Type", "application/json");
            request.AddJsonBody(altaTrabajador);

            var resultado = rest.Execute(request);

            if (resultado.IsSuccessful)
            {
                MessageBox.Show("Alta Insertada", "Alta insertada", MessageBoxButton.OK, MessageBoxImage.Information);
                return true;
            }
            else
            {
                Fallos.falloConexionDB();
                return false;
            }
        }

        public static bool actualizarAltaTrabajador(string dni, string passw, string nombre, string apellidos
            , string rango, string tlf, string disponibilidad, bool isadmin, bool activo,
            /**/int numero, string numero_ss, string comienza, string finaliza, int horas_semana
            , string tipo)
        {
            AltaTrabajador altaTrabajador = new AltaTrabajador(dni, passw, nombre, apellidos
            , rango, tlf, disponibilidad, isadmin, activo,
            /**/numero, numero_ss, comienza, finaliza, horas_semana, tipo);

            var rest = new RestClient(Connection.LINK);
            var request = new RestRequest("/altatrabajador", Method.PUT);

            request.AddHeader("Content-Type", "application/json");
            request.AddJsonBody(altaTrabajador);

            var resultado = rest.Execute(request);

            if (resultado.IsSuccessful)
            {
                MessageBox.Show("Alta actualizada", "Alta actualizada", MessageBoxButton.OK, MessageBoxImage.Information);
                return true;
            }
            else
            {
                Fallos.falloConexionDB();
                return false;
            }
        }

        public static bool deleteAltaTrabajador(string dni)
        {
            var rest = new RestClient(Connection.LINK);
            var request = new RestRequest($"/altatrabajador/{dni}", Method.DELETE);

            var resultado = rest.Execute(request);

            if (resultado.IsSuccessful)
            {
                if (AltaTrabajadorController.getAlta(dni).Count == 0)
                {
                    MessageBox.Show("Alta eliminada", "Alta eliminada", MessageBoxButton.OK, MessageBoxImage.Information);
                    return true;
                }
                else
                {
                    MessageBox.Show("El Alta no ha podido ser eliminada ", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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