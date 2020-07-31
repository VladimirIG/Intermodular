var conexion = require('./conexion');

exports.list = function (req, res) {
    conexion.query("SELECT * FROM producto", (error, rows) => {
        if (error) {
            console.log(error);

        } else {
            return res.json(rows);
        }
    });

}

exports.get = function (req, res) {
    var codigo = req.params.codigo;
    conexion.query("SELECT * FROM producto WHERE codigo = ?", [codigo], (error, rows) => {
        if (error) {
            console.log(error);
        } else {
            return res.json(rows);
        }
    });

}

exports.add = function (req, res) {
    var json = JSON.parse(JSON.stringify(req.body));

    conexion.query("INSERT INTO producto set ?", json, (error, rows) => {
        if (error) {
            console.log(error);
        } else {
            console.log("Producto insertado: ", json);
        }
    });
    res.end();
}

exports.update = function (req, res) {
    var json = JSON.parse(JSON.stringify(req.body));
    var datos = {
        codigo: json.codigo,
        nombre: json.nombre,
        categoria: json.categoria,
        especificaciones: json.especificaciones,
        precio: json.precio,
        activo: json.activo,
        imagen: json.imagen
    };

    conexion.query("UPDATE producto set ? WHERE codigo = ?", [datos, json.codigo], (error, rows) => {
        if (error) {
            console.log(error);
        } else {
            console.log("Producto actualizado: ", datos);
        }
    });
    res.end();
}

exports.delete = function (req, res) {
    var codigo = req.params.codigo;

    conexion.query("DELETE FROM producto WHERE codigo = ?", [codigo], (error, rows) => {
        if (error) {
            console.log(error);
        } else {
            console.log("Producto borrado:", codigo);
        }
    });
    res.end();
}