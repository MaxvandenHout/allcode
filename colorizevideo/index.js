const fs = require('fs');
const deepai = require('deepai'); 
const fetch = require('node-fetch');
const request = require('request');

deepai.setApiKey('quickstart-QUdJIGlzIGNvbWluZy4uLi4K');
(async function(){
    let filesleft = true;
   
    let readloc = ""
    for(i = 1; i < 2; i++)
    {
        console.log(i)
        Colorize(`./read/${i}.jpeg`, `./write/${i}.jpg`)
    }
})()

function getimg(readlocation) {
    fs.readFile(readlocation, (err, data) => {if (err) {console.log(err)} return data})
}

function Colorize(readlocation, writelocation) {
    
    deepai.setApiKey('9938ece3-7834-4c77-bb6c-c37911e1c588');
    deepai.callStandardApi("colorizer", {
            image: fs.createReadStream(readlocation)
    })
    .then(data => {
        download(data.output_url, writelocation)
        console.log(`${writelocation} is written`)
    })
    .catch(err => {console.log(err)})  
}

function WriteImg(writelocation, url){
    console.log(url)
    fetch(url)
    .then(data => {
        console.log(data)
        fs.createWriteStream(writelocation, data, ()=>{console.log`${writelocation} is written` })
    })
    
}

async function download(url, dest) {

    /* Create an empty file where we can save data */
    const file = fs.createWriteStream(dest);

    /* Using Promises so that we can use the ASYNC AWAIT syntax */
    await new Promise((resolve, reject) => {
      request({
        /* Here you should specify the exact link to the file you are trying to download */
        uri: url,
        gzip: true,
      })
          .pipe(file)
          .on('finish', async () => {
            console.log(`The file is finished downloading.`);
            resolve();
          })
          .on('error', (error) => {
            reject(error);
          });
    })
        .catch((error) => {
          console.log(`Something happened: ${error}`);
        });
}