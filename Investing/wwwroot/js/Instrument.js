document.getElementById('allInstruments').addEventListener('click', function (e) {
    const row = e.target.closest('tr');
    if (row && row.dataset.id) {

        const results = allInstruments.find(instrument => {
            return instrument.secId.toLowerCase().includes(row.dataset.id.toLowerCase());
        });
        window.location.href = `/${results.typeInstrument}/Detail${results.typeInstrument}?substring=${encodeURIComponent(results.secId)}`;
    }
}); 