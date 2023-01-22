function ClickPress(event) {
  if (event.key == "Enter") {
      inputVal = document.querySelector("#inputField").value;
      logo = document.getElementById('aetherLogo');
      mic = document.getElementById('micC2');
      label = document.getElementById('labelC2');
      textarea = document.querySelector("#outputC2");
      taBorder = document.querySelector("#outBorderC2");

      textarea.classList.remove("opacity-0");
      taBorder.classList.add("border");
      /*
      switch (inputVal.substring(0, inputVal.indexOf(" ")).toLowerCase()){
        case "random":
        
        break;
        default:
        break;
      }
      */
      fetch("localhost:5000/joke") 
      .then(response => response.text())
      .then((data) => {
        console.log(data);
      });
       
      textarea.innerHTML = inputVal;
      logo.classList.add('-translate-y-96');
      logo.classList.add('opacity-0');
      label.classList.add('-translate-y-64');
      mic.classList.add('-translate-y-64');
    }
}