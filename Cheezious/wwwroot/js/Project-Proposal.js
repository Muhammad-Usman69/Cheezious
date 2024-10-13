function openCity(cityName) {
    
    var i;
    var x = document.getElementsByClassName("city");
    for (i = 0; i < x.length; i++) {
      x[i].style.display = "none";  
    }
    document.getElementById(cityName).style.display = "block"; 
   
  }



  function openNav() {
    document.getElementById("mySidenav").style.width = "250px";
    document.getElementById("main").style.marginLeft = "250px";
  }
  
  function closeNav() {
    document.getElementById("mySidenav").style.width = "0";
    document.getElementById("main").style.marginLeft = "0";
  };










document.addEventListener('DOMContentLoaded', function () {
  const tabLinks = document.querySelectorAll('.tab-links a');
  const tabs = document.querySelectorAll('.tab');

  tabLinks.forEach(link => {
      link.addEventListener('click', function (event) {
          event.preventDefault();

          tabLinks.forEach(link => link.parentElement.classList.remove('active'));
          tabs.forEach(tab => tab.classList.remove('active'));

          this.parentElement.classList.add('active');
          const target = document.querySelector(this.getAttribute('href'));
          target.classList.add('active');
      });
  });
});




document.querySelector('.imageUpload').addEventListener('change', function (event) {
    const file = event.target.files[0];
    const reader = new FileReader();

    reader.onload = function (e) {
        const img = document.querySelector('.imagePreview');
        img.src = e.target.result;
        img.style.display = 'block';
    };

    if (file) {
        reader.readAsDataURL(file);
    }
});
