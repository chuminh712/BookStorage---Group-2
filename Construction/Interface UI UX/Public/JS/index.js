function showPassword() {
    const passwordField = document.getElementById('password-input');
    if (passwordField.type === "password") {
        passwordField.type = "text";
    } else {
        passwordField.type = "password";
    }
}

function direct(location) {
    window.location.href = location;
}
if (document.getElementById('search-sidebar')) {
    var searchSidebar = document.getElementById('search-sidebar');
}
if (document.getElementById('sidebar-title')) {
    var sidebarTitle = document.getElementById('sidebar-title');
}
if (document.getElementById('sidebar')) {
    var sidebar = document.getElementById('sidebar');
}
if (document.getElementById('content')) {
    var content = document.getElementById('content');
}
if (document.getElementsByClassName('admin-info')[0]) {
    var adminInfo = document.getElementsByClassName('admin-info')[0];
}
if (document.getElementsByClassName('menu-item')) {
    var menuItems = document.getElementsByClassName('menu-item');
}
if (document.getElementById('avatar-sidebar')) {
    var avatarSidebar = document.getElementById('avatar-sidebar');
}
if (document.getElementsByClassName('administrator')) {
    var administrator = document.getElementsByClassName('administrator');
}

function menuDisplay() {
    if (sidebar.style.width != '3%') {
        sidebar.style.width = '3%';
        content.style.width = '97%';
        adminInfo.style.display = 'none';
        avatarSidebar.style.width = '30px';
        avatarSidebar.style.height = '30px';
        searchSidebar.style.display = 'none';
        sidebarTitle.style.display = 'none';
        for (i = 0; i < menuItems.length; i++) {
            menuItems[i].style.display = 'none';
        }
        setTimeout(function() {
            administrator[0].style.display = 'none';
            administrator[1].style.display = 'block';
        }, 200);
    } else {
        sidebar.style.width = '15%'
        content.style.width = '85%';
        avatarSidebar.style.width = '50px';
        avatarSidebar.style.height = '50px';
        setTimeout(function(){
            administrator[1].style.display = 'none';
            administrator[0].style.display = 'block';
            adminInfo.style.display = 'block';
            searchSidebar.style.display = 'block';
            sidebarTitle.style.display = 'block';
            for (i = 0; i < menuItems.length; i++) {
                menuItems[i].style.display = 'unset';
            }
        }, 200);
    }
}

function addRow() {
    var input = document.getElementsByTagName('input');
    var inputContainer = document.getElementsByClassName('input-container');
    var importFormRow = document.getElementById('import-form-row');
    var inputHTML = [];
    for (i = 0; i < inputContainer.length; i++) {
        var j = i * 6 + 2;
        inputHTML[i] = `
            <div class="input-container">
                <p>${ i + 1 }</p>
                <input type="${ input[j].type }" value="${ input[j].value }">
                <input type="${ input[j + 1].type }" value="${ input[j + 1].value }">
                <input type="${ input[j + 2].type }" value="${ input[j + 2].value }">            
                <input type="${ input[j + 3].type }" value="${ input[j + 3].value }">
                <input type="${ input[j + 4].type }" value="${ input[j + 4].value }">
                <input type="${ input[j + 5].type }" value="${ input[j + 5].value }">
            </div>
        `;
    }
    var oldValue = `${inputHTML.join("")}`;
    var newValue = `
        <div>
            <p>${ inputContainer.length + 1 }</p>
            <input type="text" placeholder="Mã sách">
            <input type="text" placeholder="Tên sách">
            <input type="number" placeholder="Số lượng dự kiến">
            <input type="number" placeholder="Số lượng thực nhận">
            <input type="number" placeholder="Gía tiền (VND)">
            <input type="number" placeholder="Thành tiền (VND)">
        </div>
    `;
    importFormRow.innerHTML = `${ oldValue }${ newValue }`;
}

function showImportForm() {
    if (document.getElementsByClassName('import-form-container')) {
        var importFormContainer = document.getElementsByClassName('import-form-container')[0];
        importFormContainer.style.display = "flex";
    }
}

function closeImportForm() {
    if (document.getElementsByClassName('import-form-container')) {
        var importFormContainer = document.getElementsByClassName('import-form-container')[0];
        importFormContainer.style.display = "none";
    }
}

function showExportForm() {
    if (document.getElementsByClassName('export-form-container')) {
        var exportFormContainer = document.getElementsByClassName('export-form-container')[0];
        exportFormContainer.style.display = "flex";
    }
}

function closeExportForm() {
    if (document.getElementsByClassName('export-form-container')) {
        var exportFormContainer = document.getElementsByClassName('export-form-container')[0];
        exportFormContainer.style.display = "none";
    }
}