// Obtener los elementos de los campos de entrada
var Cantidad = document.getElementById('Cantidad');
var Precio = document.getElementById('Precio');
var rTotal = document.getElementById('rTotal');

// Función para calcular la suma y actualizar el campo de entrada
function calcularSuma() {
    var result = parseFloat(Cantidad.value) * parseFloat(Precio.value);
    rTotal.value = isNaN(result) ? '' : result;
}

//Agregar un evento onchange a cada campo de entrada para actualizar la suma
Cantidad.addEventListener('change', calcularSuma);
Precio.addEventListener('change', calcularSuma);
