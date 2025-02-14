function Login() {
    var girisEkrani = document.getElementById('GirisEkrani1');
    var loginButonu = document.getElementById('loginacma');

    if (girisEkrani.style.display === 'none') {
        girisEkrani.style.display = 'block';
        loginButonu.style.display = 'none';  
    } else {
        girisEkrani.style.display = 'none';
    }
}
function LoginDisplayOpen() {

    var girisEkrani = document.getElementById('GirisEkrani1');
    var loginButonu = document.getElementById('loginacma');

    girisEkrani.style.display = 'none';
    if (girisEkrani.style.display === 'none') {
        loginButonu.style.display = 'block';
        girisEkrani.style.display = 'none';  
    } else {
        loginButonu.style.display = 'none';
    }

}
function kullanicikaydet(modalID) {
     
    var emailparam = document.querySelector('#' + modalID + ' input[name="email"]').value;
    var passwordparam = document.querySelector('#' + modalID + ' input[name="psw"]').value;
    var confirmPassword = document.querySelector('#' + modalID + ' input[name="psw-repeat"]').value;

  
    if (passwordparam !== confirmPassword) {
        alert("Girdiğiniz şifreler eşleşmiyor!");
        return;
    }

    var emaildeseni = /^[a-za-z0-9_-]+@[a-za-z0-9.-]+\.[a-za-z]{2,}$/;
    if (!emaildeseni.test(emailparam)) {
        alert("lütfen geçerli bir e-posta adresi girin.");
        return;
    }

    var kullanicikayit = {
        email: emailparam,
        sifre: passwordparam
    };


    $.ajax(
        {
            url: "/kullaniciregister",
            type: "POST",
            data: kullanicikayit,
            success: function (data) {
                if (data.email != null) {
                    alert("Kayıt işlemi başarıyla tamamlandı!");
                    $('#' + modalID).hide();
 
                }
         
            }
 
        });



     

}
window.sessionad;
function submitForm(modalId) {
    var emailparam = document.querySelector('#' + modalId + ' input[name="email"]').value;
    var passwordparam = document.querySelector('#' + modalId + ' input[name="psw"]').value;
   

 
    var emaildeseni = /^[a-za-z0-9_-]+@[a-za-z0-9.-]+\.[a-za-z]{2,}$/;
    if (!emaildeseni.test(emailparam)) {
        alert("lütfen geçerli bir e-posta adresi girin.");
        return;
    }

    var kullanicikayit = {
        email: emailparam,
        sifre: passwordparam
    };
    $.ajax(
        {
            url: "/kullaniciLogin",
            type: "PUT",
            data: kullanicikayit,
            success: function (data) {
                if (data != null) {
                    alert("Giriş Başarılı");
                    window.sessionStorage.setItem('kullaniciAdi', data.ad); 

                    window.location.href = 'https://localhost:44354/PersonelEkleme/PersonelListesi';
                }
                else {
                    alert("Yanlış Mail veya Şifre Girdiniz");
                }

            }

        });




 
}

 

 
function showRegistrationForm() {
    document.getElementById('kayit').style.display = 'block';
}



