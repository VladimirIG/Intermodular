var conexion = require('./conexion');

exports.list = function (req, res) {
    conexion.query("SELECT * FROM pedido", (error, rows) => {
        if (error) {
            console.log(error);

        } else {
            return res.json(rows);
        }
    });

}

exports.get = function (req, res) {
    var id = req.params.id;
    conexion.query("SELECT * FROM pedido WHERE id = ?", [id], (error, rows) => {
        if (error) {
            console.log(error);
        } else {
            return res.json(rows);
        }
    });

}

exports.add = function (req, res) {
    var json = JSON.parse(JSON.stringify(req.body));

    conexion.query("INSERT INTO pedido set ?", json, (error, rows) => {
        if (error) {
            console.log(error);
        } else {
            console.log("Pedido insertado: ", json);
        }
    });
    res.end();
}

exports.update = function (req, res) {
    var json = JSON.parse(JSON.stringify(req.body));
    var datos = {
        id: json.id,
        comensales: json.comensales,
        id_mesa: json.id_mesa
    };

    conexion.query("UPDATE pedido set ? WHERE id = ?", [datos, json.id], (error, rows) => {
        if (error) {
            console.log(error);
        } else {
            console.log("Pedido actualizado: ", datos);
        }
    });
    res.end();
}

exports.delete = function (req, res) {
    var id = req.params.id;

    conexion.query("DELETE FROM pedido WHERE id = ?", [id], (error, rows) => {
        if (error) {
            console.log(error);
        } else {
            console.log("Pedido borrado:", id);
        }
    });
    res.end();
}