function makearray()  {
let arr = []
    for (i = 0; i <129; i++ ){
        for (b = 0; b <129; b++ ){
            arr.push(i^b)
        }
    }
    console.log(128^128)
    console.log(arr.length)
}
makearray()