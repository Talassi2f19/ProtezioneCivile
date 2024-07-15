let item = document.querySelector('.icon-hamburger');
item.addEventListener("click", function() {
  document.body.classList.toggle('menu-open');
});

const myNav = document.getElementById('mynav');
window.onscroll = function () { 
    if (document.body.scrollTop >= 2 || document.documentElement.scrollTop >= 2 ){
        myNav.classList.add("nav-colored");
        myNav.classList.remove("nav-transparent");
    } 
    else {
        myNav.classList.add("nav-transparent");
        myNav.classList.remove("nav-colored");
    }
};