// 입력 항목 체크

function validate() {
    'use strict'
    var forms = document.querySelectorAll('.needs-validation')
    let check = true;
    Array.prototype.slice.call(forms)
        .forEach(function (form) {
            form.addEventListener('submit', function (event) {
                if (!form.checkValidity()) {
                    event.preventDefault()
                    event.stopPropagation()
                    check = false;
                }

                form.classList.add('was-validated')
            }, false)
        })
    return check;
}