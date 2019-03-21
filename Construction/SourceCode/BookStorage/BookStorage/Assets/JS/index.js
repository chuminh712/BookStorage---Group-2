$(window).load(function(){
    $('.loader-container').fadeOut('slow');
});

function showPassword() {
    const passwordField = document.getElementById('password-input');
    if (passwordField.type === "password") {
        passwordField.type = "text";
    } else {
        passwordField.type = "password";
    }
}

if (document.getElementById('sidebar')) {
    var sidebar = document.getElementById('sidebar');
}
if (document.getElementById('content')) {
    var content = document.getElementById('content');
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
    if (sidebar.style.width != '53px') {
        sidebar.style.width = '53px';
        content.style.width = `${ window.innerWidth - 53 }px`;
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

function showImportForm() {
    document.getElementsByClassName('import-form-container')[0].style.display = 'flex';
    document.getElementsByClassName('import-form-container')[0].style.background = 'rgba(109, 109, 109, 0.7)';
}

function closeImportForm() {
    document.getElementsByClassName('import-form-container')[0].style.display = 'none';
    document.getElementsByClassName('import-form-container')[0].style.background = 'rgba(0, 0, 0, 0.7)';
}