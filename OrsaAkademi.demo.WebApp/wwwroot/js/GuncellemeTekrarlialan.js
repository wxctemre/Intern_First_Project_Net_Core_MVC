window.guncellemetekrarlialanclone = [];
function dinlemeyapforclone(count) {
    if (count > 0) {

    }
}
document.addEventListener('change', function (event) {
    if (event.target && event.target.classList.contains('schoolfileinput')) {
        guncellemetekrarlialan(event.target);
    }
});
 
var tekrarlialanclonecount = {};

 
function guncellemetekrarlialan(input) {
    const schoolarea = input.closest('.form-tekrarlialan').querySelector('.schoolarea');
    const files = input.files;
    const closestTekrarliAlan = input.closest('.form-tekrarlialan');

    if (closestTekrarliAlan) {
        var okulAlan = closestTekrarliAlan.id;
    }
    var okulismi = okulAlan;

   
    if (!guncellemetekrarlialanclone[okulismi]) {
        guncellemetekrarlialanclone[okulismi] = [];
    }


    [...files].forEach(file => {
        if (file.type.match('image/jpeg') || file.type.match('image/png') || file.type.match('image/jpg')) {
            const reader = new FileReader();

            reader.addEventListener('load', function () {
                const imageData = this.result;
                const newImage = new Image();
                const imageId = sssadsd(okulismi);

                newImage.src = imageData;
                newImage.id = imageId;
                newImage.alt = "Yüklenen Fotoğraf";
                newImage.style.maxWidth = "100%";
                newImage.style.maxHeight = "100%";
                newImage.style.width = "100%";
                newImage.style.height = "100%";
                const containerDiv = document.createElement('div');
                containerDiv.className = 'image-container col-6 mb-2'


                const deleteButton = document.createElement('button');
                deleteButton.innerHTML = 'X';
                deleteButton.className = 'btn btn-danger rounded-circle d-inline-block mb-2';
                deleteButton.style.position = 'absolute';
                deleteButton.style.transform = 'translate(-100%, -0%)';
                deleteButton.style.fontSize = '0.5rem';

                deleteButton.addEventListener('click', function () {
                    const imageId = this.previousSibling.id;
                    const index = guncellemetekrarlialanclone[okulismi].findIndex(img => img.id === imageId);
                    if (index !== -1) {
                        guncellemetekrarlialanclone[okulismi].splice(index, 1);
                        const deletedElement = this.parentElement;
                        deletedElement.remove();
                        for (let i = index; i < guncellemetekrarlialanclone[okulismi].length; i++) {
                            const updatedImageId = "schoolimage_" + (i + 1);
                            const updatedElement = document.getElementById("schoolImage_" + (i + 2));
                            if (updatedElement) {
                                updatedElement.id = updatedImageId;
                                console.log(updatedElement.id);

                            }
                        }


                        updateGroupedFilesIDs(index, okulismi);

                        reidimages(okulismi);

                    }

                });
                function updateGroupedFilesIDs(startIndex, okulismi) {
                    const files = guncellemetekrarlialanclone[okulismi];
                    for (let i = startIndex; i < files.length; i++) {
                        files[i].id = "schoolimage_" + (i + 1);
                    }
                }
           
                containerDiv.appendChild(newImage);
                containerDiv.appendChild(deleteButton);
                schoolarea.appendChild(containerDiv);



            });

            reader.readAsDataURL(file);
            guncellemetekrarlialanclone[okulismi].push(file);

        }
        reidimages(okulismi);
    });



}


function reidimages(okulismi) {
    guncellemetekrarlialanclone[okulismi].forEach((image, index) => {
        image.id = 'schoolImage_' + (index + 1);
    });
}


function sssadsd(okulismi) {
    if (!tekrarlialanclonecount[okulismi]) {
        tekrarlialanclonecount[okulismi] = 1;
    } else {
        tekrarlialanclonecount[okulismi]++;
    }
    return 'schoolImage_' + tekrarlialanclonecount[okulismi];
}
function idver(okulismi) {
    guncellemetekrarlialanclone[okulismi] = 1;
}


