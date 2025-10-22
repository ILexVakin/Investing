let searchTimeout;
let response = [];

async function loadAllInstruments() {
    try {
        response = await fetch('/api/home/getinstruments');
        if (response.ok) {
            allInstruments = await response.json();
            isDataLoaded = true;
        }
    } catch (error) {
        console.error('Ошибка загрузки:', error);
    }
}

async function funcBrowseSort(searchTerm) {
    try {
        const results = allInstruments.filter(instrument => {
            return instrument.secName.toLowerCase().includes(searchTerm.toLowerCase());
        }).slice(0, 10);

        displayResults(results, searchTerm);
    }
    catch (error) {
        console.error('Ошибка загрузки:', error);
    }
} 

async function searchOnServer() {
    let searchTerm = document.getElementById('searchInput').value;
    if (searchTerm.length === 0) {
        hideDropdown();
        return;
    }
    try {
        const response = await fetch(`/Instrument/GetListInstruments?substring=${encodeURIComponent(searchTerm)}`);

        if (response.ok) {
            const results = await response.json();
            displayResults(results, searchTerm);
        } else {
            console.error('Ошибка поиска:', response.status);
            hideDropdown();
        }
    } catch (error) {
        console.error('Ошибка при поиске:', error);
        hideDropdown();
    }
}

async function detectedController(searchTerm) {
    const results = allInstruments.find(instrument => {
        return instrument.secName.toLowerCase().includes(searchTerm.toLowerCase());
    });
    const response = await fetch(`/${results.typeInstrument}/Detail${results.typeInstrument}?substring=${encodeURIComponent(results.secId)}`);

}

async function translateInstrument() {

}

// Отображение результатов
function displayResults(results, searchTerm) {
    const dropdownList = document.getElementById('dropdown-src');
    const dropdownContainer = document.getElementById('dropdownResults');

    dropdownList.innerHTML = "";

    if (results && results.length > 0) {
        results.forEach(item => {
            let displayText = highlightMatch(item, searchTerm);

            let listItem = document.createElement('li');
            listItem.innerHTML = displayText;
            listItem.className = 'dropdown-item';
            listItem.style.cursor = 'pointer';
            listItem.style.padding = '10px';
            listItem.style.borderBottom = '1px solid #eee';

            // Ховер-эффекты
            listItem.addEventListener('mouseenter', function () {
                this.style.backgroundColor = '#f5f5f5';
            });

            listItem.addEventListener('mouseleave', function () {
                this.style.backgroundColor = 'white';
            });

            // Клик по элементу
            listItem.onclick = function () {
                document.getElementById('searchInput').value = item.secName;
                hideDropdown();
                // Автоматически отправляем форму при выборе
                document.getElementById('text-srch').submit();
            };

            dropdownList.appendChild(listItem);
        });

        dropdownContainer.classList.add('active');
    }
}

// Скрытие dropdown
function hideDropdown() {
    const dropdownContainer = document.getElementById('dropdownResults');
    dropdownContainer.classList.remove('active');
}

// Подсветка совпадений
function highlightMatch(text, searchTerm) {
    if (!searchTerm || !text) return text;
    const regex = new RegExp(`(${escapeRegExp(searchTerm)})`, 'gi');
    return text.secName.replace(regex, '<strong>$1</strong>');
}

// Экранирование для RegExp
function escapeRegExp(string) {
    return string.replace(/[.*+?^${}()|[\]\\]/g, '\\$&');
}

// Дебаунс для оптимизации
function debouncedSearch() {
    if (document.getElementById('searchInput').value.length >= 1) {
        searchText = document.getElementById('searchInput').value.trim();
        clearTimeout(searchTimeout);
        searchTimeout = setTimeout(() => funcBrowseSort(searchText));
    } else {
        hideDropdown();
    }
}

// Закрытие dropdown при клике вне области
document.addEventListener('click', function (event) {
    const dropdown = document.getElementById('dropdownResults');
    const searchInput = document.getElementById('searchInput');

    if (!dropdown.contains(event.target) && event.target !== searchInput) {
        hideDropdown();
    }
});

//подсветка активной кнопки главного меню
document.addEventListener('DOMContentLoaded', function () {
    const buttons = document.querySelectorAll('.instrument-btn');
    const currentPath = window.location.pathname.toLowerCase();

    buttons.forEach(button => {
        const href = button.getAttribute('href') || button.pathname;
        if (href && currentPath.includes(href.toLowerCase())) {
            button.classList.add('active');
        }
    });
});