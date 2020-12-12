const getWord = async () => {
    const res = await fetch('/getword')
    const word = await res.text()
    console.log("hakl;jsdg" + word + res)
    document.getElementById('randomWord').innerText = word
} 

