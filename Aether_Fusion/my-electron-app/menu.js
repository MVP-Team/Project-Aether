const choice1 = document.getElementById("choice1");
const choice2 = document.getElementById("choice2");
const choice3 = document.getElementById("choice3");
const choice4 = document.getElementById("choice4");

const content1 = document.getElementById("content1");
const content2 = document.getElementById("content2");
const content3 = document.getElementById("content3");
const content4 = document.getElementById("content4");

// Add event listeners to the buttons
choice1.addEventListener("click", function() {
    showContent(content1);
});

choice2.addEventListener("click", function() {
    showContent(content2);
});

choice3.addEventListener("click", function() {
    showContent(content3);
});

choice4.addEventListener("click", function() {
    showContent(content4);
});

// showContent function to show the corresponding content
function showContent(content) {
    const currentContent = document.querySelector(".visible");
    currentContent.classList.remove("visible");
    currentContent.classList.add("hidden");

    content.classList.remove("hidden");
    content.classList.add("visible");
}

const searchForm = document.getElementById("search-form");

const searchInput = document.getElementById("search-input");
searchInput.addEventListener("keyup", function(event) {
  if (event.key === "Enter") {
    event.preventDefault();
    searchForm.submit();
  }
});



var nodeConsole = require('console');
var myConsole = new nodeConsole.Console(process.stdout, process.stderr);


