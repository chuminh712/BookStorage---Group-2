function showPassword() {
    const passwordField = document.getElementById('password-input');
    if (passwordField.type === "password") {
        passwordField.type = "text";
    } else {
        passwordField.type = "password";
    }
}
