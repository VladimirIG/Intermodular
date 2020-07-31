var conexion = require('./conexion');

exports.list = function (req, res) {
    conexion.query("SELECT * FROM ticket", (error, rows) => {
        if (error) {
            console.log(error);

        } else {
            return res.json(rows);
        }
    });

}

exports.get = function (req, res) {
    var codigo = req.params.codigo;
    conexion.query("SELECT * FROM ticket WHERE codigo = ?", [codigo], (error, rows) => {
        if (error) {
            console.log(error);
        } else {
            return res.json(rows);
        }
    });

}

exports.add = function (req, res) {
    var json = JSON.parse(JSON.stringify(req.body));

    conexion.query("INSERT INTO ticket set ?", json, (error, rows) => {
        if (error) {
            console.log(error);
        } else {
            console.log("Ticket insertado", json);
        }
    });
    res.end();
}

exports.update = function (req, res) {
    var json = JSON.parse(JSON.stringify(req.body));

    var datos = {
        codigo: json.codigo,
        precio_total: json.precio_total,
        comentario: json.comentario
    };

    conexion.query("UPDATE ticket set ? WHERE codigo = ?", [datos, json.codigo], (error, rows) => {
        if (error) {
            console.log(error);
        } else {
            console.log("Ticket actualizado: ", datos);
        }
    });
    res.end();
}

exports.delete = function (req, res) {
    var codigo = req.params.codigo;

    conexion.query("DELETE FROM ticket WHERE codigo = ?", [codigo], (error, rows) => {
        if (error) {
            console.log(error);
        } else {
            console.log("Ticket borrado: ", codigo);
        }
    });
    res.end();
}