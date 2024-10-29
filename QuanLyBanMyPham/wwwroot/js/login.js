document.addEventListener("DOMContentLoaded", function () {
    // Hiện/Ẩn mật khẩu
    document.getElementById('toggle-password').addEventListener('click', function () {
        const passwordInput = document.querySelector('input[type="password"]');

        // Kiểm tra xem trường mật khẩu có phải là loại "password" hay không
        const passwordInputType = passwordInput.getAttribute('type') === 'password' ? 'text' : 'password';
        passwordInput.setAttribute('type', passwordInputType);

        // Chuyển đổi biểu tượng giữa eye và eye-slash
        this.querySelector('i').classList.toggle('fa-eye');
        this.querySelector('i').classList.toggle('fa-eye-slash');
    });

    // Kiểm tra thông tin đăng nhập
    document.getElementById('login-form').addEventListener('submit', function (event) {
        const username = this.querySelector('input[name="Username"]').value;
        const password = this.querySelector('input[name="Password"]').value;
        let errorMessage = '';

        // Kiểm tra tên đăng nhập
        if (!username) {
            document.getElementById('username-error').style.display = 'block';
            errorMessage += 'Tên người dùng không được để trống.<br>';
        } else {
            document.getElementById('username-error').style.display = 'none';
        }

        // Kiểm tra mật khẩu
        if (!password) {
            document.getElementById('password-error').style.display = 'block';
            errorMessage += 'Mật khẩu không được để trống.<br>';
        } else {
            document.getElementById('password-error').style.display = 'none';
        }

        // Hiển thị thông báo lỗi nếu có
        if (errorMessage) {
            event.preventDefault(); // Ngăn gửi form
            document.getElementById('login-error').innerHTML = errorMessage;
            document.getElementById('login-error').style.display = 'block';
        } else {
            document.getElementById('login-error').style.display = 'none';
        }
    });
});
