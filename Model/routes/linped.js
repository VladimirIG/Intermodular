const express = require('express');
var router = express.Router();
const linped = require('../controllers/linped');

router.get("/linped", linped.list);
router.get("/linped/:id_pedido/:linea", linped.get);
router.get("/linped/:id_pedido", linped.getFromComanda);
router.post("/linped", linped.add);
router.put("/linped", linped.update);
router.delete("/linped/:numero/:linea", linped.delete);

module.exports = router;