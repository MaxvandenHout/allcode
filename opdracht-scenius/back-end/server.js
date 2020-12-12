const express = require('express');
const cors = require('cors');
const mongoose = require('mongoose');

require('dotenv').config();

const app = express();
const port = process.env.PORT || 3000;

app.use(cors());
app.use(express.json());

const uri = 'mongodb://mongo:27017/opdracht';
// Connect to MongoDB
mongoose
  .connect(
    uri,
    { useNewUrlParser: true }
  )
  .then(() => console.log('MongoDB Connected'))
  .catch(err => console.log(err));

const messageRouter = require('./routes/message.js');

app.use('/api', messagesRouter);

app.listen(port, () => {
    console.log(`Server is running on port: ${port}`);
});
