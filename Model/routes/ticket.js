const express = require('express');
var router = express.Router();
const ticket = require('../controllers/ticket');

router.get("/ticket", ticket.list);
router.get("/ticket/:codigo", ticket.get);
router.post("/ticket", ticket.add);
router.put("/ticket", ticket.update);
router.delete("/ticket/:codigo", ticket.delete);

module.exports = router;
