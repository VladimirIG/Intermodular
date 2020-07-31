using Controller.Logica;
using Intermodular_MVC_VladimirIriarte.Modelo;
using Newtonsoft.Json;
using RestSharp;
using System.Collections.Generic;
using System.Windows;

namespace Controller.Controles
{
    public class MesaController
    {
        public static List<Mesa> listarMesa()
        {
            var rest = new RestClient(Connection.LINK);
            var request = new RestRequest("/mesa", Method.GET);
            var response = rest.Execute(request);

            return JsonConvert.DeserializeObject<List<Mesa>>(response.Content);
        }

        public static List<Mesa> getMesa(int id)
        {
            var rest = new RestClient(Connection.LINK);
            var request = new RestRequest($"/mesa/{id}", Method.GET);
            var response = rest.Execute(request);

            return JsonConvert.DeserializeObject<List<Mesa>>(response.Content);
        }

        public static bool insertarMesa(int id, string zona, int n_sillas, bool activa)
        {
            Mesa mesa = new Mesa(id, zona, n_sillas, activa);

            var rest = new RestClient(Connection.LINK);
            var request = new RestRequest("/mesa", Method.POST);

            request.AddHeader("Content-Type", "application/json");
            request.AddJsonBody(mesa);

            var resultado = rest.Execute(request);

            if (resultado.IsSuccessful)
            {
                MessageBox.Show("Mesa Insertada", "Mesa Insertada", MessageBoxButton.OK, MessageBoxImage.Information);
                return true;
            }
            else
            {
                Fallos.falloConexionDB();
                return false;
            }
        }

        public static bool insertarMesa(string zona, int n_sillas, bool activa) //PK autoincrementable
        {
            Mesa mesa = new Mesa(zona, n_sillas, activa);

            var rest = new RestClient(Connection.LINK);
            var request = new RestRequest("/mesa", Method.POST);

            request.AddHeader("Content-Type", "application/json");
            request.AddJsonBody(mesa);

            var resultado = rest.Execute(request);

            if (resultado.IsSuccessful)
            {
                MessageBox.Show("Mesa Insertada", "Mesa Insertada", MessageBoxButton.OK, MessageBoxImage.Information);
                return true;
            }
            else
            {
                Fallos.falloConexionDB();
                return false;
            }
        }

        public static bool actualizarMesa(int id, string zona, int n_sillas, bool activa)
        {
            Mesa mesa = new Mesa(id, zona, n_sillas, activa);

            var rest = new RestClient(Connection.LINK);
            var request = new RestRequest("/mesa", Method.PUT);

            request.AddHeader("Content-Type", "application/json");
            request.AddJsonBody(mesa);

            var resultado = rest.Execute(request);

            if (resultado.IsSuccessful)
            {
                MessageBox.Show("Mesa Actualizada", "Mesa Actualizada", MessageBoxButton.OK, MessageBoxImage.Information);
                return true;
            }
            else
            {
                Fallos.falloConexionDB();
                return false;
            }
        }

        public static bool deleteMesa(int id)
        {
            var rest = new RestClient(Connection.LINK);
            var request = new RestRequest($"/mesa/{id}", Method.DELETE);

            var resultado = rest.Execute(request);

            if (resultado.IsSuccessful)
            {

                if (MesaController.getMesa(id).Count == 0)
                {
                    MessageBox.Show("Mesa eliminada", "Mesa eliminada", MessageBoxButton.OK, MessageBoxImage.Information);
                    return true;
                }
                else
                {
                    MessageBox.Show("La Mesa no ha podido ser eliminada ", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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