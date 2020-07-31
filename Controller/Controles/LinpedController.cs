using Controller.Logica;
using Intermodular_MVC_VladimirIriarte.Modelo;
using Newtonsoft.Json;
using RestSharp;
using System.Collections.Generic;

namespace Controller.Controles
{
    public class LinpedController
    {
        public static List<Linped> listarLinped()
        {
            var rest = new RestClient(Connection.LINK);
            var request = new RestRequest("/linped", Method.GET);
            var response = rest.Execute(request);

            return JsonConvert.DeserializeObject<List<Linped>>(response.Content);
        }

        public static List<Linped> getLinped(int numero, int linea)
        {
            var rest = new RestClient(Connection.LINK);
            var request = new RestRequest($"/linped/{numero}/{linea}", Method.GET);
            var response = rest.Execute(request);

            return JsonConvert.DeserializeObject<List<Linped>>(response.Content);
        }

        public static List<Linped> getFromPedidoId(int numeroPedido)
        {
            var rest = new RestClient(Connection.LINK);
            var request = new RestRequest($"/linped/{numeroPedido}", Method.GET);
            var response = rest.Execute(request);

            return JsonConvert.DeserializeObject<List<Linped>>(response.Content);
        }

        public static void insertarLinped(int id_pedido, int linea, int cantidad, int codigo_producto, decimal importe)

        {
            Linped linped = new Linped(id_pedido, linea, cantidad, codigo_producto, importe);

            var rest = new RestClient(Connection.LINK);
            var request = new RestRequest("/linped", Method.POST);

            request.AddHeader("Content-Type", "application/json");
            request.AddJsonBody(linped);

            rest.Execute(request);
        }

        public static void actualizarLinped(int numero, int linea, int cantidad, int id_pedido, int codigo_producto)
        {
            Linped linped = new Linped(numero, linea, cantidad, id_pedido, codigo_producto);

            var rest = new RestClient(Connection.LINK);
            var request = new RestRequest("/linped", Method.PUT);

            request.AddHeader("Content-Type", "application/json");
            request.AddJsonBody(linped);

            rest.Execute(request);
        }

        public static void deleteLinped(int numero, int linea)
        {
            var rest = new RestClient(Connection.LINK);
            var request = new RestRequest($"/linped/{numero}/{linea}", Method.DELETE);
            var response = rest.Execute(request);
        }
    }
}