const mongoose = require('mongoose')
const router = require('express').Router()
let Data = require('../models/data.model')
let User = require('../models/user.model')
let Token = require('../models/token.model')

function Verification(quest, req, res){
    User.findOne({'username': req.body.verification.user.username})
        .then(user => 
            {
                if (user) 
                {   
                    if (user.password === req.body.verification.user.password)
                    {  
                        if(user.tables.filter(e => e.name == req.body.verification.tablename).length > 0) quest(user)
                        else res.status(400).json("this user does not own this table")
                    }
                    else res.status(400).json("password is incorect")
                }
                else res.status(400).json("user is not found")
            }
        )
        .catch(err => res.status(400).json("Error: " + err))
}

function Authorization(quest, questtype, req, res){
    let now = new Date()
    let token 
    Token.findOne({"string": req.body.token})
    .then(result => {
        token = result
        console.log(token)
    if (token) {
            let tables = []
            tables = token.get("tables")
            console.log(tables)
        if (token.endtime < now ) {
            res.status(400).json("token is expired. Try to reload the page") 
            Token.findByIdAndDelete(token._id)}
        else {
            if(tables.some(table => table.tableid === req.body.tableid)){
                let table = tables.find(table => table.tableid === req.body.tableid)
                if (table.rights.includes(questtype)) quest()
                else res.status(400).json("this token does not have the rights for this action in this table")
            }
            else res.status(400).json("this token has no rights to this table")
            }
        }
     else res.status(400).json("Token incorrect")
    }) 
}

function filter(req, res) {
    if (req.body.filter){
        let filter = req.body.filter
        filter.tableid =  req.body.tableid
        return filter
        }
    else {
        const nofilter = {tableid: req.body.tableid}
    return nofilter
}  
}

function projection(req, res) {
    let projection = ""
    projection = req.body.projection
    if (projection){
        if (projection.includes("-")) {projection += " -tableid "}
        else {
            if (projection.includes("tableid")) {
                res.status(400).json("Projecting the user or tablename properties is not allowed")}
        }
        return projection
       }
    else {
         return " -user -tablename"
    }
    
}
function options(req, res){
    if (req.body.options){
        let options = req.body.options
        return options
    }
    else {
         return null
    }
    
}
function callback(err, data, req, res){
    if (err) {res.status(400).json("Error: " + err)}
    else {
        const callbackstring = req.body.callbackfunction + "(err, data, req, res)"
        eval(callbackstring)
    }
}

function randomString(length) {
        var randomstring     = '';
        var characters       = 'ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789~!@#$%^&*()_+{}:>?<';
        var charactersLength = characters.length;
        for ( var i = 0; i < length; i++ ) {
            randomstring += characters.charAt(Math.floor(Math.random() * charactersLength));
        }
        return randomstring
}
// token
router.route('/token').get((req, res) => {
    Authorization( function (user) {
            let randomstring = ""
            randomstring = randomString(30)
            let newToken 
            let tokentemplates = []
            tokentemplates = user.get("tokentemplates")
        if (tokentemplates.some(template => template.name = req.body.template)) {
            newToken = tokentemplates.find(template => template.name = req.body.template)
            let endtime = new Date() 
            endtime.setHours( endtime.getHours() + newToken.time )
            newToken.string = randomstring
            newToken.endtime = endtime
            Token.create(newToken)
            res.json(randomstring);   
        }
        else res.status(400).json('Error: tokentemplate does not exists')
             
    }, "token", req, res)
})



function tokenUpgrade(req, res){
            let randomstring = ""
            randomstring = randomString(30)
            let newToken 
            let tokentemplates = []
            tokentemplates = user.get("tokentemplates")
        if (tokentemplates.some(template => template.name = req.body.template)) {
            newToken = tokentemplates.find(template => template.name = req.body.template)
            let endtime = new Date() 
            endtime.setHours( endtime.getHours() + newToken.time )
            newToken.string = randomstring
            newToken.endtime = endtime
            Token.create(newToken)
            res.json(randomstring);   
        }
        else res.status(400).json('Error: tokentemplate does not exists')

}

function hashcheck(req, res, err, data) {
   
    let reqhash = req.hash
    let dbhash = data.get(req.hashproperty)
                if (reqhash === dbhash){
                    return true
                }
                else {
                    return false
                }
}


//add
router.route('/add').post((req, res) => {
    Authorization( function () {
        const data = req.body.data
        data.tableid = req.body.tableid
        const newData = new Data(data)
        newData.save()
        Data.create(newData, options(req), callback(req))
            .then(() => res.json('Data added!'))
            .catch(err => res.status(400).json('Error: ' + err))
    }, "add", req, res)
})

//addMany
router.route('/addmany').post((req, res) => {
    Authorization( function () {
        let newData = []
        newData = req.body.data
        newData.forEach(data => {
            data.tableid = req.body.tableid
            })
        Data.create(newData, options(req), callback(res))
            .then(() => res.json('Data added!'))
            .catch(err => res.status(400).json('Error: ' + err))
    }, "addmany", req, res)
})

//Finders
//find
router.route('/').get((req, res) => 
    Authorization( function () {
            Data.find(filter(req, res), projection(req, res), options(req, res), callback(req, res))
            .then(data => res.json(data))
            .catch(err => res.status(400).json('Error: ' + err))
    }, "find", req, res)
)

//findbyid
router.route('/:id').get((req, res) => {
    Authorization( function () {
        Data.findById(req.params.id, null, options(req, res), callback(req, res))
        .then(data => {
            if (data.toJSON().tablename != req.body.tablename) {res.json('data does not match verification')}
            else {
                 res.json(data.select(projection(req, res)))
            }
        })
        .catch(err => res.status(400).json('Error: ' + err))
    }, "findbyid", req, res)
})

//findone
router.route('/one').get((req, res) => {
    Authorization( function () {
        Data.findOne(filter(req, res), projection(req, res), options(req, res), callback(req, res))
        .then(data => res.json(data))
        .catch(err => res.status(400).json('Error: ' + err))
    }, "findone", req, res)
})

//Deleters

//deleteone
router.route('/one').delete((req, res) => {
    Authorization( function () {
        Data.findOneAndDelete( filter(req, res), options(req, res), callback(req, res))
        .then(res.json("Data deleted"))
        .catch(err => res.status(400).json('Error: ' + err))
    }, "deleteone", req, res)
})


 
// findbyidanddelete
router.route('/:id').delete((req, res) => {
    Authorization( function () {
        Data.findById(req.params.id, callback(req, res))
        .then(data => {
            if (data.toJSON().tablename != req.body.tablename) {res.json('data does not match verification')}
            else {
                data.delete()
                .then(() => res.json('Data deleted'))
                .catch(err => res.status(400).json('Error: ' + err))
            }
        })
        .catch(err => res.status(400).json('Error: ' + err))
    }, "findbyidanddelete", req, res)   
})

// deleteMany
router.route('/').delete((req, res) => {
    Authorization( function () {
        Data.deleteMany(filter(req, res), options(req, res), callback(req, res))
        .then(() => res.json('Data deleted'))
        .catch(err => res.status(400).json('Error: ' + err))
    },"deletemany", req, res)   
})

//Updaters
//updateone
router.route('/updateone').post((req, res) => {
    Authorization( function () {
        const doc = req.body.data
        Data.findOneAndUpdate( filter(req, res), doc, options(req, res), callback(req, res))
        .then(res.json("Data updated!"))
        .catch(err => res.status(400).json('Error: ' + err))
    },"updateone", req, res)
})

//findbyidandupdate
router.route('/update/:id').post((req, res) => {
    Authorization( function () {
        Data.findById(req.params.id, callback(req, res))
        .then(data => { 
           
            if (data.toJSON().tablename != req.body.tablename) {res.json('data does not match verification')}
            else{
            for (const prop in req.body.data) {
                console.log(prop)
                console.log(req.body.data)
                data.set(prop, req.body.data[prop]);}
                
            
            data.save()
                .then(() => res.json('Data updated'))
                .catch(err => res.status(400).json('Error: ' + err))} 
            }   
    )
        .catch(err => res.status(400).json('Error: ' + err))
    },"findbyidandupdate", req, res) 
})

//updateMany
router.route('/update').post((req, res) => {
    Authorization( function () {
        const doc = req.body.data
        Data.updateMany(filter(req, res), doc , options(req, res), callback(req, res))
        .then(res.json("Data updated!") )
        .catch(err => res.status(400).json('Error: ' + err))
    },"updatemany", req, res) 
})


module.exports = router     