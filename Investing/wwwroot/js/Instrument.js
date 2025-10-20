document.getElementById('allInstruments').addEventListener('click', function (e) {
    const row = e.target.closest('tr');
    if (row && row.dataset.id) {

        const results = allInstruments.find(instrument => {
            return instrument.secId.toLowerCase().includes(row.dataset.id.toLowerCase());
        });
        window.location.href = `/${results.typeInstrument}/Detail${results.typeInstrument}?substring=${encodeURIComponent(results.secId)}`;
    }
});

async function filterByStock(){
    const tableInstruments = document.getElementById('allInstruments');
    var tr = tableInstruments.getElementsByTagName("tr");
    var td;
    for (i = 0; i < tr.length; i++) {
        td = tr[i].getElementsByTagName("td")[1];
        if (td) {
            txtValue = (td.textContent || td.innerText).toLowerCase();
            if (txtValue.includes("акции")) {
                tr[i].style.display = "";
            } else {
                tr[i].style.display = "none";
            }
        }
    }
    let name = document.getElementById('visible-insrument-filter');
    name.style.display = "";
    name.textContent = 'Выбраны акции';
}
async function filterByCurrency() {
    const tableInstruments = document.getElementById('allInstruments');
    var tr = tableInstruments.getElementsByTagName("tr");
    var td;
    for (i = 0; i < tr.length; i++) {
        td = tr[i].getElementsByTagName("td")[1];
        if (td) {
            txtValue = (td.textContent || td.innerText).toLowerCase();
            if (txtValue.includes("валюта")) {
                tr[i].style.display = "";
            } else {
                tr[i].style.display = "none";
            }
        }
    }
    let name = document.getElementById('visible-insrument-filter');
    name.style.display = "";
    name.textContent = 'Выбрана валюта';
}
async function filterByBond() {
    const tableInstruments = document.getElementById('allInstruments');
    var tr = tableInstruments.getElementsByTagName("tr");
    var td;
    for (i = 0; i < tr.length; i++) {
        td = tr[i].getElementsByTagName("td")[1];
        if (td) {
            txtValue = (td.textContent || td.innerText).toLowerCase();
            if (txtValue.includes("облигации")) {
                tr[i].style.display = "";
            } else {
                tr[i].style.display = "none";
            }
        }
    }
    let name = document.getElementById('visible-insrument-filter');
    name.style.display = "";
    name.textContent = 'Выбраны облигации';
}
async function filterByFunds() {
    const tableInstruments = document.getElementById('allInstruments');
    var tr = tableInstruments.getElementsByTagName("tr");
    var td;
    for (i = 0; i < tr.length; i++) {
        td = tr[i].getElementsByTagName("td")[1];
        if (td) {
            txtValue = (td.textContent || td.innerText).toLowerCase();
            if (txtValue.includes("фонды")) {
                tr[i].style.display = "";
            } else {
                tr[i].style.display = "none";
            }
        }
    }
    let name = document.getElementById('visible-insrument-filter');
    name.style.display = "";
    name.textContent = 'Выбраны фонды';
}
async function filterByFutures() {
    const tableInstruments = document.getElementById('allInstruments');
    var tr = tableInstruments.getElementsByTagName("tr");
    var td;
    for (i = 0; i < tr.length; i++) {
        td = tr[i].getElementsByTagName("td")[1];
        if (td) {
            txtValue = (td.textContent || td.innerText).toLowerCase();
            if (txtValue.includes("фьючерсы")) {
                tr[i].style.display = "";
            } else {
                tr[i].style.display = "none";
            }
        }
    }
    let name = document.getElementById('visible-insrument-filter');
    name.style.display = "";
    name.textContent = 'Выбраны фьючерсы';
}



async function clearFilter() {
    const tableInstruments = document.getElementById('allInstruments');
    var tr = tableInstruments.getElementsByTagName("tr");
    for (let i = 0; i < tr.length; i++) {
        tr[i].style.display = "";
    }
    let name = document.getElementById('visible-insrument-filter');
    name.style.display = "none";
}