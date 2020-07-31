var conexion = require('./conexion');

exports.list = function (req, res) {
    conexion.query("SELECT * FROM linped", (error, rows) => {
        if (error) {
            console.log(error);
        } else {
            return res.json(rows);
        }
    });

}

exports.get = function (req, res) {
    var id_pedido = req.params.id_pedido;
    var linea = req.params.linea;
    conexion.query("SELECT * FROM linped WHERE id_pedido = ? AND linea = ?", [id_pedido, linea], (error, rows) => {
        if (error) {
            console.log(error);
        } else {
            return res.json(rows);
        }
    });

}

exports.add = function (req, res) {
    var json = JSON.parse(JSON.stringify(req.body));

    conexion.query("INSERT INTO linped set ?", json, (error, rows) => {
        if (error) {
            console.log(error);
        } else {
            console.log("Linped insertado: ", `Pedido ${req.body.id_pedido}, Línea ${req.body.linea}`);
        }
    });
    res.end();
}

exports.update = function (req, res) {

    var json = JSON.parse(JSON.stringify(req.body));
    var id_pedido = json.id_pedido;
    var linea = json.linea;
    var datos = {
        id_pedido: json.id_pedido,
        linea: json.linea,
        cantidad: json.cantidad,
        id_pedido: json.id_pedido,
        codigo_producto: json.codigo_producto
    };

    conexion.query("UPDATE linped set ? WHERE id_pedido = ? AND linea = ?", [datos, id_pedido, linea], (error, rows) => {
        if (error) {
            console.log(error);
        } else {
            console.log("Linped actualizado: ", datos);
        }
    });
    res.end();
}

exports.delete = function (req, res) {
    var id_pedido = req.params.id_pedido;
    var linea = req.params.linea;

    conexion.query("DELETE FROM linped WHERE id_pedido = ? AND linea = ?", [id_pedido, linea], (error, rows) => {
        if (error) {
            console.log(error);
        } else {
            console.log("Linped borrado.", `Pedido ${req.body.id_pedido}, Línea ${req.body.linea}`);
        }
    });
    res.end();
}


//--------------------------Esta Select es para el apartado Historial de Empleados

exports.getFromComanda = function (req, res) {

    conexion.query("SELECT * FROM linped WHERE id_pedido = ?", [req.params.id_pedido], (error, rows) => {
        if (error) {
            console.log(error);
        } else {
            return res.json(rows);
        }
    });
}
