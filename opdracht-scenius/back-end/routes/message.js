const router = require('express').Router();
let Message = require('../models/message.model');

router.route('/message').get((req, res) => {
  Message.find()
    .then(exercises => res.json(exercises))
    .catch(err => res.status(400).json('Error: ' + err));
});

router.route('/add').post((req, res) => {
  const username = req.body.username;
  const message = req.body.message;

  const newMessage = new Message({
    username,
    message
  });

  newMessage.save()
  .then(() => res.json('Message added!'))
  .catch(err => res.status(400).json('Error: ' + err));
});

module.exports = router;