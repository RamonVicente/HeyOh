
let visualizar_cracha = document.querySelector('#ver-cracha');
let visualizar_logomarca = document.querySelector('#ver-logomarca');

var canvas = new fabric.Canvas('c');
// Define an array with all fonts
var fonts = ["Pacifico", "VT323", "Quicksand", "Inconsolata"];

var textbox = new fabric.Textbox('Lorum ipsum dolor sit amet', {
    left: 50,
    top: 50,
    width: 150,
    fontSize: 20
});
canvas.add(textbox).setActiveObject(textbox);

//renderiza layout na tela quando ela é carregada
document.querySelector('#layoutCracha').addEventListener('change', function(){

    let layoutCracha = this.files;

    if (layoutCracha && layoutCracha[0]) {
        RenderizarImagem(layoutCracha[0], visualizar_cracha);
    }

});
//renderiza logomarca na tela quando ela é carregada
document.querySelector('#logoMarca').addEventListener('change', function () {

    let arquivoImagem = this.files;

    if (arquivoImagem && arquivoImagem[0]) {
        RenderizarImagem(arquivoImagem[0], visualizar_logomarca);
    }

});


let RenderizarImagem = function (imagemCarregada, exibirImagem) {

    let leitorArquivo = new FileReader;
    let imagem = new Image;

    leitorArquivo.readAsDataURL(imagemCarregada);
    leitorArquivo.onload = function (_imagemCarregada) {

        imagem.src = _imagemCarregada.target.result;
        imagem.onload = function () {

            exibirImagem.setAttribute('src', _imagemCarregada.target.result);
            exibirImagem.style.display = 'block';

        }

    }

}

var ClearPreview = function () {
    $("#imageBrowes").val('');
    $("#description").text('');
    $("#imgPreview").hide();

}

var Uploadimage = function () {

    var file = $("#imageBrowes").get(0).files;

    var data = new FormData;
    data.append("ImageFile", file[0]);
    data.append("ProductName", "SamsungA8");

    $.ajax({

        type: "Post",
        url: "/Test/ImageUpload",
        data: data,
        contentType: false,
        processData: false,
        success: function (response) {
            ClearPreview();

            $("#uploadedImage").append('<img src="/UploadedImage/' + response + '" class="img-responsive thumbnail"/>');


        }

    })


    //
}