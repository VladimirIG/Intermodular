const express = require('express');
var router = express.Router();
const empleado = require('../controllers/empleado');

router.get("/empleado", empleado.list);
router.get("/empleado/:dni", empleado.get);
router.post("/empleado", empleado.add);
router.put("/empleado", empleado.update);
router.delete("/empleado/:dni", empleado.delete);

module.exports = router;
