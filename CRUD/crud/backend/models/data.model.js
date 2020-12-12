const mongoose = require('mongoose')

const Schema = mongoose.Schema

const dataSchema = new Schema({
    
}, {strict: false,
    timestamps: true,
})

const Data = mongoose.model('Data', dataSchema)

module.exports = Data;