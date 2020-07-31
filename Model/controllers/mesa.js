const conexion = require('./conexion');


exports.list = function (req, res) {
    conexion.query("SELECT * FROM mesa", (error, rows) => {
        if (error) {
            console.log(error);

        } else {
            return res.json(rows);
        }
    });

}
exports.get = function (req, res) {
    var id = req.params.id
    conexion.query("SELECT * FROM mesa WHERE id = ?", [id], (error, rows) => {
        if (error) {
            console.log(error);
        } else {
            return res.json(rows);
        }
    });

}
exports.add = function (req, res) {

    var json = JSON.parse(JSON.stringify(req.body));
    conexion.query("INSERT INTO mesa set ?", json, (error, rows) => {
        if (error) {
            console.log(error);
        } else {
            console.log("Mesa insertada: ", json);
        }
    });
    res.end();
}

exports.update = function (req, res) {
    var json = JSON.parse(JSON.stringify(req.body));
    var datos = {
        id: json.id,
        zona: json.zona,
        n_sillas: json.n_sillas,
        activa: json.activa
    };

    conexion.query("UPDATE mesa set ? WHERE id = ?", [datos, datos.id], (error, rows) => {
        if (error) {
            console.log(error);
        } else {
            console.log("Mesa actualizada: ", datos);
        }
    });
    res.end();
}
exports.delete = function (req, res) {
    var id = req.params.id;

    conexion.query("DELETE FROM Mesa WHERE id = ?", [id], (error, rows) => {
        if (error) {
            console.log(error);
        } else {
            console.log("Mesa borrada: ", id);
        }
    });
    res.end();
}





