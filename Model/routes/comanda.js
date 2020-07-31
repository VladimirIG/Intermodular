const express = require('express');
var router = express.Router();
const comanda = require('../controllers/comanda');

router.get("/comanda", comanda.list);
router.get("/comanda/:id_pedido/:codigo_ticket/:codigo_empleado", comanda.get);
router.post("/comanda", comanda.add);
router.put("/comanda", comanda.update);
router.delete("/comanda/:id_pedido/:codigo_ticket/:codigo_empleado", comanda.delete);

module.exports = router;