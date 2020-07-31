const express = require('express');
var router = express.Router();
const pedido = require('../controllers/pedido');

router.get("/pedido", pedido.list);
router.get("/pedido/:id", pedido.get);
router.post("/pedido", pedido.add);
router.put("/pedido", pedido.update);
router.delete("/pedido/:id", pedido.delete);

module.exports = router;
