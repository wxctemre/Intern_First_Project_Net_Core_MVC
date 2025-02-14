async function Guncelleme() {
    const fotografyuklemealaniguncelleme = new FormData();
    let deletedindex = [];
    const password = $("#Sifre").val();
    const telefon = $("#PersonelTelefon").val().trim();
    const email = $("#PersonelEmail").val().trim();
    const emailDeseni = /^[a-zA-Z0-9_\-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$/;
    const telefonDeseni = /^\d{11}$/;

    if ($("#PersonelAd").val().trim() === "" || $("#PersonelSoyad").val().trim() === "" || telefon === "" || email === "" || $("#PersonelDogumTarihi").val() === "" || password === "" || $("#TekrarSifre").val() === "") {
        alert("Lütfen eklemek istediğiniz değerleri boş bırakmayınız");
        return;
    }

    if (!emailDeseni.test(email)) {
        alert("Lütfen geçerli bir e-posta adresi girin.");
        return;
    }

    //if (!telefonDeseni.test(telefon)) {
    //    alert("Lütfen geçerli bir telefon numarası girin.");
    //    return;
    //}

    //if (password !== $("#TekrarSifre").val()) {
    //    alert("Şifreler uyuşmuyor.");
    //    return;
    //}

  

    const url = window.location.href;
    const parts = url.split('/');
    const id = parseInt(parts[parts.length - 1]);

    const personel = {
        Id: id,
        Ad: $("#PersonelAd").val().trim(),
        Soyad: $("#PersonelSoyad").val().trim(),
        Telefon: telefon,
        Email: email,
        DogumTarihi: $("#PersonelDogumTarihi").val(),
        Sifre: password
    };
    var scformdata = new FormData();
    $('.form-school').each((index, form) => {
        const schoolId = $(form).find('[id^="veritabaniokul_"]').val();
        const yenialanschoolid = $(form).find('[id^="yenialanokul_"]').val();
        const graduationYear = $(form).find(' [id^="veritabaniokulmezuniyet_"]').val();
        const yenialangraduationYear = $(form).find('[id^="yenialanmezuniyet_"]').val();
        const customDivId = $(form).find('[id^="customDiv_"]').attr('id').split('_')[1];
        const okulBilgiDiv = $(form).find('.okulBilgiId');
        const okulBilgiId = okulBilgiDiv.length > 0 ? okulBilgiDiv.text() : null;

 
        if (typeof schoolId !== 'undefined' && schoolId !== null && schoolId !== '') {
            scformdata.append(`schools[${index}][schoolId]`, schoolId);
            scformdata.append(`schools[${index}][graduationYear]`, graduationYear);
        } else {
       
            scformdata.append(`schools[${index}][yenialanokul]`, yenialanschoolid);
            scformdata.append(`schools[${index}][yenialangraduationYear]`, yenialangraduationYear);
        }

        scformdata.append(`schools[${index}][customDivId]`, customDivId);
        if (okulBilgiId !== null) {
            scformdata.append(`schools[${index}][okulBilgiId]`, okulBilgiId);
        }

        const images = allImages[form.id];
        if (images) {
            images.forEach((image, imageIndex) => {
                scformdata.append(`schools[${index}][images][${imageIndex}]`, image.file, image.file.name);
            });
        }
    });
    scformdata.append('personelid', id);


    try {
        const validationResponse = await $.ajax({
            url: "/modelvalid/validatemodel",
            type: "POST",
            dataType: "json",
            data: personel
        });

        if (!validationResponse.success) {
            alert(validationResponse.errors);
            return;
        }

        const updateResponse = await $.ajax({
            url: "/PersonelDuzenleme/PersoneliGuncelle",
            type: "PUT",
            dataType: "json",
            data: personel
        });

        if (UploadedImages.length > 0) {
            $.each(UploadedImages, (index, file) => {
                fotografyuklemealaniguncelleme.append(`file_${index}`, file);
            });
            fotografyuklemealaniguncelleme.append(`PersonelId`, updateResponse.id);
        }

        await $.ajax({
            url: "/PersonelEkleme/PersonelFotografiKaydet",
            type: 'POST',
            data: fotografyuklemealaniguncelleme,
            processData: false,
            contentType: false
        });

        if (deletedindex.length > 0) {
            await $.ajax({
                url: "/FotografSil",
                type: "DELETE",
                data: { idler: window.silinenpersonelfotograflari }
            });
        }
        await $.ajax({
            url: "/OkulMedyaSil",
            type: "DELETE",
            data: { idler: window.silinenokulfotograflari } 
         
        });
        await $.ajax({
            url: '/TekrarliAlanin/tekrarlialanGuncelle',
            type: 'PUT',
            data: scformdata,
            processData: false,
            contentType: false
        });

        alert("Güncelleme İşlemi Başarılı");
        window.location.href = 'https://localhost:44354/PersonelEkleme/PersonelListesi';
    } catch (error) {
        console.error("Güncelleme hatası:", error);
        alert("Güncelleme sırasında bir hata oluştu.");
    }
}
