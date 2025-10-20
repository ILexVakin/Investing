
var metalls = ["gld", "plt", "slv", "xag", "xr","pld"] 
async function getAllCurrency() {
    const tableCurrency = document.getElementById('table-currency');
    var tr = tableCurrency.getElementsByTagName("tr");
    for (let i = 0; i < tr.length; i++) {
        tr[i].style.display = "";
    }
    let name = document.getElementById('visible-currency-name');
    name.style.display = "none";
}
async function getNededMetalls() {
    const tableCurrency = document.getElementById('table-currency');
    var tr = tableCurrency.getElementsByTagName("tr");
    var td;
    for (i = 0; i < tr.length; i++) {
        td = tr[i].getElementsByTagName("td")[0];
        if (td) {
            txtValue = td.textContent || td.innerText;
            if (metalls.some(metal =>
                txtValue.toUpperCase().includes(metal.toUpperCase())
            )) {
                tr[i].style.display = "";
            } else {
                tr[i].style.display = "none";
            }
        }
    }
    let name = document.getElementById('visible-currency-name');
    name.style.display = "";
    name.textContent = 'Выбран металл';
}
async function getNededCurrency() {
    const tableCurrency = document.getElementById('table-currency');
    var tr = tableCurrency.getElementsByTagName("tr");
    var td;
    for (i = 0; i < tr.length; i++) {
        td = tr[i].getElementsByTagName("td")[0];
        if (td) {
            txtValue = td.textContent || td.innerText;
            if (!metalls.some(metal =>
                txtValue.toUpperCase().includes(metal.toUpperCase())
            )) {
                tr[i].style.display = "";
            } else {
                tr[i].style.display = "none";
            }
        }
    }
    let name = document.getElementById('visible-currency-name');
    name.style.display = "";
    name.textContent = 'Выбрана валюта';
}
