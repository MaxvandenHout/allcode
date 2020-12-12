const express = require('express')
const cors = require('cors')
const mongoose = require('mongoose') 

require('dotenv').config

const app = express()
const port = process.env.PORT || 4000

app.use(cors())
app.use(express.json())

const url = "mongodb://127.0.0.1:27017/users";

mongoose.connect(url, {useNewUrlParser: true, useCreateIndex: true, useUnifiedTopology: true })
const connection = mongoose.connection
connection.once('open', () => {console.log("MongoDB database connection established succesfully")})

const usersRouter = require('./routes/users')
const dataRouter = require('./routes/data')
app.use('/users', usersRouter)
app.use('/data', dataRouter)
app.use('/token', tokenRouter)


app.listen(port, () => {
    console.log(`Server is running on port: ${port}`)
})