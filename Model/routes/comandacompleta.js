const express = require('express');
var router = express.Router();
const comandacompleta = require('../controllers/comandacompleta');

router.get("/comandacompleta", comandacompleta.list);
router.get("/comandacompleta/:id_pedido/:codigo_ticket/:dni_empleado", comandacompleta.get);


router.delete("/comandacompleta/:id_pedido/:codigo_ticket/:dni_empleado", comandacompleta.delete);

module.exports = router;