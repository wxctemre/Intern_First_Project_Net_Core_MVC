$(document).ready(function () {
    var kullaniciAdi = window.sessionStorage.getItem('kullaniciAdi');  
    if (kullaniciAdi) {
        $('#kullaniciadi').html('<span class="nav-link text-dark">' + kullaniciAdi + '</span>');
        $('#logout').html('<a href="#" id="logoutBtn" class="nav-link text-dark">Çıkış Yap</a>');
    }

    
   

    $('#logoutBtn').click(function (e) {
        e.preventDefault(); 

        $.ajax({
            url: '/Logout', 
            type: 'POST',  
            success: function (response) {
         
                window.sessionStorage.removeItem('kullaniciAdi');  
                window.location.href = 'https://localhost:44354/';
            },
            error: function (xhr, status, error) {
           
                console.error('Logout işlemi sırasında bir hata oluştu:', error);
           
                window.sessionStorage.removeItem('kullaniciAdi');  
                window.location.href = 'https://localhost:44354/';
            }
        });
    });
});