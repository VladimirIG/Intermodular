var conexion = require('../controllers/conexion');

exports.get = function (req, res) {

    conexion.query("SELECT passw FROM empleado WHERE dni = ?", [req.params.dni], (error, rows) => {
        if (error) {
            console.log(error);
        } else {
            return res.json(rows);
        }
    });

}