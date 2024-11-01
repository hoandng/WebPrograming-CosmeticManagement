document.addEventListener("DOMContentLoaded", function () {
   
    document.getElementById('toggle-password').addEventListener('click', function () {
        const passwordInput = document.querySelector('input[type="password"]');
        const passwordInputType = passwordInput.getAttribute('type') === 'password' ? 'text' : 'password';
        passwordInput.setAttribute('type', passwordInputType);
        this.querySelector('i').classList.toggle('fa-eye');
        this.querySelector('i').classList.toggle('fa-eye-slash');
    });

    
    document.getElementById('login-form').addEventListener('submit', function (event) {
        const username = this.querySelector('input[name="Username"]').value.trim();
        const password = this.querySelector('input[name="Password"]').value.trim();
        let hasError = false;

     
        if (!username) {
            document.getElementById('username-error').style.display = 'block';
            hasError = true;
        } else {
            document.getElementById('username-error').style.display = 'none';
        }

        if (!password) {
            document.getElementById('password-error').style.display = 'block';
            hasError = true;
        } else {
            document.getElementById('password-error').style.display = 'none';
        }

       
        if (hasError) {
            event.preventDefault();
            document.getElementById('login-error').style.display = 'none';
        }
    });
});
