document.addEventListener('DOMContentLoaded', function () {
    const btn = document.getElementById('notificationBtn');
    const popup = document.getElementById('notificationPopup');

    btn.addEventListener('click', function (e) {
        e.stopPropagation();
        popup.style.display = popup.style.display === 'none' || popup.style.display === ''
            ? 'block'
            : 'none';
    });

    document.addEventListener('click', function (e) {
        if (popup.style.display === 'block' && !popup.contains(e.target) && e.target !== btn) {
            popup.style.display = 'none';
        }
    });
});
