async function choice(inputVal){
    textarea = document.querySelector("#outputC2");
    let output = "";
    let words = inputVal.split(" ");
    if (inputVal.toLowerCase() == "random joke") {
      await fetch("http://localhost:5000/joke", {
        mode: "cors",
      })
        .then((response) => response.text())
        .then((data) => {
          output = data;
        });
    } else if (words[0].toLowerCase() == "search") {
      await fetch(`http://localhost:5000/search=${inputVal.substring(inputVal.indexOf(" "))}`, {
        mode: "cors",
      })
        .then((response) => response.text())
        .then((data) => {
          output = "Search will be resumed in your browser!";
        });
    } else if(words[0].toLowerCase() == "open"){
      await fetch(`http://localhost:5000/open=${inputVal.substring(inputVal.indexOf(" "))}`, {
        mode: "cors",
      })
        .then((response) => response.text())
        .then((data) => {
          output = "Application will be opened!";
        });
    }else if(words[0].toLowerCase() == "translate"){
      await fetch(`http://localhost:5000/translate=${inputVal.substring(inputVal.indexOf(" "))}`, {
        mode: "cors",
      })
        .then((response) => response.text())
        .then((data) => {
          output = data;
        });
    }else if(words[0].toLowerCase() == "close"){
      close();
      output = "Bye! See you later!"
    }else {
      output = "Sorry, the requested command is unknown.";
    }
    textarea.innerHTML = "";
    for (i = 0; i < output.length; i++) {
      await sleep(30);
      textarea.innerHTML += output.charAt(i);
    }
}
  

function sleep(ms) {
  return new Promise((resolve) => setTimeout(resolve, ms));
}