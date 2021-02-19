$(document).ready(function () {
    //$(".btnRecord").click(function (e) {
    //    alert(e.target.closest("div").children[0].children[0].attributes[0].nodeValue);
    //    //alert(e.target.children("option:selected").val());
    //});
});

function filter() {
   

    const movies = Array.from(document.getElementsByClassName("movie"));
    const recordings = Array.from(document.getElementsByClassName("recording"));
    const tvItems = Array.from(document.getElementsByClassName("tv-item"));
    const all = movies.concat(recordings).concat(tvItems);
    Array.prototype.forEach.call(all, function (el) {
        filterItem(el);
    });


}

function filterItem(element) {
    const input = document.getElementsByClassName("search-input")[0];
    const text = input.value.toLowerCase();

    const category = element.getElementsByClassName("movie-category")[0].innerText.toLowerCase();
    const title = element.getElementsByClassName("card-title")[0].innerText.toLowerCase();
    if (title.includes(text) || category.includes(text) || text.length < 3) {
        element.style.display = "block";
    }
    else {

        element.style.display = "none";
    }

}