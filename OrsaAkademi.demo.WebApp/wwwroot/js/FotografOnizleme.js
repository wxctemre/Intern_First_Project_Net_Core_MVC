

const fileinput = document.getElementById('photoUpload');
const text = document.getElementById('defaultText');
window.UploadedImages = [];
let imagecounter = 1;

function Textvisibility() {
    if (!!UploadedImages) {
        $('#defaultText').hide();
    }
    else{
        $('#defaultText').show();
    }
}

fileinput.addEventListener('change', function () {
    const previewArea = document.getElementById('previewArea');

    [...this.files].forEach(file => {
        if (file.type.match('image/jpg') || file.type.match('image/png') || file.type.match('image/jpeg')) {
            const reader = new FileReader();
            const newImage = new Image();
            let imageId;

            reader.addEventListener('load', function () {
                const imageData = this.result;
                imageId = 'previewImage_' + imagecounter++;
                const imagearr = {
                    id: imageId,
                    name: file.name,
                    path: this.result,
                };
                const containerDiv = document.createElement('div');
                containerDiv.className = 'image-container col-12 mb-2';
                newImage.src = imageData;
                newImage.id = imageId;
                newImage.alt = "Yüklenen Fotoğraf";
                newImage.style.maxWidth = "100%";
                newImage.style.maxHeight = "100%";
                newImage.style.width = "100%";
                newImage.style.height = "100%";
                const deleteButton = document.createElement('button');
                deleteButton.style.position = 'absolute';
                deleteButton.innerHTML = 'X';
                deleteButton.className = 'btn btn-danger rounded-circle d-inline-block';
                deleteButton.style.transform = 'translate(-100%,-0%)';
                deleteButton.style.fontSize = '0.5rem';
                deleteButton.addEventListener('click', function () {
                    previewArea.removeChild(containerDiv);
                    window.UploadedImages = window.UploadedImages.filter(img => img.name !== file.name);
                    document.getElementById("photoUpload").value = "";
                    imagecounter--;
                    if (window.UploadedImages.lenght === 0) {
                        Textvisibility()
                    }


                });

                containerDiv.appendChild(newImage);
                containerDiv.appendChild(deleteButton);
                previewArea.appendChild(containerDiv);

            });
            reader.readAsDataURL(file);
            window.UploadedImages.push(file);
            Textvisibility()


        }





    });



});