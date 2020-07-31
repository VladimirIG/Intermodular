const express = require('express');
var router = express.Router();
const contrato = require('../controllers/contrato');

router.get("/contrato", contrato.list);
router.get("/contrato/:numero", contrato.get);
router.post("/contrato", contrato.add);
router.put("/contrato", contrato.update);
router.delete("/contrato/:numero", contrato.delete);

module.exports = router;