const mongoose = require('mongoose')

const Schema = mongoose.Schema

const tokenSchema = new Schema({
    
}, {strict: false,
    timestamps: true,
})

const Token = mongoose.model('Token', tokenSchema)

module.exports = Token;