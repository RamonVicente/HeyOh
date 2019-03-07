
let visualizar_cracha = document.querySelector('#ver-cracha');
let visualizar_logomarca = document.querySelector('#ver-logomarca');

//editor de texto no textarea
bkLib.onDomLoaded(nicEditors.allTextAreas);

/**
 * renderiza logomarca na tela quando ela é carregada
 */
document.querySelector('#layoutCracha').addEventListener('change', function(){

    let layoutCracha = this.files;

    if (layoutCracha && layoutCracha[0]) {
        RenderizarImagem(layoutCracha[0], visualizar_cracha);
    }

});

/**
 * renderiza logomarca na tela quando ela é carregada
 */
document.querySelector('#logoMarca').addEventListener('change', function () {

    let arquivoImagem = this.files;

    if (arquivoImagem && arquivoImagem[0]) {
        RenderizarImagem(arquivoImagem[0], visualizar_logomarca);
    }

});

/**
 * Essa função renderiza a imagem carregada no upload
 * @argument  {string} obrigatorio
 * @argument  {string} obrigatorio
 * 
 */
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

/**
 * aqui é um código onde o tem a opção de digitar texto 
 * em qualquer parte da imagem do canvas
 */
/*var canvas = new fabric.Canvas("#c");
// Define an array with all fonts
var fonts = ["Pacifico", "VT323", "Quicksand", "Inconsolata"];

var textbox = new fabric.Textbox('Lorum ipsum dolor sit amet', {
    left: 50,
    top: 50,
    width: 150,
    fontSize: 20
});
canvas.add(textbox).setActiveObject(textbox);
*/

