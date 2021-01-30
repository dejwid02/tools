$(document).ready(function () {
    //$(".btnRecord").click(function (e) {
    //    alert(e.target.closest("div").children[0].children[0].attributes[0].nodeValue);
    //    //alert(e.target.children("option:selected").val());
    //});
});

function filter() {
   

    var movies = document.getElementsByClassName("movie");
    Array.prototype.forEach.call(movies, function (el) {filterItem(el)});


}

function filterItem(element) {
    const input = document.getElementsByClassName("search-input").item(0);
    const text = input.value;

    const title = element.getElementsByClassName("card-title")[0].innerText;
    if (!title.includes(text) && text.length > 3) {
        element.classList.add("d-none");
    }
    else {
        element.classList.remove("d-none");
    }

}