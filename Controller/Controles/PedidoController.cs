using Controller.Logica;
using Intermodular_MVC_VladimirIriarte.Modelo;
using Newtonsoft.Json;
using RestSharp;
using System.Collections.Generic;
using System.Windows;

namespace Controller.Controles
{
    public class PedidoController
    {
        public static List<Pedido> listarPedido()
        {
            var rest = new RestClient(Connection.LINK);
            var request = new RestRequest("/pedido", Method.GET);
            var response = rest.Execute(request);

            return JsonConvert.DeserializeObject<List<Pedido>>(response.Content);
        }

        public static List<Pedido> getPedido(int id)
        {
            var rest = new RestClient(Connection.LINK);
            var request = new RestRequest($"/pedido/{id}", Method.GET);
            var response = rest.Execute(request);

            return JsonConvert.DeserializeObject<List<Pedido>>(response.Content);
        }

        public static void insertarPedido(int id, int comensales, int id_mesa)
        {
            Pedido pedido = new Pedido(id, comensales, id_mesa);

            var rest = new RestClient(Connection.LINK);
            var request = new RestRequest("/pedido", Method.POST);

            request.AddHeader("Content-Type", "application/json");
            request.AddJsonBody(pedido);

            rest.Execute(request);
        }

        public static void insertarPedido(int comensales, int id_mesa)
        {
            Pedido pedido = new Pedido(comensales, id_mesa);

            var rest = new RestClient(Connection.LINK);
            var request = new RestRequest("/pedido", Method.POST);

            request.AddHeader("Content-Type", "application/json");
            request.AddJsonBody(pedido);

            rest.Execute(request);
        }

        public static void actualizarPedido(int id, int comensales, int id_mesa)
        {
            Pedido pedido = new Pedido(id, comensales, id_mesa);

            var rest = new RestClient(Connection.LINK);
            var request = new RestRequest("/pedido", Method.PUT);

            request.AddHeader("Content-Type", "application/json");
            request.AddJsonBody(pedido);

            rest.Execute(request);
        }

        public static bool deletePedido(int id)
        {
            var rest = new RestClient(Connection.LINK);
            var request = new RestRequest($"/pedido/{id}", Method.DELETE);
            var resultado = rest.Execute(request);

            if (resultado.IsSuccessful)
            {
                if (PedidoController.getPedido(id).Count == 0)
                {
                    MessageBox.Show("Pedido eliminado", "Pedido eliminado", MessageBoxButton.OK, MessageBoxImage.Information);
                    return true;
                }
                else
                {
                    MessageBox.Show("El pedido no ha podido ser eliminado ", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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